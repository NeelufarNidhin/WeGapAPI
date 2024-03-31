using System;
using WeGapApi.Data;
using WeGapApi.Repository.Interface;

namespace WeGapApi.Repository
{
	public class RepositoryManager :IRepositoryManager
	{
        private readonly ApplicationDbContext _dbContext;

        private readonly  Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IEmployerRepository> _employerRepository;
        private readonly Lazy<IEducationRepository> _educationRepository;
        private readonly Lazy<IExperienceRepository> _experienceRepository;
        private readonly Lazy<IJobRepository> _jobRepository;
        private readonly Lazy<IJobSkillRepository> _jobSkillRepository;
        private readonly Lazy<IJobTypeRepository> _jobTypeRepository;
        private readonly Lazy<ISkillRepository> _skillRepository;


        public RepositoryManager(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() =>  new EmployeeRepository(dbContext));
            _employerRepository = new Lazy<IEmployerRepository>(() => new EmployerRepository(dbContext));
            _educationRepository = new Lazy<IEducationRepository>(() => new EducationRepository(dbContext));
            _experienceRepository = new Lazy<IExperienceRepository>(() => new ExperienceRepository(dbContext));
            _jobRepository = new Lazy<IJobRepository>(() => new JobRepository(dbContext));
            _jobSkillRepository = new Lazy<IJobSkillRepository>(() => new JobSkillRepository(dbContext));
            _jobTypeRepository = new Lazy<IJobTypeRepository>(() => new JobTypeRepository(dbContext));
            _skillRepository = new Lazy<ISkillRepository>(() => new SkillRepository(dbContext));


        }

        public IUserRepository User => _userRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public IEmployerRepository Employer => _employerRepository.Value;

        public IEducationRepository Education => _educationRepository.Value;

        public IExperienceRepository Experience => _experienceRepository.Value;

        public IJobRepository Job => _jobRepository.Value;

        public IJobSkillRepository JobSkill => _jobSkillRepository.Value;

        public IJobTypeRepository JobType => _jobTypeRepository.Value;

        public ISkillRepository Skill => _skillRepository.Value;

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

