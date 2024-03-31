using System;
namespace WeGapApi.Services.Services.Interface
{
	public interface IServiceManager
	{
		IUserService UserService { get; }
		IEmployeeService EmployeeService { get; }
		IEmployerService EmployerService { get; }
		IEducationService EducationService { get; }
		IExperienceService ExperienceService { get; }
		IJobService JobService { get; }
		IJobSkillService JobSkillService { get; }
        ISkillService SkillService { get; }
        IJobTypeService JobTypeService { get; }
		IAuthenticationService AuthenticationService { get; }
	}
}

