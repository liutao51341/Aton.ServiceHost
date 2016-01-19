using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Aton.ServiceHost.ServiceConfigHandler
{
    /// <summary>
    /// 服务配置信息
    /// </summary>
    public class AtonServiceConfig
    {
        public AtonServiceConfig(string name, string desc, string type)
        {
            Desc = desc;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类/路径名
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
 
    public class AtonServiceConfigHandler : List<AtonServiceConfig>
    {
        /// <summary>
        /// 服务概览
        /// </summary>
        public AtonServiceSummary ServiceSummary
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AtonServiceConfigHandler(string sectionName = "AtonServiceHost")
        {
            var serverConfig = ConfigurationManager.GetSection(sectionName) as AtonServiceConfiguration;
            AddRange(serverConfig.AtonServices.ToServiceConfigArray());
            ServiceSummary = serverConfig.AtonServiceSummary;
        }
    }
}
