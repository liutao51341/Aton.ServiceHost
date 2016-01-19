using System.Collections.Generic;
using System.Configuration;

namespace Aton.ServiceHost.ServiceConfigHandler
{
    [ConfigurationCollection(typeof(AtonService), AddItemName = "AtonService")]
     class AtonServiceCollection : ConfigurationElementCollection, IEnumerable<AtonService>
    {
        public AtonService this[int index]
        {
            get
            {
                return (AtonService)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AtonService();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AtonService)element).Name;
        }

        public new IEnumerator<AtonService> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return (AtonService)base.BaseGet(i);
            }  
        }

        public IEnumerable<AtonServiceConfig> ToServiceConfigArray()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                AtonService service= (AtonService)base.BaseGet(i);
                yield return new AtonServiceConfig(service.Name,service.Desc, service.Type);
            }  
        }
    }
}
