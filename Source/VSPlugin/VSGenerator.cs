//////////////////////////////////////////////////////////////////////
// Copyright © 2013 The IronMeta Project
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer.
//     * Redistributions in binary form must reproduce the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer in the documentation and/or other materials 
//       provided with the distribution.
//     * Neither the name of the IronMeta Project nor the names of its 
//       contributors may be used to endorse or promote products 
//       derived from this software without specific prior written 
//       permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE  IMPLIED WARRANTIES OF  MERCHANTABILITY AND FITNESS 
// FOR  A  PARTICULAR  PURPOSE  ARE DISCLAIMED. IN  NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT  LIMITED TO, PROCUREMENT  OF SUBSTITUTE  GOODS  OR SERVICES; 
// LOSS OF USE, DATA, OR  PROFITS; OR  BUSINESS  INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF  LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR  TORT (INCLUDING NEGLIGENCE  OR OTHERWISE) ARISING IN 
// ANY WAY OUT  OF THE  USE OF THIS SOFTWARE, EVEN  IF ADVISED  OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

using IronMeta.Generator;
using IronMeta.Matcher;
using Microsoft.Win32;
using System.Diagnostics;

namespace IronMeta.VSPlugin
{

    [ComVisible(true)]
    [Guid("BBFE6A50-F3A7-409A-97E4-08217808896F")]
    public class VSGenerator : IVsSingleFileGenerator
    {
        #region IVsSingleFileGenerator Members

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".g.cs";
            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            using (var ms = new MemoryStream())
            {
                MatchResult<char, AST.Node> result;

                using (var tw = new StreamWriter(ms, Encoding.UTF8))
                {
                    pGenerateProgress.Progress(0, 1);
                    result = CSharpShell.Process(bstrInputFileContents, tw, wszDefaultNamespace);
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
                    int line = CharMatcher<AST.Node>.GetLineNumber(result.Memo, result.ErrorIndex);

                    pGenerateProgress.GeneratorError(0, 0, result.Error, (uint)(line - 1), uint.MaxValue);

                    rgbOutputFileContents = null;
                    pcbOutput = 0;

                    return VSConstants.E_FAIL;
                }
            }
        }

        #endregion

        #region COM Registration and Unregistration

        static readonly string[] VS_PRODUCTS = { "VisualStudio", "VSWinExpress", "VWDExpress", "WDExpress" };
        static readonly string[] VS_VERSIONS = { "11.0", "11.0_Config" };

        const string VS_PREFIX = @"Software\Microsoft\";
        const string VS_KEY = @"\Generators\{fae04ec1-301f-11d3-bf4b-00c04f79efbc}\";
        const string TOOL_NAME = "IronMetaGenerator";
        const string TOOL_EXT = ".ironmeta";

        static IEnumerable<string> GetVSKeys()
        {
            return VS_PRODUCTS.SelectMany(product => VS_VERSIONS.Select(version => VS_PREFIX + product + @"\" + version + VS_KEY));
        }

        [ComRegisterFunction]
        private static void ComRegisterFunction(Type t)
        {
            var guid = t.GUID.ToString("B");

            using (var classKey = Registry.ClassesRoot.CreateSubKey(@"CLSID\" + guid))
            {
                classKey.SetValue("", "COM+ class: " + t.FullName);
                classKey.SetValue("InprocServer32", "C:\\WINDOWS\\system32\\mscoree.dll");
                classKey.SetValue("ThreadingModel", "Both");
                classKey.SetValue("Class", t.FullName);
                classKey.SetValue("Assembly", t.Assembly.FullName);
            }

            foreach (var vs_key in GetVSKeys())
            {                
                using (var vsKey = Registry.CurrentUser.CreateSubKey(vs_key + TOOL_NAME))
                {
                    vsKey.SetValue("", "IronMeta C# Generator");
                    vsKey.SetValue("CLSID", guid);
                    vsKey.SetValue("GeneratesDesignTimeSource", 0, RegistryValueKind.DWord);
                }

                using (var extKey = Registry.CurrentUser.CreateSubKey(vs_key + TOOL_EXT))
                {
                    extKey.SetValue("", "IronMetaGenerator");
                }
            }
        }

        [ComUnregisterFunction]
        private static void ComUnregisterFunction(Type t)
        {
            var guid = t.GUID.ToString("B");

            foreach (var vs_key in GetVSKeys())
            {
                Registry.LocalMachine.DeleteSubKey(vs_key + TOOL_EXT);
                Registry.CurrentUser.DeleteSubKeyTree(vs_key + TOOL_NAME);
                Registry.LocalMachine.DeleteSubKey(vs_key + TOOL_EXT);
                Registry.CurrentUser.DeleteSubKeyTree(vs_key + TOOL_NAME);
            }

            Registry.ClassesRoot.DeleteSubKeyTree(@"CLSID\" + guid);
        }

        #endregion
    }

}
