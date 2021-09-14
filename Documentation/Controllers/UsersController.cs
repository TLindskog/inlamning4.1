using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APILibrary.Models.Todos;
using APILibrary.Models.Users;
using APILibrary.Repositories;
using APILibrary.Repositories.Users;

namespace Documentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Fetches all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        /// <summary>
        /// Fetches an user based on the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetUserWithId(id);
            if (user is null)
                return NotFound(user);
            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
                _userRepository.Add(user);
                return user;
        }

        /// <summary>
        /// Deletes an existing todo based on the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserWithId(id);
            if (user is null)
                return NotFound($"No user with {id} found");
            _userRepository.Delete(user);
            return NoContent();
        }
    }
}