using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Aton.ServiceHost.ServiceConfigHandler
{
    public class AtonServiceSummary : ConfigurationElement
    {
        public AtonServiceSummary() { }
        public AtonServiceSummary(string serviceName, string serviceDesc)
        {
            base["ServiceName"] = serviceName;
            base["ServiceDesc"] = serviceDesc;
        }

        [ConfigurationProperty("ServiceName")]
        public string ServiceName
        {
            get { return base["ServiceName"].ToString(); }
            set { base["ServiceName"] = value; }
        }

        [ConfigurationProperty("ServiceDesc")]
        public string ServiceDesc
        {
            get { return  base["ServiceDesc"].ToString(); }
            set { base["ServiceDesc"] = value; }
        }

        public NameValueCollection Options { get; private set; }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            if (Options == null)
            {
                Options = new NameValueCollection();
            }

            Options.Add(name, value);
            return true;
        }
    }
}
