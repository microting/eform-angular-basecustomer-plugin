using System.Reflection;
using Customers.Pn.Abstractions;
using Microsoft.Extensions.Localization;

namespace Customers.Pn.Services
{
    public class CustomersLocalizationService : ICustomersLocalizationService
    {
        private readonly IStringLocalizer _localizer;
 
        public CustomersLocalizationService(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("CustomersResources",
                Assembly.GetEntryAssembly().FullName);
        }
 
        public string GetString(string key)
        {
            var str = _localizer[key];
            return str.Value;
        }

        public string GetString(string format, params object[] args)
        {
            var message = _localizer[format];
            if (message?.Value == null)
            {
                return null;
            }

            return string.Format(message.Value, args);
        }
    }
}
