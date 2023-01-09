using AngularEshop.Core.Services.Interfaces;
using AngularEshop.DataLayer.Entities.Site;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region constructor
        private IGenericRepository<Slider> sliderRepository;

        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            this.sliderRepository = sliderRepository;
        }


        #endregion

        #region slider
        public async Task<List<Slider>> GetActiveSliders()
        {
            return await sliderRepository.GetEntitiesQuery().Where(slider => !slider.IsDelete).ToListAsync();
        }

        public async Task<List<Slider>> GetAllSlider()
        {
            return await sliderRepository.GetEntitiesQuery().ToListAsync();
        }
        public async Task AddSlider(Slider slider)
        {
           await sliderRepository.AddEntity(slider);
            await sliderRepository.SaveChanges();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await sliderRepository.GetEntityById(sliderId);
        }

        public async Task UpdateSlider(Slider slider)
        {
            sliderRepository.UpdateEntity(slider);
            await sliderRepository.SaveChanges();
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            sliderRepository?.Dispose();
        }
        #endregion
    }
}
