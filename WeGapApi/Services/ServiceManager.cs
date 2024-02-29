using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WeGapApi.Models;
using WeGapApi.Repository.Interface;
using WeGapApi.Services.Services.Interface;

namespace WeGapApi.Services
{
	public class ServiceManager :IServiceManager
	{
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IEmployerService> _employerService;
        private readonly Lazy<IJobService> _jobService;
        private readonly Lazy<IJobSkillService> _jobSkillService;
        private readonly Lazy<IJobTypeService> _jobTypeService;
        private readonly Lazy<EducationService> _educationService;
        private readonly Lazy<ExperienceService> _experienceService;

		public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper, UserManager<ApplicationUser> userManager)
		{
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, mapper));
            _employerService = new Lazy<IEmployerService>(() => new EmployerService(repositoryManager, mapper));
            _educationService = new Lazy<EducationService>(() => new EducationService(repositoryManager, mapper));
            _experienceService = new Lazy<ExperienceService>(() => new ExperienceService(repositoryManager, mapper));
            _jobService = new Lazy<IJobService>(() => new JobService(repositoryManager, mapper));
            _jobSkillService = new Lazy<IJobSkillService>(() => new JobSkillService(repositoryManager, mapper));
            _jobTypeService = new Lazy<IJobTypeService>(() => new JobTypeService(repositoryManager, mapper));
		}

        public IUserService UserService => _userService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IEmployerService EmployerService => _employerService.Value;

        public IEducationService EducationService => _educationService.Value;

        public IExperienceService ExperienceService => _experienceService.Value;

        public IJobService JobService => _jobService.Value;

        public IJobSkillService JobSkillService => _jobSkillService.Value;

        public IJobTypeService JobTypeService => _jobTypeService.Value;
    }
}

