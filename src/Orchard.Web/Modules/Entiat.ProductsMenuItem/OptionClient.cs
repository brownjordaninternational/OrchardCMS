using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class OptionClient : ClientBase<IOptionService>, IOptionService
    {
        public IEnumerable<Option> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Option Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Option> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Option GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateOption(Option entity)
        {
            return Channel.UpdateOption(entity);
        }
        public long AddOption(Option entity)
        {
            return Channel.AddOption(entity);
        }
    }
}
