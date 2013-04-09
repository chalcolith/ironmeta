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
        static readonly string[] VS_VERSIONS = { "11.0", "11.0_Config" };

        const string VS_PREFIX = @"Software\Microsoft\";
        const string VS_KEY = @"\Generators\{fae04ec1-301f-11d3-bf4b-00c04f79efbc}";
        const string TOOL_NAME = @"\IronMetaGenerator";
        const string TOOL_EXT = @"\.ironmeta";

        static IEnumerable<string> GetVSKeys()
        {
            return VS_PRODUCTS.SelectMany(product => VS_VERSIONS.Select(version => VS_PREFIX + product + @"\" + version + VS_KEY));
        }

        private static void Register(string path)
        {
            // get DLL
            var pluginAssembly = Assembly.LoadFile(path);
            var generatorType = pluginAssembly.GetType("IronMeta.VSPlugin.VSGenerator");

            Console.WriteLine("VSPackage got assembly {0}, type {1}, guid {2}", pluginAssembly.FullName, generatorType.FullName, generatorType.GUID);

            if (!pluginAssembly.GlobalAssemblyCache)
            {
                //Console.WriteLine("VSPackage registering VSPlugin in GAC");

                var publisher = new System.EnterpriseServices.Internal.Publish();
                publisher.GacInstall(pluginAssembly.Location);
            }

            // register class
            var installed = DateTime.Now.ToString("u");

            var guid = generatorType.GUID.ToString("B");

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + generatorType.FullName))
                key.SetValue("", generatorType.FullName);

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + generatorType.FullName + @"\CLSID"))
                key.SetValue("", guid);

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + @"\CLSID\" + guid))
                key.SetValue("", generatorType.FullName);

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + @"\CLSID\" + guid + @"\InprocServer32"))
            {
                key.SetValue("", "mscoree.dll");
                key.SetValue("ThreadingModel", "Both");
                key.SetValue("Class", generatorType.FullName);
                key.SetValue("Assembly", pluginAssembly.FullName);
                key.SetValue("RuntimeVersion", pluginAssembly.ImageRuntimeVersion);
                key.SetValue("Installed", installed);
            }

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + @"\CLSID\" + guid + @"\InprocServer32\" + pluginAssembly.GetName().Version.ToString()))
            {
                key.SetValue("Class", generatorType.FullName);
                key.SetValue("Assembly", pluginAssembly.FullName);
                key.SetValue("RuntimeVersion", pluginAssembly.ImageRuntimeVersion);
                key.SetValue("Installed", installed);
            }

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + @"\CLSID\" + guid + @"\ProgId"))
                key.SetValue("", generatorType.FullName);

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + @"\CLSID\" + guid + @"\Implemented Categories\{62C8FE65-4EBB-45E7-B440-6E39B2CDBF29}"))
                key.SetValue("Installed", installed);

            // register for all known versions of VS11
            foreach (var product in VS_PRODUCTS)
            {
                foreach (var version in VS_VERSIONS)
                {
                    var rootKey = VS_PREFIX + product + @"\" + version;

                    using (var clsidKey = Registry.CurrentUser.CreateSubKey(rootKey + @"\CLSID\" + guid))
                    {
                        //Console.WriteLine("VSPackage setting class info in " + clsidKey.Name);

                        clsidKey.SetValue("", "COM+ class: " + generatorType.FullName);
                        clsidKey.SetValue("InprocServer32", "mscoree.dll");
                        clsidKey.SetValue("ThreadingModel", "Both");
                        clsidKey.SetValue("Class", generatorType.FullName);
                        clsidKey.SetValue("Assembly", generatorType.Assembly.FullName);
                        clsidKey.SetValue("Installed", installed);
                    }

                    using (var genKey = Registry.CurrentUser.CreateSubKey(rootKey + VS_KEY + TOOL_NAME))
                    {
                        //Console.WriteLine("VSPackage setting generator info in " + genKey.Name);

                        genKey.SetValue("", "IronMeta C# Generator");
                        genKey.SetValue("CLSID", guid);
                        genKey.SetValue("GeneratesDesignTimeSource", 1, RegistryValueKind.DWord);
                        genKey.SetValue("Installed", installed);
                    }

                    using (var extKey = Registry.CurrentUser.CreateSubKey(rootKey + VS_KEY + TOOL_EXT))
                    {
                        //Console.WriteLine("VSPackage setting extension info in " + extKey.Name);

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
                if (args.Length > 0)
                    Register(args[0]);
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
