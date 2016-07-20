using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class TermsClient : ClientBase<ITermsService>, ITermsService
    {
        public IEnumerable<Terms> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Terms Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Terms> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Terms GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateTerms(Terms entity)
        {
            return Channel.UpdateTerms(entity);
        }
        public long AddTerms(Terms entity)
        {
            return Channel.AddTerms(entity);
        }
		public bool DeleteTerms(Terms entity)
        {
            return Channel.DeleteTerms(entity);
        }
    }
}
