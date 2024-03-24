using System;
using AutoMapper;
using WeGapApi.Models;
using WeGapApi.Models.Dto;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class EmployerService : IEmployerService
	{
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public EmployerService(IRepositoryManager repositoryManager, IMapper mapper)
		{
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

       

        public async Task<EmployerDto> AddEmployerAsync(AddEmployerDto addEmployerDto)
        {
           

            var employerDomain = _mapper.Map<Employer>(addEmployerDto);
            //  var userDomain = _employerRepository.GetEmployerByIdAsync;

            var EmployerDomain = await _repositoryManager.Employer.AddEmployerAsync(employerDomain);

            var employerDto = _mapper.Map<EmployerDto>(EmployerDomain);
            return employerDto;

        }



        public async Task<List<EmployerDto>> GetAllEmployerAsync()
        {

            var employers = await _repositoryManager.Employer.GetAllEmployerAsync();

            var employerDto = _mapper.Map<List<EmployerDto>>(employers);

            return employerDto;
        }

        public async Task<EmployerDto> GetEmployerByIdAsync(Guid id)
        {
            //get data from Database
            var employer = await _repositoryManager.Employer.GetEmployerByIdAsync(id);


            //return DTO usimg Mapper
            var employerDto = _mapper.Map<EmployerDto>(employer);

            //if (employer is null)
            //    return NotFound();

            return employerDto;
        }

        

        public async Task<EmployerDto> UpdateEmployerAsync(Guid id, UpdateEmployerDto updateEmployerDto)
        {
            ////validation
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            //Map DTO to Domain model
            var employerDomain = _mapper.Map<Employer>(updateEmployerDto);

            //check if employee exists
            employerDomain = await _repositoryManager.Employer.UpdateEmployerAsync(id, employerDomain);

            //if (employerDomain == null)
            //    return NotFound();


            var employerDto = _mapper.Map<EmployerDto>(employerDomain);

            return employerDto;
        }


        public async Task<EmployerDto> DeleteEmployerAsync(Guid id)
        {
            var employerDomain = await _repositoryManager.Employer.DeleteEmployerAsync(id);

            

           var employerDto =  _mapper.Map<EmployerDto>(employerDomain);
            return employerDto;
        }

        public async Task<EmployerDto> EmployerExists(string id)
        {
            var employer =  await _repositoryManager.Employer.EmployerExists(id);
            var employerDto = _mapper.Map<EmployerDto>(employer);
            return employerDto;
        }
    }
}

