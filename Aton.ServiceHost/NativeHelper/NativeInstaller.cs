using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Aton.Services.Host
{
    /// <summary>
    /// installer 
    /// </summary>
    internal static class NativeInstaller
    {
        /// <summary>
        /// 输出缓冲区不够大，无法容纳设备ID
        /// </summary>
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        /// <summary>
        /// 普通读权限
        /// </summary>
        public const int GENERIC_READ = -2147483648;
        /// <summary>
        /// 不改变
        /// </summary>
        public const int SERVICE_NO_CHANGE = -1;
        /// <summary>
        /// 必要权限
        /// </summary>
        public const int STANDARD_RIGHTS_REQUIRED = 983040;

        /// <summary>
        /// 改变服务配置
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="dwServiceType">服务类型</param>
        /// <param name="dwStartType">启动类型：自动、手动还是禁止</param>
        /// <param name="dwErrorControl">服务失败的严重性</param>
        /// <param name="lpBinaryPathName">实现服务代码的二进制文件的路径</param>
        /// <param name="lpLoadOrderGroup">加载顺序组的名称</param>
        /// <param name="lpdwTagId">接受Tag标志码</param>
        /// <param name="lpDependencies">依赖服务的名称组</param>
        /// <param name="lpServiceStartName">启动服务的帐号，如果为NULL，表示使用LocalSystem</param>
        /// <param name="lpPassword">启动服务帐号的口令</param>
        /// <param name="lpDisplayName">友好名称</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool ChangeServiceConfig(IntPtr hService, ServiceType dwServiceType, ServiceStartType dwStartType, int dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword, string lpDisplayName);
        /// <summary>
        /// 改变服务配置2
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="dwInfoLevel">信息级别</param>
        /// <param name="lpInfo">信息</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool ChangeServiceConfig2(IntPtr hService, InfoLevel dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_DESCRIPTION lpInfo);
        /// <summary>
        /// 改变服务配置2
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="dwInfoLevel">信息级别</param>
        /// <param name="lpInfo">信息</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool ChangeServiceConfig2(IntPtr hService, InfoLevel dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_FAILURE_ACTIONS lpInfo);
        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="hSCObject">对象句柄</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool CloseServiceHandle(IntPtr hSCObject);
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="dwControl">停止指令</param>
        /// <param name="lpServiceStatus">状态</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool ControlService(IntPtr hService, ServiceControlType dwControl, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_STATUS lpServiceStatus);
        /// <summary>
        /// 复制内存
        /// </summary>
        /// <param name="dst">目标</param>
        /// <param name="src">源</param>
        /// <param name="length">长度</param>
        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(IntPtr dst, SC_ACTION[] src, int length);
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="hSCManager">控制台句柄</param>
        /// <param name="lpSvcName">服务名称</param>
        /// <param name="lpDisplayName">友好名称</param>
        /// <param name="dwDesiredAccess">访问权限</param>
        /// <param name="dwServiceType">服务类型：独立进程、共享进程、驱动程序还是交互式登录模式</param>
        /// <param name="dwStartType">启动类型：自动、手动还是禁止</param>
        /// <param name="dwErrorControl">服务失败的严重性</param>
        /// <param name="lpPathName">实现服务代码的二进制文件的路径</param>
        /// <param name="lpLoadOrderGroup">加载顺序组的名称</param>
        /// <param name="lpdwTagId">接受Tag标志码</param>
        /// <param name="lpDependencies">依赖服务的名称组</param>
        /// <param name="lpServiceStartName">启动服务的帐号，如果为NULL，表示使用LocalSystem</param>
        /// <param name="lpPassword">启动服务帐号的口令</param>
        /// <returns>句柄</returns>
        [DllImport("advapi32.dll")]
        public static extern IntPtr CreateService(IntPtr hSCManager, string lpSvcName, string lpDisplayName, ServiceControlManagerType dwDesiredAccess, ServiceType dwServiceType, ServiceStartType dwStartType, ServiceErrorControl dwErrorControl, string lpPathName, string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <returns>结果</returns>
        [DllImport("advapi32.dll")]
        public static extern int DeleteService(IntPtr hService);
        /// <summary>
        /// 锁定服务数据库
        /// </summary>
        /// <param name="hSCManager">控制台句柄</param>
        /// <returns>锁定服务数据库句柄</returns>
        [DllImport("advapi32.dll")]
        public static extern IntPtr LockServiceDatabase(IntPtr hSCManager);
        /// <summary>
        /// 打开控制台
        /// </summary>
        /// <param name="lpMachineName">机器名</param>
        /// <param name="lpDatabaseName">数据库名</param>
        /// <param name="dwDesiredAccess">访问权限</param>
        /// <returns>控制台句柄</returns>
        [DllImport("advapi32.dll")]
        public static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceControlManagerType dwDesiredAccess);
        /// <summary>
        /// 打开服务
        /// </summary>
        /// <param name="hSCManager">控制台句柄</param>
        /// <param name="lpServiceName">服务名称</param>
        /// <param name="dwDesiredAccess">访问权限</param>
        /// <returns>服务句柄</returns>
        [DllImport("advapi32.dll")]
        public static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ACCESS_TYPE dwDesiredAccess);
        /// <summary>
        /// 查询服务配置
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="config">服务配置</param>
        /// <param name="cbBufSize">缓冲区大小</param>
        /// <param name="pcbBytesNeeded">需要字节数</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool QueryServiceConfig(IntPtr hService, IntPtr config, int cbBufSize, ref int pcbBytesNeeded);
        /// <summary>
        /// 查询服务状态
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="statusType">状态类型</param>
        /// <param name="lpBuffer">输出缓冲区</param>
        /// <param name="cbBufSize">缓冲区大小</param>
        /// <param name="pcbBytesNeeded">需要字节数</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool QueryServiceStatusEx(IntPtr hService, ServiceStatusType statusType, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_STATUS_PROCESS lpBuffer, int cbBufSize, ref int pcbBytesNeeded);
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="hService">服务句柄</param>
        /// <param name="dwNumServiceArgs">参数</param>
        /// <param name="lpServiceArgVectors">参数</param>
        /// <returns>结果</returns>
        [DllImport("advapi32.dll")]
        public static extern int StartService(IntPtr hService, int dwNumServiceArgs, string lpServiceArgVectors);
        /// <summary>
        /// 解锁服务数据库
        /// </summary>
        /// <param name="hSCManager">控制台句柄</param>
        /// <returns>是否成功</returns>
        [DllImport("advapi32.dll")]
        public static extern bool UnlockServiceDatabase(IntPtr hSCManager);

        /// <summary>
        /// 访问权限类型 [ACCESS_TYPE]
        /// </summary>
        public enum ACCESS_TYPE
        {
            /// <summary>
            /// 所有权限
            /// </summary>
            SERVICE_ALL_ACCESS = 983551,
            /// <summary>
            /// 改变配置
            /// </summary>
            SERVICE_CHANGE_CONFIG = 2,
            /// <summary>
            /// 能呼叫ENUMDEPENDENTSERVICES以依据服务而将所有的服务列举出来
            /// </summary>
            SERVICE_ENUMERATE_DEPENDENTS = 8,
            /// <summary>
            /// 允许立即获取服务状态
            /// </summary>
            SERVICE_INTERROGATE = 128,
            /// <summary>
            /// 暂停/继续
            /// </summary>
            SERVICE_PAUSE_CONTINUE = 64,
            /// <summary>
            /// 查询配置
            /// </summary>
            SERVICE_QUERY_CONFIG = 1,
            /// <summary>
            /// 查询状态
            /// </summary>
            SERVICE_QUERY_STATUS = 4,
            /// <summary>
            /// 启动
            /// </summary>
            SERVICE_START = 16,
            /// <summary>
            /// 停止
            /// </summary>
            SERVICE_STOP = 32,
            /// <summary>
            /// 用户自定义
            /// </summary>
            SERVICE_USER_DEFINED_CONTROL = 256
        }

        /// <summary>
        /// 信息级别 [InfoLevel]
        /// </summary>
        public enum InfoLevel
        {
            /// <summary>
            /// 描述性信息
            /// </summary>
            SERVICE_CONFIG_DESCRIPTION = 1,
            /// <summary>
            /// 错误动作信息
            /// </summary>
            SERVICE_CONFIG_FAILURE_ACTIONS = 2
        }

        /// <summary>
        /// 查询服务配置 [QUERY_SERVICE_CONFIG]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class QUERY_SERVICE_CONFIG
        {
            /// <summary>
            /// 服务类型：独立进程、共享进程、驱动程序还是交互式登录模式
            /// </summary>
            public int dwServiceType;
            /// <summary>
            /// 启动类型：自动、手动还是禁止
            /// </summary>
            public int dwStartType;
            /// <summary>
            /// 服务失败的严重性
            /// </summary>
            public int dwErrorControl;
            /// <summary>
            /// 实现服务代码的二进制文件的路径
            /// </summary>
            public string lpBinaryPathName;
            /// <summary>
            /// 加载顺序组的名称
            /// </summary>
            public string lpLoadOrderGroup;
            /// <summary>
            /// 接受Tag标志码
            /// </summary>
            public int dwTagId;
            /// <summary>
            /// 依赖服务的名称组
            /// </summary>
            public string lpDependencies;
            /// <summary>
            /// 启动服务的帐号
            /// </summary>
            public string lpServiceStartName;
            /// <summary>
            /// 服务友好名称
            /// </summary>
            public string lpDisplayName;
        }

        /// <summary>
        /// 控制台动作 [SC_ACTION]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SC_ACTION
        {
            /// <summary>
            /// 动作类型
            /// </summary>
            public NativeInstaller.SC_ACTION_TYPE SCActionType;
            /// <summary>
            /// 延迟(毫秒)
            /// </summary>
            public int Delay;
        }

        /// <summary>
        /// 控制台动作类型 [SC_ACTION_TYPE]
        /// </summary>
        public enum SC_ACTION_TYPE
        {
            SC_ACTION_NONE,
            SC_ACTION_RESTART,
            SC_ACTION_REBOOT,
            SC_ACTION_RUN_COMMAND
        }

        /// <summary>
        /// 服务描述 [SERVICE_DESCRIPTION]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_DESCRIPTION
        {
            /// <summary>
            /// 描述
            /// </summary>
            public string lpDescription;
        }

        /// <summary>
        /// 错误动作 [SERVICE_FAILURE_ACTIONS]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_FAILURE_ACTIONS
        {
            /// <summary>
            /// 重置失败计数(秒)
            /// </summary>
            public int dwResetPeriod;
            /// <summary>
            /// 重启这前，发送到网络上计算机的信息
            /// </summary>
            public string lpRebootMsg;
            /// <summary>
            /// 命令
            /// </summary>
            public string lpCommand;
            /// <summary>
            /// 动作数量
            /// </summary>
            public int cActions;
            /// <summary>
            /// 动作指针
            /// </summary>
            public int lpsaActions;
        }

        /// <summary>
        /// 服务状态 [SERVICE_STATUS]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_STATUS
        {
            /// <summary>
            /// 服务类型：独立进程、共享进程、驱动程序还是交互式登录模式
            /// </summary>
            public int dwServiceType;
            /// <summary>
            /// 当前状态
            /// </summary>
            public int dwCurrentState;
            /// <summary>
            /// 服务接受那些SCM控制
            /// </summary>
            public int dwControlsAccepted;
            /// <summary>
            /// 退出代码
            /// </summary>
            public int dwWin32ExitCode;
            /// <summary>
            /// 服务详细退出代码
            /// </summary>
            public int dwServiceSpecificExitCode;
            /// <summary>
            /// 周期性报告其进度的递增值
            /// </summary>
            public int dwCheckPoint;
            /// <summary>
            /// 等待估计时间
            /// </summary>
            public int dwWaitHint;
        }

        /// <summary>
        /// 服务进程状态 [SERVICE_STATUS_PROCESS]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_STATUS_PROCESS
        {
            /// <summary>
            /// 服务类型：独立进程、共享进程、驱动程序还是交互式登录模式
            /// </summary>
            public int dwServiceType;
            /// <summary>
            /// 当前状态
            /// </summary>
            public int dwCurrentState;
            /// <summary>
            /// 服务接受那些SCM控制
            /// </summary>
            public int dwControlsAccepted;
            /// <summary>
            /// 退出代码
            /// </summary>
            public int dwWin32ExitCode;
            /// <summary>
            /// 服务详细退出代码
            /// </summary>
            public int dwServiceSpecificExitCode;
            /// <summary>
            /// 周期性报告其进度的递增值
            /// </summary>
            public int dwCheckPoint;
            /// <summary>
            /// 等待估计时间
            /// </summary>
            public int dwWaitHint;
            /// <summary>
            /// 进程标识
            /// </summary>
            public int dwProcessID;
            /// <summary>
            /// 服务标识
            /// </summary>
            public int dwServiceFlags;
        }

        /// <summary>
        /// 服务接受那些SCM控制 [ServiceControlAccepted]
        /// </summary>
        public enum ServiceControlAccepted
        {
            /// <summary>
            /// 改变硬件配置
            /// </summary>
            SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 32,
            /// <summary>
            /// 改变网络绑定
            /// </summary>
            SERVICE_ACCEPT_NETBINDCHANGE = 16,
            /// <summary>
            /// 改变参数
            /// </summary>
            SERVICE_ACCEPT_PARAMCHANGE = 8,
            /// <summary>
            /// 暂停/继续
            /// </summary>
            SERVICE_ACCEPT_PAUSE_CONTINUE = 2,
            /// <summary>
            /// 电源事件
            /// </summary>
            SERVICE_ACCEPT_POWEREVENT = 64,
            /// <summary>
            /// 改变会话
            /// </summary>
            SERVICE_ACCEPT_SESSIONCHANGE = 128,
            /// <summary>
            /// 关闭
            /// </summary>
            SERVICE_ACCEPT_SHUTDOWN = 4,
            /// <summary>
            /// 停止
            /// </summary>
            SERVICE_ACCEPT_STOP = 1
        }

        /// <summary>
        /// 控制台管理类型 [ServiceControlManagerType]
        /// </summary>
        public enum ServiceControlManagerType
        {
            /// <summary>
            /// 所有权限
            /// </summary>
            SC_MANAGER_ALL_ACCESS = 983103,
            /// <summary>
            /// 连接
            /// </summary>
            SC_MANAGER_CONNECT = 1,
            /// <summary>
            /// 创建服务
            /// </summary>
            SC_MANAGER_CREATE_SERVICE = 2,
            /// <summary>
            /// 枚举服务
            /// </summary>
            SC_MANAGER_ENUMERATE_SERVICE = 4,
            /// <summary>
            /// 锁定
            /// </summary>
            SC_MANAGER_LOCK = 8,
            /// <summary>
            /// 修改启动配置
            /// </summary>
            SC_MANAGER_MODIFY_BOOT_CONFIG = 32,
            /// <summary>
            /// 查询服务状态
            /// </summary>
            SC_MANAGER_QUERY_LOCK_STATUS = 16
        }

        /// <summary>
        /// 服务控制类型 [ServiceControlType]
        /// </summary>
        public enum ServiceControlType
        {
            /// <summary>
            /// 继续
            /// </summary>
            SERVICE_CONTROL_CONTINUE = 3,
            /// <summary>
            /// 设备事件
            /// </summary>
            SERVICE_CONTROL_DEVICEEVENT = 11,
            /// <summary>
            /// 改变硬件配置
            /// </summary>
            SERVICE_CONTROL_HARDWAREPROFILECHANGE = 12,
            /// <summary>
            /// 立即获取服务状态
            /// </summary>
            SERVICE_CONTROL_INTERROGATE = 4,
            /// <summary>
            /// 增加网络绑定
            /// </summary>
            SERVICE_CONTROL_NETBINDADD = 7,
            /// <summary>
            /// 禁用网络绑定
            /// </summary>
            SERVICE_CONTROL_NETBINDDISABLE = 10,
            /// <summary>
            /// 启用网络绑定
            /// </summary>
            SERVICE_CONTROL_NETBINDENABLE = 9,
            /// <summary>
            /// 删除网络绑定
            /// </summary>
            SERVICE_CONTROL_NETBINDREMOVE = 8,
            /// <summary>
            /// 改变参数
            /// </summary>
            SERVICE_CONTROL_PARAMCHANGE = 6,
            /// <summary>
            /// 暂停
            /// </summary>
            SERVICE_CONTROL_PAUSE = 2,
            /// <summary>
            /// 电源事件　
            /// </summary>
            SERVICE_CONTROL_POWEREVENT = 13,
            /// <summary>
            /// 改变会话
            /// </summary>
            SERVICE_CONTROL_SESSIONCHANGE = 14,
            /// <summary>
            /// 关闭
            /// </summary>
            SERVICE_CONTROL_SHUTDOWN = 5,
            /// <summary>
            /// 停止
            /// </summary>
            SERVICE_CONTROL_STOP = 1
        }

        /// <summary>
        /// 服务失败的严重性 [ServiceErrorControl]
        /// </summary>
        public enum ServiceErrorControl
        {
            /// <summary>
            /// 安装错误
            /// </summary>
            msidbServiceInstallErrorControlVital = 32768,
            /// <summary>
            /// 危险的
            /// </summary>
            SERVICE_ERROR_CRITICAL = 3,
            /// <summary>
            /// 可忽略的
            /// </summary>
            SERVICE_ERROR_IGNORE = 0,
            /// <summary>
            /// 正常的
            /// </summary>
            SERVICE_ERROR_NORMAL = 1,
            /// <summary>
            /// 严重的
            /// </summary>
            SERVICE_ERROR_SEVERE = 2,
            /// <summary>
            /// 不改变
            /// </summary>
            SERVICEERRORCONTROL_NO_CHANGE = -1
        }

        /// <summary>
        /// 服务启动类型 [ServiceStartType]
        /// </summary>
        public enum ServiceStartType
        {
            /// <summary>
            /// 自动启动
            /// </summary>
            SERVICE_AUTO_START = 2,
            /// <summary>
            /// 计算机启动时加载
            /// </summary>
            SERVICE_BOOT_START = 0,
            /// <summary>
            /// 手动
            /// </summary>
            SERVICE_DEMAND_START = 3,
            /// <summary>
            /// 禁用
            /// </summary>
            SERVICE_DISABLED = 4,
            /// <summary>
            /// 操作系统启动时加载
            /// </summary>
            SERVICE_SYSTEM_START = 1,
            /// <summary>
            /// 无变化
            /// </summary>
            SERVICESTARTTYPE_NO_CHANGE = -1
        }

        /// <summary>
        /// 服务状态 [ServiceState]
        /// </summary>
        public enum ServiceState
        {
            SERVICE_NONE,
            SERVICE_STOPPED,
            SERVICE_START_PENDING,
            SERVICE_STOP_PENDING,
            SERVICE_RUNNING,
            SERVICE_CONTINUE_PENDING,
            SERVICE_PAUSE_PENDING,
            SERVICE_PAUSED
        }

        /// <summary>
        /// 服务状态请求 [ServiceStateRequest]
        /// </summary>
        public enum ServiceStateRequest
        {
            /// <summary>
            /// 激活的状态
            /// </summary>
            SERVICE_ACTIVE = 1,
            /// <summary>
            /// 非激活的状态
            /// </summary>
            SERVICE_INACTIVE = 2,
            /// <summary>
            /// 全部的状态
            /// </summary>
            SERVICE_STATE_ALL = 3
        }

        /// <summary>
        /// 服务状态类型 [ServiceStatusType]
        /// </summary>
        public enum ServiceStatusType
        {
            SC_STATUS_PROCESS_INFO
        }

        /// <summary>
        /// 服务类型 [ServiceType]
        /// </summary>
        public enum ServiceType
        {
            /// <summary>
            /// 文件系统驱动
            /// </summary>
            SERVICE_FILE_SYSTEM_DRIVER = 2,
            /// <summary>
            /// 交互式登录进程
            /// </summary>
            SERVICE_INTERACTIVE_PROCESS = 256,
            /// <summary>
            /// 内核驱动
            /// </summary>
            SERVICE_KERNEL_DRIVER = 1,
            /// <summary>
            /// WIN32独立进程
            /// </summary>
            SERVICE_WIN32_OWN_PROCESS = 16,
            /// <summary>
            /// WIN32共享进程
            /// </summary>
            SERVICE_WIN32_SHARE_PROCESS = 32,
            /// <summary>
            /// 不改变
            /// </summary>
            SERVICETYPE_NO_CHANGE = -1
        }
    }

}
