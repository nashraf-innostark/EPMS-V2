﻿using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ImageSliderService : IImageSliderService
    {
        #region Private

        private readonly IImageSliderRepository repository;
        private readonly IPartnerRepository partnerRepository;
        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ImageSliderService(IImageSliderRepository repository, IPartnerRepository partnerRepository)
        {
            this.repository = repository;
            this.partnerRepository = partnerRepository;
        }

        #endregion

        public HomePageResponse GetHomePageResponse()
        {
            HomePageResponse response = new HomePageResponse
            {
                ImageSlider = repository.GetAll(),
                Partners = partnerRepository.GetAll()
            };
            return response;
        }

        public IEnumerable<ImageSlider> GetAll()
        {
            return repository.GetAll();
        }

        public ImageSlider FindImageSliderById(long id)
        {
            return repository.Find(id);
        }

        public bool AddImageSlider(ImageSlider imageSlider)
        {
            try
            {
                repository.Add(imageSlider);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateImageSlider(ImageSlider imageSlider)
        {
            try
            {
                repository.Update(imageSlider);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteImageSlider(long id)
        {
            var dataToDelete = repository.Find(id);
            repository.Delete(dataToDelete);
            repository.SaveChanges();
        }
    }
}
