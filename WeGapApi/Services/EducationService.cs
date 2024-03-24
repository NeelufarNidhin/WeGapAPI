using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class EducationService : IEducationService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public EducationService( IRepositoryManager repositoryManager,IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<EducationDto> AddEducationAsync(AddEducationDto addEducationDto)
        {

            var educationDomain = _mapper.Map<Education>(addEducationDto);

             _repositoryManager.Education.AddEducationAsync(educationDomain);

            var educationDto = _mapper.Map<EducationDto>(educationDomain);
            return educationDto;

        }

        public async Task<List<EducationDto>> GetEmployeeEducation(Guid id)
        {
            //get data from Database
            var education = await _repositoryManager.Education.GetEmployeeEducation(id);


            //return DTO usimg Mapper
            var educationDto = _mapper.Map<List<EducationDto>>(education);


            return educationDto;
        }

        public async Task<EducationDto> DeleteEducationAsync(Guid id)
        {
            var educationDomain = await _repositoryManager.Education.DeleteEducationAsync(id);
            var educationDto = _mapper.Map<EducationDto>(educationDomain);
            return educationDto;

        }

        

        public async Task<List<EducationDto>> GetAllAsync()
        {
            var education = await _repositoryManager.Education.GetAllAsync();

            var educationDto = _mapper.Map<List<EducationDto>>(education);
            return educationDto;
        }

        public async Task<EducationDto> GetEducationByIdAsync(Guid id)
        {
            var education = await _repositoryManager.Education.GetEducationByIdAsync(id);


            var educationDto = _mapper.Map<EducationDto>(education);
            return educationDto;


        }

        public async Task<EducationDto> UpdateEducationAsync(Guid id, UpdateEducationDto updateEducationDto)
        {

            //Map DTO to Domain model
            var educationDomain = _mapper.Map<Education>(updateEducationDto);

            //check if education exists
            educationDomain = await _repositoryManager.Education.UpdateEducationAsync(id, educationDomain);

            


            var educationDto = _mapper.Map<EducationDto>(educationDomain);
            return educationDto;
        }
    }
}

