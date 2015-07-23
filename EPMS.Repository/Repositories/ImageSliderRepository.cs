using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ImageSliderRepository : BaseRepository<ImageSlider>, IImageSliderRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ImageSliderRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ImageSlider> DbSet
        {
            get { return db.ImageSliders; }
        }

        #endregion

        #region Public

        #endregion
    }
}
