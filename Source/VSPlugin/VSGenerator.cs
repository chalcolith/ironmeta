//////////////////////////////////////////////////////////////////////
//
// Copyright © 2014 Gordon Tisher
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
                    int num, offset;
                    var line = CharMatcher<AST.Node>.GetLine(result.Memo, result.ErrorIndex, out num, out offset);
                    pGenerateProgress.GeneratorError(0, 0, result.Error, (uint)(num - 1), (uint)offset);

                    rgbOutputFileContents = null;
                    pcbOutput = 0;

                    return VSConstants.E_FAIL;
                }
            }
        }

        #endregion
    }

}
