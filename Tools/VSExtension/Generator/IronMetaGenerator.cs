using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj80;

using IronMeta.Matcher;
using IronMeta.Generator;
using IronMeta.Generator.AST;

namespace IronMeta.VSExtension.Generator
{
    [ComVisible(true)]
    [Guid(VSPackageGuids.SingleFileGeneratorGuidString)]
    [ProvideObject(typeof(IronMetaGenerator))]
    [CodeGeneratorRegistration(
        typeof(IronMetaGenerator), 
        "IronMetaGenerator", 
        vsContextGuids.vsContextGuidVCSProject,
        GeneratesDesignTimeSource = true)]
    public class IronMetaGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        object site = null;

        #region IVsSingleFileGenerator Members

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".g.cs";
            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, 
            IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            using (var ms = new MemoryStream())
            {
                MatchResult<char, AstNode> result;

                using (var tw = new StreamWriter(ms, Encoding.UTF8))
                {
                    pGenerateProgress.Progress(0, 1);
                    result = CSharpShell.Process(wszInputFilePath, bstrInputFileContents, tw, wszDefaultNamespace);
                    pGenerateProgress.Progress(1, 1);
                }

                if (result.Success)
                {
                    byte[] bytes = ms.ToArray();
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(bytes.Length);
                    Marshal.Copy(bytes, 0, rgbOutputFileContents[0], bytes.Length);
                    pcbOutput = (uint)bytes.Length;

                    return VSConstants.S_OK;
                }
                else
                {
                    int num, offset;
                    var line = result.MatchState.GetLine(result.ErrorIndex, out num, out offset);
                    pGenerateProgress.GeneratorError(0, 0, result.Error, (uint)(num - 1), (uint)offset);

                    rgbOutputFileContents = null;
                    pcbOutput = 0;

                    return VSConstants.E_FAIL;
                }
            }
        }

        #endregion

        #region IObjectWithSite Members

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr pUnkSite = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(pUnkSite, ref riid, out ppvSite);
            Marshal.Release(pUnkSite);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
        }

        public void SetSite(object pUnkSite)
        {
            site = pUnkSite;
        }
        
        #endregion
    }
}
