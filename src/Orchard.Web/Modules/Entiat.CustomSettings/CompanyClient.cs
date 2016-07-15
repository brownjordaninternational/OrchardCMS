using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class CompanyClient : ClientBase<ICompanyService>, ICompanyService
    {
        public IEnumerable<Company> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Company Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Company> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
