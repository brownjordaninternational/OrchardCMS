using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class OptionGroupClient : ClientBase<IOptionGroupService>, IOptionGroupService
    {
        public IEnumerable<OptionGroup> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public OptionGroup Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<OptionGroup> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
