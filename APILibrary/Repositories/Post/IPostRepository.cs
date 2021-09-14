using System.Collections.Generic;
using APILibrary.Models.Todos;
using APILibrary.Models.Users;

namespace APILibrary.Repositories
{
    public interface IPostRepository
    {
        Post GetPostWithId(int id);

        IEnumerable<Post> GetPostsCreatedBy(string createdBy);

        IEnumerable<Post> GetPosts();

        Post Add(PostDto post, User user);

        void Update(Post todo);

        void ApplyPatch(Post post, Dictionary<string, object> patches);

        void Delete(Post todo);
    }
}