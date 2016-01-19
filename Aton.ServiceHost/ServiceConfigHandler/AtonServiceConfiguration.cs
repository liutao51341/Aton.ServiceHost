using System.Configuration;

namespace Aton.ServiceHost.ServiceConfigHandler
{
    class AtonServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("AtonServices")]
        public AtonServiceCollection AtonServices
        {
            get
            {
                return this["AtonServices"] as AtonServiceCollection;
            }
        }

        [ConfigurationProperty("AtonServiceSummary")]
        public AtonServiceSummary AtonServiceSummary
        {
            get
            {
                return this["AtonServiceSummary"] as AtonServiceSummary;
            }
        }
    }
}
