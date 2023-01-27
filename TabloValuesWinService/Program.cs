using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Reflection;
using System.Configuration;
using System.Configuration.Install;

namespace TabloValuesWinService
{
    class Program
    {

        static void Main(string[] args)
        {
            //разбираю командную строку
            var configFile = Assembly.GetExecutingAssembly().Location;
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Compare(args[i], "-install", true) == 0
                  || string.Compare(args[i], "/install", true) == 0)
                {
                    InstallService();
                    return;
                }
                else if (string.Compare(args[i], "-uninstall", true) == 0
                   || string.Compare(args[i], "/uninstall", true) == 0)
                {
                    UninstallService();
                    return;
                }
            }
            //
            ServiceBase.Run(new TabloService());
            //var data = new OperationDataBase();
            //data.GetLastTrainEvents(ConfigurationManager.ConnectionStrings["tablo"].ConnectionString);
        }

        private static void InstallService()
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            IDictionary savedState = new Hashtable();
            string[] commandLine = { "/logFile=TabloService.log" };

            installer.Assembly = Assembly.GetExecutingAssembly();
            installer.CommandLine = commandLine;
            installer.UseNewContext = true;
            try
            {
                installer.Install(savedState);
            }
            catch
            {
                installer.Rollback(savedState);
                return;
            }
            finally
            {
                //Console.WriteLine("Нажмите любую клавишу для выхода...");
                //Console.ReadLine();
            }
        }

        private static void UninstallService()
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            IDictionary savedState = new Hashtable();
            string[] commandLine = { "/logFile=TabloService.log" };

            installer.Assembly = Assembly.GetCallingAssembly();
            installer.UseNewContext = true;
            installer.CommandLine = commandLine;
            try
            {
                installer.Uninstall(savedState);
            }
            catch
            {
                return;
            }
            finally
            {
                //Console.WriteLine("Нажмите любую клавишу для выхода...");
                //Console.ReadLine();
            }
        }
    }
}
