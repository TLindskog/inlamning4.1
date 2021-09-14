using System;
using System.Collections.Generic;
using System.Linq;
using APILibrary.Models.Todos;
using APILibrary.Models.Users;
using APILibrary.Repositories.Users;

namespace APILibrary.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Dictionary<int, Post> _todos = new Dictionary<int, Post>();

        public PostRepository(IUserRepository userRepository)
        {
            List<int> post1LikeList = new List<int>
            {
                3
            };

            List<int> post2LikeList = new List<int>
            {
                4,
                5
            };

            var user = userRepository.GetUser(1);
            var todo = new Post(1)
            {
                Text = "I like feets",
                CreatedBy = user,
                HasBeenEdited = false,
                ListOfUsersThatLikedThisPost = post1LikeList

            };

            var user1 = userRepository.GetUser(2);
            var todo1 = new Post(2)
            {
                Text = "Whats your favorite Tarantino movie?",
                CreatedBy = user1,
                HasBeenEdited = false,
                ListOfUsersThatLikedThisPost = post2LikeList
            };

            _todos.Add(1, todo);
            _todos.Add(2, todo1);
        }

        public Post GetPostWithId(int id)
        {
            _todos.TryGetValue(id, out Post result);
            return result;
        }

        public IEnumerable<Post> GetPostsCreatedBy(string createdBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPosts()
        {
            return _todos.Values;
        }
        public Post Add(PostDto todoDto, User user)
        {
            var id = _todos.Count + 1;
            var todo = new Post(id, todoDto, user);
            _todos.Add(id, todo);
            return todo;
        }

        public void Update(Post todo)
        {
            _todos.Remove(todo.Id);
            _todos.Add(todo.Id, todo);
        }

        public void ApplyPatch(Post todo, Dictionary<string, object> patches)
        {
            ApplyPatch<Post>(todo, patches);
        }

        public void Delete(Post todo)
        {
            _todos.Remove(todo.Id);
        }

        private void ApplyPatch<T>(T original, Dictionary<string, object> patches)
        {
            var properties = original.GetType().GetProperties();
            foreach (var patch in patches)
            {
                foreach (var prop in properties)
                {
                    if (string.Equals(patch.Key, prop.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        prop.SetValue(original, patch.Value);
                    }
                }
            }
        }
    }
}