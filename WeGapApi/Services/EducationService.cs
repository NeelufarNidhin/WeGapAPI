using System;
using AutoMapper;
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
	}
}

