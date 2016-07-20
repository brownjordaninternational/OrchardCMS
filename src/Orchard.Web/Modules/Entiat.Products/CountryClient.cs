using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class CountryClient : ClientBase<ICountryService>, ICountryService
    {
        public IEnumerable<Country> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Country Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Country> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Country GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateCountry(Country entity)
        {
            return Channel.UpdateCountry(entity);
        }
        public long AddCountry(Country entity)
        {
            return Channel.AddCountry(entity);
        }
    }
}
