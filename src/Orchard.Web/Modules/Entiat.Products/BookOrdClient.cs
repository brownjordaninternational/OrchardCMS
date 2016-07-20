using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class BookOrdClient : ClientBase<IBookOrdService>, IBookOrdService
    {
        public IEnumerable<BookOrd> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public BookOrd Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<BookOrd> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public BookOrd GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateBookOrd(BookOrd entity)
        {
            return Channel.UpdateBookOrd(entity);
        }
        public long AddBookOrd(BookOrd entity)
        {
            return Channel.AddBookOrd(entity);
        }
    }
}
