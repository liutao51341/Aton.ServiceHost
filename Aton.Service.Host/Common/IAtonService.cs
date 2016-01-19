using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aton.ServiceHost.Common
{
    /// <summary>
    /// 服务接口
    /// </summary>
    public interface IAtonService
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        bool StartService();
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        bool StopService();
    }
}
