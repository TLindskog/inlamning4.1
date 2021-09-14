using System.Collections.Generic;
using APILibrary.Models.Users;

namespace APILibrary.Repositories.Users
{
    public interface IUserRepository
    {
        void Add(User user);

        User GetUser(int id);
        IEnumerable<User> GetUsers();
        User GetUserWithId(int id);
        void Delete(User user);

        bool UserNameIsNotUnique(User user);

    }
}