using Bj.Essentials.Entities;
using Entiat.CustomSettings.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiat.CustomSettings.Services
{
    public interface ISettingsService : IDependency
    {   
        /// <summary>
        /// Gets a list of all Channels
        /// </summary>
        /// <returns></returns>
        IEnumerable<Channel> GetAllChannels(string company);
        IEnumerable<Company> GetAllCompanies();
    }
}
