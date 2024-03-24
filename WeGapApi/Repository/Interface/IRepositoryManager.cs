using System;
using WeGapApi.Data;

namespace WeGapApi.Repository.Interface
{
	public interface IRepositoryManager
	{
		IUserRepository User { get; }
		IEmployeeRepository Employee { get; }
		IEmployerRepository Employer { get; }
		IEducationRepository Education { get; }
		IExperienceRepository Experience { get; }
		IJobRepository Job { get; }
		IJobSkillRepository  JobSkill { get;}
		IJobTypeRepository JobType { get; }
        ISkillRepository Skill { get; }

        void Save();
			 
 	}
}

