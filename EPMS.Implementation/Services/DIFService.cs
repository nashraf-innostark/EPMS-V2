using System;
using System.Collections.Generic;
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
        private readonly IDIFHistoryRepository historyRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IDIFItemRepository itemRepository;
        private readonly IDIFItemHistoryRepository itemHistoryRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="itemVariationRepository"></param>
        /// <param name="itemRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public DIFService(IDIFRepository repository, IItemVariationRepository itemVariationRepository, IDIFItemRepository itemRepository,IAspNetUserRepository aspNetUserRepository, IDIFHistoryRepository historyRepository, IDIFItemHistoryRepository itemHistoryRepository)
        {
            this.repository = repository;
            this.itemVariationRepository = itemVariationRepository;
            this.itemRepository = itemRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.historyRepository = historyRepository;
            this.itemHistoryRepository = itemHistoryRepository;
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

        public DifHistoryResponse GetDifHistoryData(long? parentId)
        {
            if (parentId == null)
            {
                return new DifHistoryResponse();
            }
            var difs = historyRepository.GetDifHistoryData((long)parentId);
            var difList = difs as IList<DIFHistory> ?? difs.ToList();
            
            DifHistoryResponse response = new DifHistoryResponse
            {
                Difs = difList.Select(x => x.CreateFromDifHistoryToDif()),
                RecentDif = repository.Find((long) parentId)
            };
            response.DifItems = response.RecentDif.DIFItems;
            //response.DifItems = difItems.Select(x => x.CreateFromDifItemHistoryToDifItem());
            if (response.RecentDif != null)
            {
                if (!string.IsNullOrEmpty(response.RecentDif.ManagerId))
                {
                    var manager = aspNetUserRepository.Find(response.RecentDif.ManagerId).Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                if (response.RecentDif.AspNetUser.Employee != null)
                {
                    var employee = response.RecentDif.AspNetUser.Employee;
                    response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                           employee.EmployeeLastNameE;
                    response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                           employee.EmployeeLastNameA;
                }
            }
            return response;
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
            var previous = repository.Find(dif.Id);
            if (previous.Status != dif.Status && dif.Status != 6)
            {
                var difHistoryToAdd = dif.CreateFromDifToDifHistory(previous.DIFItems);
                difHistoryToAdd.ManagerId = dif.ManagerId;
                historyRepository.Add(difHistoryToAdd);
                historyRepository.SaveChanges();
            }
            repository.Update(dif);
            repository.SaveChanges();
            return true;
        }

        public void DeleteDIF(DIF dif)
        {
            repository.Delete(dif);
            repository.SaveChanges();
        }

        public DifCreateResponse LoadDifResponseData(long? id, string from)
        {
            DifCreateResponse response=new DifCreateResponse
            {
                ItemVariationDropDownList = itemVariationRepository.GetItemVariationDropDownList(),
                LastFormNumber = repository.GetLastFormNumber()
            };
            if (id == null) return response;

            DIF dif = null;
            if (from == "History")
            {
                dif = historyRepository.Find((long)id).CreateFromDifHistoryToDif();
            }
            else
            {
                dif = repository.Find((long)id);
            }
            if (dif == null) return response;

            response.Dif = dif;
            var employee = aspNetUserRepository.Find(dif.RecCreatedBy).Employee;
            if (employee != null)
            {
                response.RequesterNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE;
                response.RequesterNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA;
                response.EmpJobId = employee.EmployeeJobId;
            }
            

            if (dif.Status != 6 && !string.IsNullOrEmpty(dif.ManagerId))
            {
                var manager = aspNetUserRepository.Find(dif.ManagerId).Employee;
                response.ManagerNameE = manager != null ? manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " + manager.EmployeeLastNameE : "";
                response.ManagerNameA = manager != null ? manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " + manager.EmployeeLastNameA : "";

            }
            response.DifItem = dif.DIFItems;
            return response;
        }

        public IEnumerable<DIF> GetRecentDIFs(int status, string requester, DateTime date)
        {
            return repository.GetRecentDIFs(status, requester, date);
        }
    }
}
