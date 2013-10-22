//////////////////////////////////////////////////////////////////////
//
// Copyright © 2013 Verophyle Informatics
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace IronMeta.VSPackage
{
    class Program
    {
        #region Registration

        static readonly string[] VS_PRODUCTS = { "VisualStudio", "VSWinExpress", "VWDExpress", "WDExpress" };
        static readonly string[] VS_VERSIONS = { "12.0_Config", "12.0", "11.0_Config", "11.0" };

        const string VS_PREFIX = @"Software\Microsoft\";
        const string VS_KEY = @"\Generators\{fae04ec1-301f-11d3-bf4b-00c04f79efbc}";
        const string TOOL_NAME = @"\IronMetaGenerator";
        const string TOOL_EXT = @"\.ironmeta";

        static IEnumerable<string> GetVSKeys()
        {
            return VS_PRODUCTS.SelectMany(product => VS_VERSIONS.Select(version => VS_PREFIX + product + @"\" + version + VS_KEY));
        }

        private static void GacInstall(string path)
        {
            var assembly = Assembly.LoadFile(path);
            if (!assembly.GlobalAssemblyCache)
            {
                var publisher = new System.EnterpriseServices.Internal.Publish();
                publisher.GacInstall(assembly.Location);
            }
        }

        private static void Register(string path)
        {
            var installed = DateTime.UtcNow.ToString("u");

            // get DLL
            var pluginAssembly = Assembly.LoadFile(path);
            var generatorType = pluginAssembly.GetType("IronMeta.VSPlugin.VSGenerator");

            Console.WriteLine("VSPackage got assembly {0}, type {1}, guid {2}", pluginAssembly.FullName, generatorType.FullName, generatorType.GUID);

            var publisher = new System.EnterpriseServices.Internal.Publish();
            publisher.GacInstall(pluginAssembly.Location);
            publisher.RegisterAssembly(pluginAssembly.Location);

            // register for all known versions of VS11 that are installed
            var guid = generatorType.GUID.ToString("B");

            foreach (var product in VS_PRODUCTS)
            {
                foreach (var version in VS_VERSIONS)
                {
                    var parentPath = VS_PREFIX + product + @"\" + version;

                    using (var parentKey = Registry.CurrentUser.OpenSubKey(parentPath))
                    {
                        if (parentKey == null)
                            continue;
                    }

                    using (var clsidKey = Registry.CurrentUser.CreateSubKey(parentPath + @"\CLSID\" + guid))
                    {
                        clsidKey.SetValue("", "COM+ class: " + generatorType.FullName);
                        clsidKey.SetValue("InprocServer32", "mscoree.dll");
                        clsidKey.SetValue("ThreadingModel", "Both");
                        clsidKey.SetValue("Class", generatorType.FullName);
                        clsidKey.SetValue("Assembly", generatorType.Assembly.FullName);
                        clsidKey.SetValue("Installed", installed);
                    }

                    using (var genKey = Registry.CurrentUser.CreateSubKey(parentPath + VS_KEY + TOOL_NAME))
                    {
                        genKey.SetValue("", "IronMeta C# Generator");
                        genKey.SetValue("CLSID", guid);
                        genKey.SetValue("GeneratesDesignTimeSource", 1, RegistryValueKind.DWord);
                        genKey.SetValue("Installed", installed);
                    }

                    using (var extKey = Registry.CurrentUser.CreateSubKey(parentPath + VS_KEY + TOOL_EXT))
                    {
                        extKey.SetValue("", "IronMetaGenerator");
                        extKey.SetValue("Installed", installed);
                    }
                }
            }
        }

        #endregion

        public static int Main(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; ++i)
                {
                    if (i == 0)
                        Register(args[i]);
                    else
                        GacInstall(args[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return 1;
            }

            return 0;
        }
    }
}
