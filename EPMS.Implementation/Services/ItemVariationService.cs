using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    class ItemVariationService : IItemVariationService
    {
        #region Private
        private readonly IItemVariationRepository variationRepository;
        private readonly ISizeRepository sizeRepository;
        #endregion

        #region Constructor
        public ItemVariationService(IItemVariationRepository variationRepository, ISizeRepository sizeRepository)
        {
            this.variationRepository = variationRepository;
            this.sizeRepository = sizeRepository;
        }
        #endregion

        #region Public
        public IEnumerable<ItemVariation> GetAll()
        {
            return variationRepository.GetAll();
        }

        public ItemVariation FindVariationById(long id)
        {
            return variationRepository.Find(id);
        }

        public bool AddVariation(ItemVariation itemVariation)
        {
            variationRepository.Add(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public bool UpdateVariation(ItemVariation itemVariation)
        {
            variationRepository.Update(itemVariation);
            variationRepository.SaveChanges();
            return true;
        }

        public void DeleteVartiation(ItemVariation itemVariation)
        {
            variationRepository.Delete(itemVariation);
            variationRepository.SaveChanges();
        }

        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList()
        {
            return variationRepository.GetItemVariationDropDownList();
        }
        /// <summary>
        /// Save Item Variation from Client
        /// </summary>
        public ItemVariationResponse SaveItemVariation(ItemVariationRequest variationToSave)
        {
            ItemVariation itemVariationFromDatabase = variationRepository.Find(variationToSave.ItemVariation.ItemVariationId);
            if (variationToSave.ItemVariation.ItemVariationId > 0)
            {
                UpdateItemVariation(variationToSave.ItemVariation);
                UpdateSizeList(variationToSave, itemVariationFromDatabase);
            }
            else
            {
                AddNewVariation(variationToSave.ItemVariation);
                AddSizeList(variationToSave);
            }
            return new ItemVariationResponse();
        }
        /// <summary>
        /// Add New Variation from Client
        /// </summary>
        private void AddNewVariation(ItemVariation itemVariation)
        {
            itemVariation.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecCreatedDt = DateTime.Now;
            itemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecLastUpdatedDt = DateTime.Now;
            variationRepository.Add(itemVariation);
        }
        /// <summary>
        /// Update Existing Variation from Client
        /// </summary>
        private void UpdateItemVariation(ItemVariation itemVariation)
        {
            itemVariation.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            itemVariation.RecLastUpdatedDt = DateTime.Now;
            variationRepository.Update(itemVariation);
            variationRepository.SaveChanges();
        }
        /// <summary>
        /// Save Size List from Client
        /// </summary>
        private void AddSizeList(ItemVariationRequest variationToSave)
        {
            string[] sizeList = variationToSave.SizeArrayList.Split(',');
            foreach (string item in sizeList)
            {
                Size sizeToAdd = sizeRepository.Find(Convert.ToInt64(item));
                if(sizeToAdd == null)
                    throw new Exception("Size not found in database");
                if (variationToSave.ItemVariation.Sizes == null)
                {
                    variationToSave.ItemVariation.Sizes = new Collection<Size>();
                }
                variationToSave.ItemVariation.Sizes.Add(sizeToAdd);
            }
            variationRepository.SaveChanges();
        }
        /// <summary>
        /// Update Client List from Client
        /// </summary>
        private void UpdateSizeList(ItemVariationRequest variationToSave, ItemVariation itemVariationFromDatabase)
        {
            List<Size> dbList = itemVariationFromDatabase.Sizes.ToList();
            string[] clientList = variationToSave.SizeArrayList.Split(',');

            //Add New Items from Clientlist to Database

            foreach (string item in clientList)
            {
                Size sizeToAdd = sizeRepository.Find(Convert.ToInt64(item));
                if (dbList.Any(a => a.SizeId == sizeToAdd.SizeId))
                    continue;
                if (variationToSave.ItemVariation.Sizes == null)
                {
                    variationToSave.ItemVariation.Sizes = new Collection<Size>();
                }
                variationToSave.ItemVariation.Sizes.Add(sizeToAdd);
            }
            variationRepository.SaveChanges();

            //Remove Items from Database that are not in Clientlist


            //Remove All Items from Database if Clientlist is Empty

        }
        #endregion
    }
}
