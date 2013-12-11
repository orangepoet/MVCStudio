using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Lib.Mvc4App.Models;

namespace Lib.Mvc4App.Services {

    public interface IContractService : IBaseService<Contract> {
        Contract GetNext();
    }

    public class ContractService : BaseService, IContractService {
        public IList<Contract> GetList(Func<Contract, bool> filters) {
            if (filters != null)
                return db.Contracts.Where(filters).ToList();
            else
                return db.Contracts.ToList();
        }

		public Contract Find(params object[] keys) {
            return db.Contracts.Find(keys);
        }

        public void Add(Contract model) {
            db.Contracts.Add(model);
        }

        public void Update(Contract model) {
            db.Entry(model).State = EntityState.Modified;
        }

        public void Delete(Contract model) {
            db.Contracts.Remove(model);
        }

		public void Save() {
			db.SaveChanges();
		}

        public void Dispose() {
            db.Dispose();
        }
        
        public Contract GetNext() {
            int id = 1;
            if (db.Contracts.Count() > 0)
                id = db.Contracts.Max(c => c.Id) + 1;
            return new Contract { Id = id };
        }       
    }
}
