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
    public class TIRService : ITIRService
    {
        private readonly ITIRRepository repository;
        private readonly ITIRHistoryRepository historyRepository;
        private readonly ITIRItemRepository itemRepository;
        private readonly IItemVariationRepository itemVariationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemVariationRepository"></param>
        /// <param name="aspNetUserRepository"></param>
        public TIRService(IItemVariationRepository itemVariationRepository, IAspNetUserRepository aspNetUserRepository, ITIRRepository repository, ITIRItemRepository itemRepository, ITIRHistoryRepository historyRepository)
        {
            this.itemVariationRepository = itemVariationRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.repository = repository;
            this.itemRepository = itemRepository;
            this.historyRepository = historyRepository;
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

        public TirHistoryResponse GetTirHistoryData(long? id)
        {
            if (id == null)
            {
                return new TirHistoryResponse();
            }
            var tirs = historyRepository.GetTirHistoryData((long)id);
            var tirList = tirs as IList<TIRHistory> ?? tirs.ToList();
            
            TirHistoryResponse response = new TirHistoryResponse
            {
                Tirs = tirList.Select(x => x.CreateFromTirHistoryToTir()),
                RecentTir = repository.Find((long) id)
            };
            response.TirItems = response.RecentTir.TIRItems;
            if (response.RecentTir != null)
            {
                if (!string.IsNullOrEmpty(response.RecentTir.ManagerId))
                {
                    var manager = aspNetUserRepository.Find(response.RecentTir.ManagerId).Employee;
                    response.ManagerNameEn = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " +
                                           manager.EmployeeLastNameE;
                    response.ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " +
                                           manager.EmployeeLastNameA;
                }
                if (response.RecentTir.AspNetUser.Employee != null)
                {
                    var employee = response.RecentTir.AspNetUser.Employee;
                    response.RequesterNameEn = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " +
                                           employee.EmployeeLastNameE;
                    response.RequesterNameAr = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " +
                                           employee.EmployeeLastNameA;
                }
            }
            return response;
        }

        public TIR FindTirById(long id, string from)
        {
            TIR tir;
            if (from == "History")
            {
                tir = historyRepository.Find(id).CreateFromTirHistoryToTir();
            }
            else
            {
                tir = repository.Find(id);
            }
            return tir;
        }

        public bool UpdateTirStatus(TransferItemStatus status)
        {
            try
            {
                var tir = repository.Find(status.Id);
                tir.NotesE = status.NotesEn;
                tir.NotesA = status.NotesAr;
                tir.ManagerId = status.ManagerId;
                if (tir.Status != status.Status && status.Status != 3)
                {
                    tir.Status = status.Status;
                    var tirHistoryToAdd = tir.CreateFromTirToTirHistory();
                    historyRepository.Add(tirHistoryToAdd);
                    historyRepository.SaveChanges();
                }
                repository.Update(tir);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SavePO(TIR tir)
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
                return AddTIR(tir);
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

        public IEnumerable<TIR> GetRecentTIRs(int status, string requester, DateTime date)
        {
            return repository.GetRecentTIRs(status, requester, date);
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
