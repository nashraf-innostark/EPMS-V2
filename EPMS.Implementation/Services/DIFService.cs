﻿using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class DIFService:IDIFService
    {
        private readonly IDIFRepository repository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IDIFItemRepository itemRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="itemVariationRepository"></param>
        /// <param name="itemRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public DIFService(IDIFRepository repository, IItemVariationRepository itemVariationRepository, IDIFItemRepository itemRepository,IAspNetUserRepository aspNetUserRepository)
        {
            this.repository = repository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemRepository = itemRepository;
            this.aspNetUserRepository = aspNetUserRepository;
        }

        #endregion
        public IEnumerable<DIF> GetAll()
        {
            return repository.GetAll();
        }

        public DifRequestResponse LoadAllDifs(DifSearchRequest searchRequest)
        {
            var difs = repository.LoadAllDifs(searchRequest);
            return difs;
        }

        public DIF FindDIFById(long id)
        {
            return repository.Find(id);
        }
        public bool SaveDIF(DIF dif)
        {
            if (dif.Id > 0)
            {
                //update
                if(UpdateDIF(dif))//if RFI updated, then update the items
                    DifItemUpdation(dif);
            }
            else
            {
                //save
                AddDIF(dif);
            }
            return true;
        }

        private void DifItemUpdation(DIF dif)
        {
            var itemsInDb = itemRepository.GetDifItemsById(dif.Id).ToList();

            foreach (var item in dif.DIFItems)
            {
                if (item.ItemId > 0)
                {
                    //update
                    var itemInDb = itemsInDb.Where(x => x.ItemId == item.ItemId);
                    if (itemInDb != null)
                    {
                        itemRepository.Update(item.CreateRfiItem());
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
            DeleteDifItem(itemsInDb);
        }

        private void DeleteDifItem(IEnumerable<DIFItem> itemsInDb)
        {
            foreach (var item in itemsInDb)
            {
                itemRepository.Delete(item);
                itemRepository.SaveChanges();
            }
        }

        public bool AddDIF(DIF dif)
        {
            repository.Add(dif);
            repository.SaveChanges();
            return true;
        }

        public bool UpdateDIF(DIF dif)
        {
            repository.Update(dif);
            repository.SaveChanges();
            return true;
        }

        public void DeleteDIF(DIF dif)
        {
            repository.Delete(dif);
            repository.SaveChanges();
        }

        public DifCreateResponse LoadDifResponseData(long? id)
        {
            DifCreateResponse response=new DifCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList()
            };
            if (id == null) return response;

            var dif = repository.Find((long)id);
            if (dif == null) return response;

            response.Dif = dif;
            var employee = aspNetUserRepository.Find(dif.RecCreatedBy).Employee;
            response.RequesterNameE = employee != null ? employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE : "";
            response.RequesterNameA = employee != null ? employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA : "";

            if (dif.Status != 6 && !string.IsNullOrEmpty(dif.ManagerId))
            {
                var manager = aspNetUserRepository.Find(dif.ManagerId).Employee;
                response.ManagerNameE = manager != null ? manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " + manager.EmployeeLastNameE : "";
                response.ManagerNameA = manager != null ? manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " + manager.EmployeeLastNameA : "";

            }
            response.DifItem = dif.DIFItems;
            return response;
        }
    }
}