using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeGapApi.Models;

namespace WeGapApi.Data
{
	public class UserRepository : IUserRepository
	{
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

       

        public  List<ApplicationUser> GetUsers()
        {
            var users =  _db.ApplicationUsers.ToList();
            return users;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if(user is null)
            {
                throw new InvalidOperationException("User Not Found");
            }

            return user;


        }

        public List<ApplicationUser> GetSearchQuery(string searchString)
        {
            var users = _db.ApplicationUsers.Where(x => x.Email.ToLower().Contains(searchString)
            || x.FirstName.ToLower().Contains(searchString) || x.LastName.ToLower().Contains(searchString)).ToList();

            return users;
        }





        public ApplicationUser UpdateUser(string id, ApplicationUser user)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if(userFromDb is null)
            {
                throw new InvalidOperationException("User Not Found");
            }
            _db.ApplicationUsers.Update(user);
            _db.SaveChanges();
            return userFromDb;
        }

        public ApplicationUser BlockUnblock(string id)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userFromDb is null)
            {
                throw new InvalidOperationException("User Not Found");
            }

            userFromDb.IsBlocked = !userFromDb.IsBlocked;
            _db.ApplicationUsers.Update(userFromDb);
            _db.SaveChanges();

            return userFromDb;

           
        }

        public ApplicationUser DeleteUser(string id)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userFromDb is null)
            {
                throw new InvalidOperationException("User Not Found");
            }

            _db.ApplicationUsers.Remove(userFromDb);
            return userFromDb;
        }
    }

}


