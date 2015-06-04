using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IVendorItemsRepository vendorItemsRepository;

        public VendorService(IVendorRepository vendorRepository, IVendorItemsRepository vendorItemsRepository)
        {
            this.vendorRepository = vendorRepository;
            this.vendorItemsRepository = vendorItemsRepository;
        }

        public IEnumerable<Vendor> GetAll()
        {
            return vendorRepository.GetAll();
        }

        public Vendor FindVendorById(long id)
        {
            return vendorRepository.Find((int)id);            
        }

        public bool AddVendor(Vendor vendor)
        {
            if (vendorRepository.vendorExists(vendor))
            {
                throw new InvalidOperationException("Vendor with the same name Already Exists");
            }
            vendorRepository.Add(vendor);
            vendorRepository.SaveChanges();
            return true;
        }

        public bool UpdateVendor(Vendor vendor)
        {
            if (vendorRepository.vendorExists(vendor))
            {
                throw new InvalidOperationException("Vendor with the same name Already Exists");
            }
            vendorRepository.Update(vendor);
            vendorRepository.SaveChanges();
            return true;
        }

        public void DeleteVendor(Vendor vendor)
        {
            vendorRepository.Delete(vendor);
            vendorRepository.SaveChanges();
        }

        public SaveVendorResponse SaveVendor(VendorRequest vendorToSave)
        {
            #region Add

            if (vendorToSave.Vendor.VendorId < 1)
            {
                SaveNewVendor(vendorToSave.Vendor);
                SaveVendorItems(vendorToSave);
            }

                #endregion

            #region Update

            else
            {
                UpdateExistingVendor(vendorToSave.Vendor);
                UpdateVendorItems(vendorToSave);
            }

            #endregion

            return new SaveVendorResponse();
        }

        private void SaveNewVendor(Vendor vendor)
        {
            vendor.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            vendor.RecCreatedDt = DateTime.Now;
            vendor.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            vendor.RecLastUpdatedDt = DateTime.Now;
            vendorRepository.Add(vendor);
            vendorRepository.SaveChanges();
        }

        private void UpdateExistingVendor(Vendor vendor)
        {
            vendor.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            vendor.RecLastUpdatedDt = DateTime.Now;
            vendorRepository.Update(vendor);
            vendorRepository.SaveChanges();
        }

        private void SaveVendorItems(VendorRequest itemsToSave)
        {
            if (itemsToSave.VendorItems != null)
            {
                foreach (var vendorItem in itemsToSave.VendorItems)
                {
                    VendorItem item = new VendorItem
                    {
                        ItemId = vendorItem.ItemId,
                        ItemDetails = vendorItem.ItemDetails,
                        VendorId = itemsToSave.Vendor.VendorId
                    };
                    vendorItemsRepository.Add(item);
                }
                vendorItemsRepository.SaveChanges();
            }
        }

        private void UpdateVendorItems(VendorRequest itemsToSave)
        {
            IEnumerable<VendorItem> dbItemsList = vendorItemsRepository.GetItemsByVendorId(itemsToSave.Vendor.VendorId).ToList();
            IEnumerable<VendorItem> clientItemsList = itemsToSave.VendorItems;

            //If Client List is not Empty
            if (clientItemsList != null)
            {
                //Add New Items
                foreach (VendorItem vendorItem in clientItemsList)
                {
                    if (dbItemsList.Any(a => a.ItemId == vendorItem.ItemId))
                        continue;
                    VendorItem item = new VendorItem
                    {
                        ItemId = vendorItem.ItemId,
                        ItemDetails = vendorItem.ItemDetails,
                        VendorId = itemsToSave.Vendor.VendorId
                    };
                    vendorItemsRepository.Add(item);
                }

                //Delete Items
                foreach (VendorItem vendorItem in dbItemsList)
                {
                    if (clientItemsList.Any(x => x.ItemId == vendorItem.ItemId))
                        continue;
                    var itemToDelete = vendorItemsRepository.Find(vendorItem.ItemId);
                    vendorItemsRepository.Delete(itemToDelete);
                }
            }
            else
            {
                //Delete All Items if List from Client is Empty
                foreach (VendorItem vendorItem in dbItemsList)
                {
                    var itemToDelete = vendorItemsRepository.Find(vendorItem.ItemId);
                    vendorItemsRepository.Delete(itemToDelete);
                }
            }
            vendorRepository.SaveChanges();
        }
    }
}
