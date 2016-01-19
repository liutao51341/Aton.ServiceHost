using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Reflection;
using Aton.Services.Host;
using Aton.ServiceHost.ServiceConfigHandler;
using System.Threading;
using System.Windows.Forms;
using Aton.ServicesHost;

namespace Aton.ServiceHost
{
    static partial class Program
    {
        //static string ServiceName = ConfigurationManager.AppSettings["ServiceName"];
        //static string ServiceDesc = ConfigurationManager.AppSettings["ServiceDesc"];
        static ServiceInstallerHelper helper = new ServiceInstallerHelper();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string _exePath = Assembly.GetExecutingAssembly().Location;
            AtonServiceConfigHandler configHandler;
            try
            {
               
                  configHandler = new AtonServiceConfigHandler();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write(ex.StackTrace);
                return;
            }
            if (configHandler.ServiceSummary.ServiceName.Length == 0)
            {
                Console.WriteLine("Error��Unable get service name");
                return;
            }
            if (args != null && args.Length > 0)
            {
                if (args[0].Equals("-i", StringComparison.OrdinalIgnoreCase))
                {
                    bool r = helper.InstallService(_exePath, configHandler.ServiceSummary.ServiceName, configHandler.ServiceSummary.ServiceName, configHandler.ServiceSummary.ServiceDesc, NativeInstaller.ServiceStartType.SERVICE_AUTO_START);
                    Console.WriteLine("Service Name :" + configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Desc :" + configHandler.ServiceSummary.ServiceDesc);
                    Console.WriteLine("Install Result :" + (r ? "Success" : "Fail"));
                    return;
                }
                else if (args[0].Equals("-u", StringComparison.OrdinalIgnoreCase))
                {
                    bool r = helper.UnInstallService(configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Name :" + configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Desc :" + configHandler.ServiceSummary.ServiceDesc);
                    Console.WriteLine("Uninstall :" + (r ? "Success" : "Fail"));
                    return;
                }
                else if (args[0].Equals("-r", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Service Name :" + configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Desc :" + configHandler.ServiceSummary.ServiceDesc);
                    bool r = helper.StartService(configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Start :" + (r ? "Success" : "Fail"));
                }
                else if (args[0].Equals("-s", StringComparison.OrdinalIgnoreCase))
                {
                    bool r = helper.StopService(configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Name :" + configHandler.ServiceSummary.ServiceName);
                    Console.WriteLine("Service Desc :" + configHandler.ServiceSummary.ServiceDesc);
                    Console.WriteLine("Stop :" + (r ? "Success" : "Fail"));
                }
                else if (args[0].Equals("-l", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(string.Format("Host Service List�� {0} \n", configHandler.Count));
                    foreach (var item in configHandler)
                    {
                        Console.WriteLine(string.Format("Service Name: {0} \nFullName��{1}", item.Name, item.Type.Split(',')[0]));
                        Console.WriteLine("");
                    }
                    return;
                }
                else if (args[0].Equals("-g", StringComparison.OrdinalIgnoreCase))
                {
                    var r = helper.GetServiceState(configHandler.ServiceSummary.ServiceName);
                    switch (r)
                    {
                        case NativeInstaller.ServiceState.SERVICE_STOPPED:
                            Console.WriteLine("Service State: Stopped");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_START_PENDING:
                            Console.WriteLine("Service State: Wait Start");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_STOP_PENDING:
                            Console.WriteLine("Service State: Wait Stop");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_RUNNING:
                            Console.WriteLine("Service State: Running");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_CONTINUE_PENDING:
                            Console.WriteLine("Service State: Wait Continue");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_PAUSE_PENDING:
                            Console.WriteLine("Service State: Wait Pause");
                            break;
                        case NativeInstaller.ServiceState.SERVICE_PAUSED:
                            Console.WriteLine("Service State: Pause");
                            break;
                    }
                }
                else if (args[0].Equals("/gui", StringComparison.OrdinalIgnoreCase))
                {
                    HostGUI gui = new HostGUI();
   
                    gui.InitHost(configHandler, _exePath);
                    gui.ShowDialog();
                    Console.ReadKey();
 
                }
                else if (args[0].Equals("/?", StringComparison.OrdinalIgnoreCase))
                {
                    WriteHelp();
                }
                else if (args[0].Equals("/c", StringComparison.OrdinalIgnoreCase))
                {
                    WriteHelpC();
                }
            }
            else
            {
                 RunAsService();
            }
        }

        static void WriteHelp()
        {
            Console.Error.WriteLine("");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("------------------------------------------------------------------------------");
            Console.Error.WriteLine("AtonServiceHost    [-I] [-U] [-R] [-S] -[L] [-A] [-M] [-D] -[G] /[GUI]  [/?] [/C]");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("Desc: Service Manager Command Program, Paramemter is Case sensitive.");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("    -I           Install the service");
            Console.Error.WriteLine("    -U           Uninstall the service");
            Console.Error.WriteLine("    -R           Running the service");
            Console.Error.WriteLine("    -S           Stop the service");
            Console.Error.WriteLine("    -L           List All Managed Sub Service");
            Console.Error.WriteLine("    -A           Change service start Type to automatic");
            Console.Error.WriteLine("    -M           Change service start Type to manual");
            Console.Error.WriteLine("    -D           Change service start Type to disable");
            Console.Error.WriteLine("    -G           Get Aton Container Service Status");
            Console.Error.WriteLine("    -GUI      Launch the GUI interface manager");
            Console.Error.WriteLine("    /?           Print The Help Informathon");
            Console.Error.WriteLine("    /C           Print The Chinese Help Informathon ");
            Console.Error.WriteLine("------------------------------------------------------------------------------");
        }

        static void WriteHelpC()
        {
            Console.Error.WriteLine("");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("------------------------------------------------------------------------------");
            Console.Error.WriteLine("AtonServiceHost    [-I] [-U] [-R] [-S] -[L] [-A] [-M] [-D] -[G] /[GUI] [/?] [/C]");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("����: �����з���������, �����ǷǴ�Сд����.");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("    -I           ��װ����");
            Console.Error.WriteLine("    -U           ж�ط���");
            Console.Error.WriteLine("    -R           ��������");
            Console.Error.WriteLine("    -S           ֹͣ����");
            Console.Error.WriteLine("    -L           �йܵ��ӷ����б�");
            Console.Error.WriteLine("    -A           ���ķ�����������Ϊ���Զ�");
            Console.Error.WriteLine("    -M           ���ķ�����������Ϊ���ֶ�");
            Console.Error.WriteLine("    -D           ���ķ�����������Ϊ������");
            Console.Error.WriteLine("    -G           ��ȡAton�й����������״̬");
            Console.Error.WriteLine("    -GUI      ����GUIӦ�ù������");
            Console.Error.WriteLine("    /?           ��ӡ������Ϣ");
            Console.Error.WriteLine("    /C           ��ӡ���İ�����Ϣ");
            Console.Error.WriteLine("------------------------------------------------------------------------------");
        }

        static void RunAsService()
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[] { new MainService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}