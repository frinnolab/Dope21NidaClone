using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEEK8APP.DATABASE;
using WEEK8APP.DTO;

namespace WEEK8APP.DAO.Implement
{
    public class UserRepository : IUSER
    {
        private readonly Persistance DB;

        public UserRepository(Persistance persistance)
        {
            DB = persistance;
        }

        public void CreateUser(User user)
        {
            var newUser = new User() 
            {
                FullName = user.FullName,
                Email = user.Email,
                mobile = user.mobile,
                NidaNumber = user.NidaNumber,
            };

            DB.Add(newUser);

            DB.SaveChanges();

        }

        public IList<User> GetAllUsers()
        {
            var users = DB.Users.ToList();
            return users;
        }

        public User GetCurrentUser(long id)
        {
            var user = DB.Users.Find(id);

            if (user!=null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        

        public User UpdateUser(User user)
        {
            var userUpdate = DB.Users.Find(user.Id);

            if (userUpdate!=null)
            {
                DB.Users.Update(user);
                DB.SaveChanges();
                return userUpdate;
            }
            return null;
        }
    }
}
