using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using APILibrary.Models.Users;
using UserApi.Repositories;

namespace APILibrary.Repositories.Users
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<int, User> _users = new Dictionary<int, User>();

        public DictionaryUserRepository()
        {
            var user = new User
            {
                Id = 1,
                UserName = "Tom",
                Email = "tom@mail.com"
            };
            var user1 = new User
            {
                Id = 2,
                UserName = "Tim",
                Email = "Tim@mail.com"
            };
            _users.Add(1, user);
            _users.Add(2, user1);
        }

        public bool UserNameIsNotUnique(User user)
        {
            return _users.Any(e => e.Value.UserName == user.UserName);
        }

        public User GetUser(int id)
        {
            return _users[id];
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Values;
        }

        public User GetUserWithId(int id)
        {
            _users.TryGetValue(id, out User result);
            return result;
        }

        public void Add(User user)
        {
            if (UserNameIsNotUnique(user))
            {
                throw new NonUniqueUserName();
            }
            if (_users.ContainsKey(user.Id))
            {
                throw new NonUniqueId();
            }

            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(user.UserName))
            {
                throw new NonValidUserName();
            }

            if (!user.Email.Contains("@") || !user.Email.Contains("."))
            {
                throw new NonValidEmail();
            }
            _users.Add(user.Id, user);
        }

        public void Delete(User user)
        {
            _users.Remove(user.Id);
        }
    }
}