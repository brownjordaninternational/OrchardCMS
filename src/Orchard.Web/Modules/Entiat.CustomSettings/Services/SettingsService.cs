using System;
using Orchard;
using Orchard.Widgets.Services;
using Orchard.Mvc;
using Orchard.Environment.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiat.CustomSettings.Models;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Bj.Essentials.Entities;
using Bj.Essentials.Proxies;

namespace Entiat.CustomSettings.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService() { }

        public IEnumerable<Channel> GetAllChannels(string company)
        {
            IEnumerable<Channel> channels = new List<Channel>();

            using(ChannelClient proxy = new ChannelClient())
            {
                return proxy.GetAll(company);
            }
        }
        public IEnumerable<Company> GetAllCompanies()
        {
            IEnumerable<Company> companies = new List<Company>();
            using(CompanyClient proxy = new CompanyClient())
            {
                return proxy.GetAll();
            }
        }
        
    }

    //Custom Rule Settings
    //if you use function 'urlregex' when setting up layer rules, this will allow for any regex checks on urls
    public class CustomURLRuleProvider: IRuleProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShellSettings _shellSettings;

        public CustomURLRuleProvider(IHttpContextAccessor httpContextAccessor, ShellSettings shellSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _shellSettings = shellSettings;
        }
        public void Process(RuleContext ruleContext)
        {
            if (!String.Equals(ruleContext.FunctionName, "urlregex", StringComparison.OrdinalIgnoreCase))
                return;

            var context = _httpContextAccessor.Current();
            var url = Convert.ToString(ruleContext.Arguments[0]);

            if (url.StartsWith("~/"))
            {
                url = url.Substring(2);

                string[] urlParts = url.Split('/');
                var requestPath = context.Request.Path;
                if(requestPath.StartsWith("/"))
                {
                    requestPath = requestPath.Substring(1);
                }
                string[] requestPathParts = requestPath.Split('/');
                if(urlParts.Length < requestPathParts.Length)
                {
                    ruleContext.Result = false;
                    return;
                }
                for (int i = 0; i < urlParts.Length; i++) {
                    if(urlParts[i].StartsWith("["))
                    {
                        Regex rgx = new Regex(urlParts[i].ToString());
                        if (!rgx.IsMatch(requestPathParts[i]))
                        {
                            ruleContext.Result = false;
                            return;
                        }
                    }
                    else if(!urlParts[i].ToString().Equals(requestPathParts[i].ToString()))
                    {
                        ruleContext.Result = false;
                        return;
                    }
                }
                
            }
            ruleContext.Result = true;
        }
    }
}
