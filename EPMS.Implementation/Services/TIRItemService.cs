using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class TIRItemService : ITIRItemService
    {
        private readonly ITIRItemRepository repository;

        public TIRItemService(ITIRItemRepository repository)
        {
            this.repository = repository;
        }

        public bool AddTIRItem(TIRItem tirItem)
        {
            try
            {
                repository.Add(tirItem);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTIRItem(TIRItem tirItem)
        {
            try
            {
                repository.Update(tirItem);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteTIRItem(TIRItem tirItem)
        {
            repository.Delete(tirItem);
            repository.SaveChanges();
        }
    }
}
