using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;

namespace Aton.Services.Host
{
    /// <summary>
    /// 服务安装类
    /// </summary>
    internal class ServiceInstallerHelper
    {
        /// <summary>
        /// 改变服务启动类型 [手动或自动]
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="isAutoStart">是否自动启动</param>
        /// <returns>是否成功</returns>
        public bool ChangeServiceStartType(string serviceName, NativeInstaller.ServiceStartType startType)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hSCManagerLock = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            bool result = true;
            try
            {
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hSCManagerLock = NativeInstaller.LockServiceDatabase(hSCManager);
                if (hSCManagerLock == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "无法锁定服务控制台");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                result = NativeInstaller.ChangeServiceConfig(hService, NativeInstaller.ServiceType.SERVICE_WIN32_OWN_PROCESS, startType, -1, null, null, 0, null, null, null, null);
                if (!result) throw new Win32Exception(Marshal.GetLastWin32Error(), "无法修改服务配置");
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                result = false;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManagerLock != IntPtr.Zero)
                {
                    NativeInstaller.UnlockServiceDatabase(hSCManagerLock);
                    hSCManagerLock = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取服务启动类型 [手动或自动]
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns>服务启动类型</returns>
        public NativeInstaller.ServiceStartType GetServiceStartType(string serviceName)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            int flag = 4;
            try
            {
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                NativeInstaller.QUERY_SERVICE_CONFIG config = new NativeInstaller.QUERY_SERVICE_CONFIG();
                IntPtr ptr = Marshal.AllocHGlobal(4096);
                int needBytes = 0;
                bool result = NativeInstaller.QueryServiceConfig(hService, ptr, 4096, ref needBytes);
                Marshal.PtrToStructure(ptr, config);
                Marshal.FreeHGlobal(ptr);
                if (!result) throw new Win32Exception(Marshal.GetLastWin32Error(), "获取服务启动类型失败！");
                flag = config.dwStartType;
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                flag = 4;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
            }
            return (NativeInstaller.ServiceStartType)flag;
        }

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>状态标识</returns>
        public NativeInstaller.ServiceState GetServiceState(string serviceName)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            int flag = 0;
            try
            {
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                NativeInstaller.SERVICE_STATUS_PROCESS status = new NativeInstaller.SERVICE_STATUS_PROCESS();
                int needBytes = 0;
                if (!NativeInstaller.QueryServiceStatusEx(hService, NativeInstaller.ServiceStatusType.SC_STATUS_PROCESS_INFO, ref status, 36, ref needBytes)) throw new Win32Exception(Marshal.GetLastWin32Error(), "获取服务状态失败！");
                flag = status.dwCurrentState;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                flag = 0;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
            }
            return (NativeInstaller.ServiceState)flag;
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="servicePath">服务路径</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceDispName">服务友好名称</param>
        /// <param name="serviceDescription">描述</param>
        /// <param name="isAutoStart">是否自动启动</param>
        /// <returns>是否成功</returns>
        public bool InstallService(string servicePath, string serviceName, string serviceDispName, string serviceDescription, NativeInstaller.ServiceStartType startType)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hSCManagerLock = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            bool result = true;
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hSCManagerLock = NativeInstaller.LockServiceDatabase(hSCManager);
                if (hSCManagerLock == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "无法锁定服务控制台");
                hService = NativeInstaller.CreateService(hSCManager, serviceName, serviceDispName, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS, NativeInstaller.ServiceType.SERVICE_WIN32_OWN_PROCESS, startType, NativeInstaller.ServiceErrorControl.SERVICE_ERROR_NORMAL, servicePath, null, 0, null, null, null);
                if (hService == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "创建服务失败！");
                NativeInstaller.SERVICE_DESCRIPTION description = new NativeInstaller.SERVICE_DESCRIPTION
                {
                    lpDescription = serviceDescription
                };
                result = NativeInstaller.ChangeServiceConfig2(hService, NativeInstaller.InfoLevel.SERVICE_CONFIG_DESCRIPTION, ref description);
                if (!result) throw new Win32Exception(Marshal.GetLastWin32Error(), "无法修改服务描述");
                NativeInstaller.SERVICE_FAILURE_ACTIONS failureActions = new NativeInstaller.SERVICE_FAILURE_ACTIONS
                {
                    dwResetPeriod = 600,
                    lpRebootMsg = "服务启动失败! 正在重启中...",
                    lpCommand = ""
                };
                NativeInstaller.SC_ACTION[] actions = new NativeInstaller.SC_ACTION[3];
                failureActions.cActions = actions.Length;
                actions[0].Delay = 30000;
                actions[0].SCActionType = NativeInstaller.SC_ACTION_TYPE.SC_ACTION_RESTART;
                actions[1].Delay = 30000;
                actions[1].SCActionType = NativeInstaller.SC_ACTION_TYPE.SC_ACTION_RESTART;
                actions[2].Delay = 30000;
                actions[2].SCActionType = NativeInstaller.SC_ACTION_TYPE.SC_ACTION_NONE;
                IntPtr ptrScActions = new IntPtr();
                ptrScActions = Marshal.AllocHGlobal((int)(Marshal.SizeOf(new NativeInstaller.SC_ACTION()) * 3));
                NativeInstaller.CopyMemory(ptrScActions, actions, Marshal.SizeOf(new NativeInstaller.SC_ACTION()) * 3);
                failureActions.lpsaActions = ptrScActions.ToInt32();
                result = NativeInstaller.ChangeServiceConfig2(hService, NativeInstaller.InfoLevel.SERVICE_CONFIG_FAILURE_ACTIONS, ref failureActions);
                if (!result) throw new Win32Exception(Marshal.GetLastWin32Error(), "无法设置服务的故障恢复模式");
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                result = false;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManagerLock != IntPtr.Zero)
                {
                    NativeInstaller.UnlockServiceDatabase(hSCManagerLock);
                    hSCManagerLock = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
                Console.ResetColor();
            }
            return result;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        public bool StartService(string serviceName)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            bool result = true;
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                //检测服务状态
                NativeInstaller.ServiceState state = GetServiceState(serviceName);
                if (state == NativeInstaller.ServiceState.SERVICE_START_PENDING)
                {
                    throw new Exception(string.Format("服务正在启动中.....请耐心等待...."));
                }
                if (state == NativeInstaller.ServiceState.SERVICE_RUNNING)
                {
                    throw new Exception(string.Format("服务已经启动运行成功"));
                }
                if (NativeInstaller.StartService(hService, 0, null) == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "启动指定的服务出现系统异常！");
                Console.WriteLine("正在启动服务,请稍等10秒钟........");
                Thread.Sleep(10000);
                state = GetServiceState(serviceName);
                if (state == NativeInstaller.ServiceState.SERVICE_START_PENDING)
                {
                    Console.WriteLine("服务未启动完成,继续等待5秒钟启动........");
                    Thread.Sleep(5000);
                }
                state = GetServiceState(serviceName);
                if (state != NativeInstaller.ServiceState.SERVICE_RUNNING)
                {
                    throw new Exception(string.Format("启动指定的服务[{0}]失败！当前服务状态为[{1}]", serviceName, state));
                }
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                result = false;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
                Console.ResetColor();
            }
            return result;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        public bool StopService(string serviceName)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            bool result = true;
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                NativeInstaller.SERVICE_STATUS status = new NativeInstaller.SERVICE_STATUS();
                result = NativeInstaller.ControlService(hService, NativeInstaller.ServiceControlType.SERVICE_CONTROL_STOP, ref status);
                if (!result) throw new Win32Exception(Marshal.GetLastWin32Error(), "停止指定的服务失败！");
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                result = false;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
                Console.ResetColor();
            }
            return result;
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        public bool UnInstallService(string serviceName)
        {
            IntPtr hSCManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            bool result = true;
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                hSCManager = NativeInstaller.OpenSCManager(null, null, NativeInstaller.ServiceControlManagerType.SC_MANAGER_ALL_ACCESS);
                if (hSCManager == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "不能打开服务控制台！");
                hService = NativeInstaller.OpenService(hSCManager, serviceName, NativeInstaller.ACCESS_TYPE.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero) throw new ApplicationException("不能打开指定服务");
                if (NativeInstaller.DeleteService(hService) == 0) throw new Win32Exception(Marshal.GetLastWin32Error(), "删除指定的服务失败！");
            }
            catch (Exception objExp)
            {
                Console.Error.WriteLine(objExp.Message);
                result = false;
            }
            finally
            {
                if (hService != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hService);
                    hService = IntPtr.Zero;
                }
                if (hSCManager != IntPtr.Zero)
                {
                    NativeInstaller.CloseServiceHandle(hSCManager);
                    hSCManager = IntPtr.Zero;
                }
                Console.ResetColor();
            }
            return result;
        }
    }
}




