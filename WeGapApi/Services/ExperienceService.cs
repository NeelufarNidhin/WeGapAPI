using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class ExperienceService : IExperienceService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ExperienceService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ExperienceDto> AddExperienceAsync(AddExperienceDto addExperienceDto)
        {
            

            var experienceDomain = _mapper.Map<Experience>(addExperienceDto);
           

            var ExperienceDomain = await _repositoryManager.Experience.AddExperienceAsync(experienceDomain);

            var experienceDto = _mapper.Map<ExperienceDto>(ExperienceDomain);
            return experienceDto;
        }

        public async  Task<ExperienceDto> DeleteExperienceAsync(Guid id)
        {
            var experinceDomain = await _repositoryManager.Experience.DeleteExperienceAsync(id);

            
           var experienceDto =  _mapper.Map<ExperienceDto>(experinceDomain);

            return experienceDto;

        }

        public async Task<List<ExperienceDto>> GetAllExperienceAsync()
        {
            var experience = await _repositoryManager.Experience.GetAllExperienceAsync();
            var experienceDto = _mapper.Map<List<ExperienceDto>>(experience);
            return experienceDto;
        }

        public async Task<ExperienceDto> GetExperienceByIdAsync(Guid id)
        {
            //get data from Database
            var experience = await _repositoryManager.Experience.GetExperienceByIdAsync(id);


            //return DTO usimg Mapper
            var experienceDto = _mapper.Map<ExperienceDto>(experience);

           
            return experienceDto;
        }

        public async Task<List<ExperienceDto>> GetEmployeeExperience(Guid id)
        {
            //get data from Database
            var experience = await _repositoryManager.Experience.GetEmployeeExperience(id);


            //return DTO usimg Mapper
            var experienceDto = _mapper.Map<List<ExperienceDto>>(experience);


            return experienceDto;
        }

        public async Task<ExperienceDto> UpdateExperienceAsync(Guid id, UpdateExperienceDto updateExperienceDto)
        {
           


            //Map DTO to Domain model
            var experienceDomain = _mapper.Map<Experience>(updateExperienceDto);

            //check if experience exists
            experienceDomain = await _repositoryManager.Experience.UpdateExperienceAsync(id, experienceDomain);

            
            var experienceDto = _mapper.Map<ExperienceDto>(experienceDomain);
            return experienceDto;
        }
    }
}

