using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class HoldReasonClient : ClientBase<IHoldReasonService>, IHoldReasonService
    {
        public IEnumerable<HoldReason> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public HoldReason Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<HoldReason> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
