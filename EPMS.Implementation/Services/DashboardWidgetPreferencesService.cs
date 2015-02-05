using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class DashboardWidgetPreferencesService : IDashboardWidgetPreferencesService
    {
        private readonly IDashboardWidgetPreferencesRepository Repository;
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public DashboardWidgetPreferencesService(IDashboardWidgetPreferencesRepository repository)
        {
            Repository = repository;
        }

        #endregion

        public DashboardWidgetPreferences LoadPreferencesByUserId(string userId)
        {
            return Repository.LoadPreferencesByUserId(userId);
        }

        public IEnumerable<DashboardWidgetPreferences> LoadAllPreferencesByUserId(string userId)
        {
            return Repository.LoadAllPreferencesByUserId(userId);
        }
        public bool Addpreferences(DashboardWidgetPreferences preferences)
        {
            try
            {
                Repository.Add(preferences);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updatepreferences(DashboardWidgetPreferences preferences)
        {
            try
            {
                Repository.Update(preferences);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Deletepreferences(DashboardWidgetPreferences preferences)
        {
            try
            {
                Repository.Delete(preferences);
                Repository.SaveChanges();
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }
}
