using System;
using WeGapApi.Models;

namespace WeGapApi.Data
{
	public interface IUserRepository
	{
        
       
        ApplicationUser GetUserById(string id);
        ApplicationUser UpdateUser(string id,ApplicationUser user);
       List<ApplicationUser> GetUsers();
        List<ApplicationUser> GetSearchQuery(string searchString);
        ApplicationUser BlockUnblock(string id);
        ApplicationUser DeleteUser(string id);
    }
}


