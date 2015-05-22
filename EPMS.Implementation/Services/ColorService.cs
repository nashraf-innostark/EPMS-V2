using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class ColorService : IColorService
    {
        private readonly IColorRepository colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public IEnumerable<Color> GetAll()
        {
            return colorRepository.GetAll();
        }

        public Color FindColorById(long id)
        {
            return colorRepository.Find(id);
        }

        public bool AddColor(Color color)
        {
            if (colorRepository.ColorExists(color))
            {
                throw new ArgumentException("Color already exists");
            }
            colorRepository.Add(color);
            colorRepository.SaveChanges();
            return true;
        }

        public bool UpdateColor(Color color)
        {
            if (colorRepository.ColorExists(color))
            {
                throw new ArgumentException("Color already exists");
            }
            colorRepository.Update(color);
            colorRepository.SaveChanges();
            return true;
        }

        public void DeleteColor(Color color)
        {
            colorRepository.Delete(color);
            colorRepository.SaveChanges();
        }
    }
}
