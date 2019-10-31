using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj80;

using IronMeta.Matcher;
using IronMeta.Generator;
using IronMeta.Generator.AST;

namespace IronMeta.VSExtension
{
    [ComVisible(true)]
    [Guid(IronMetaGenerator.GeneratorGuidString)]
    [CodeGeneratorRegistration(
        typeof(IronMetaGenerator),
        "IronMetaGenerator",
        vsContextGuids.vsContextGuidVCSProject,
        GeneratesDesignTimeSource = true,
        GeneratorRegKeyName = "IronMetaGenerator")]
    [CodeGeneratorRegistration(
        typeof(IronMetaGenerator),
        "IronMetaGenerator",
        "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", // dot net core
        GeneratesDesignTimeSource = true,
        GeneratorRegKeyName = "IronMetaGenerator")]
    [ProvideObject(
        typeof(IronMetaGenerator),
        RegisterUsing = RegistrationMethod.CodeBase)]
    public class IronMetaGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        public const string GeneratorGuidString = "018E7744-5601-4800-961E-299DD73BF726";
        internal static string name = "IronMetaGenerator";

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
            ThreadHelper.ThrowIfNotOnUIThread();

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
            {
                throw new COMException("object is not sited", VSConstants.E_FAIL);
            }

            IntPtr pUnkSite = Marshal.GetIUnknownForObject(site);
            IntPtr intPointer = IntPtr.Zero;
            Marshal.QueryInterface(pUnkSite, ref riid, out intPointer);

            if (intPointer == IntPtr.Zero)
            {
                throw new COMException("site does not support requested interface", VSConstants.E_NOINTERFACE);
            }

            ppvSite = intPointer;
        }

        public void SetSite(object pUnkSite)
        {
            site = pUnkSite;
        }

        #endregion
    }
}
