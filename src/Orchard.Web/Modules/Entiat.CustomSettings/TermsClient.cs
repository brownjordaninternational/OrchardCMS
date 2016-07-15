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
    }
}
