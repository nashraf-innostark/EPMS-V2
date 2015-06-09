using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class TIRService : ITIRService
    {
        private readonly ITIRRepository repository;
        private readonly ITIRItemRepository itemRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemVariationRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public TIRService(IItemVariationRepository itemVariationRepository, IAspNetUserRepository aspNetUserRepository, ITIRRepository repository, ITIRItemRepository itemRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.repository = repository;
            this.itemRepository = itemRepository;
        }

        #endregion
        public TIRCreateResponse LoadTirResponseData(long? id)
        {

            TIRCreateResponse response = new TIRCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id == null) return response;

            var tir = repository.Find((long)id);
            if (tir == null) return response;

            response.Tir = tir;
            var employee = aspNetUserRepository.Find(tir.RecCreatedBy).Employee;
            response.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
            response.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";
            
            return response;
        }

        public TIRListResponse GetAllTirs(TransferItemSearchRequest searchRequest)
        {
            return repository.GetAllTirs(searchRequest);
        }

        public bool SaveDIF(TIR tir)
        {
            if (tir.Id > 0)
            {
                //update
                if (UpdateTIR(tir))//if RFI updated, then update the items
                    TirItemUpdation(tir);
            }
            else
            {
                //save
                AddTIR(tir);
            }
            return true;
        }
        public bool UpdateTIR(TIR tir)
        {
            try
            {
                repository.Update(tir);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddTIR(TIR tir)
        {
            try
            {
                repository.Add(tir);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void DeleteTIR(TIR tir)
        {
            repository.Add(tir);
            repository.SaveChanges();
        }

        private void TirItemUpdation(TIR tir)
        {
            var itemsInDb = itemRepository.GetTirItemsById(tir.Id).ToList();

            foreach (var item in tir.TIRItems)
            {
                if (item.ItemId > 0)
                {
                    //update
                    var itemInDb = itemsInDb.Where(x => x.ItemId == item.ItemId);
                    if (itemInDb != null)
                    {
                        itemRepository.Update(item);
                        itemRepository.SaveChanges();
                        itemsInDb.RemoveAll(x => x.ItemId == item.ItemId);
                    }
                }
                else
                {
                    //save
                    itemRepository.Add(item);
                    itemRepository.SaveChanges();
                }
            }
            DeleteTirItem(itemsInDb);
        }
        private void DeleteTirItem(IEnumerable<TIRItem> itemsInDb)
        {
            foreach (var item in itemsInDb)
            {
                itemRepository.Delete(item);
                itemRepository.SaveChanges();
            }
        }
    }
}
