using System.Collections.Generic;
using HTS.Data.Entity;
using HTS.Dto.City;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    [Authorize]
    public class CityService : ApplicationService, ICityService
    {

        private readonly IRepository<City, int> _cityRepository;
        public CityService(IRepository<City, int> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<CityDto> GetAsync(int id)
        {
            return ObjectMapper.Map<City, CityDto>(await _cityRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<CityDto>> GetListAsync()
        {
            //Get all entities
            var responseList = ObjectMapper.Map<List<City>, List<CityDto>>(await _cityRepository.GetListAsync());
            var totalCount = await _cityRepository.CountAsync();//item count
            //TODO:Hopsy Ask Kerem the isActive case 
            return new PagedResultDto<CityDto>(totalCount, responseList);
        }

        public async Task<CityDto> CreateAsync(SaveCityDto city)
        {
            var entity = ObjectMapper.Map<SaveCityDto, City>(city);
            await _cityRepository.InsertAsync(entity);
            return ObjectMapper.Map<City, CityDto>(entity);
        }

        public async Task<CityDto> UpdateAsync(int id, SaveCityDto city)
        {
            var entity = await _cityRepository.GetAsync(id);
            ObjectMapper.Map(city, entity);
            return ObjectMapper.Map<City, CityDto>(await _cityRepository.UpdateAsync(entity));
        }

        public async Task DeleteAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }
    }
}
