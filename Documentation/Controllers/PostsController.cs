using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APILibrary.Models.Todos;
using APILibrary.Repositories;
using APILibrary.Repositories.Users;

namespace Documentation.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IPostRepository todoRepository, IUserRepository userRepository)
        {
            _postRepository = todoRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Fetches all posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        /// <summary>
        /// Fetches the post based on the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound(post);
            return post;
        }

        /// <summary>
        /// Creates a new post. Using the todoDto.
        /// </summary>
        ///<param name="postDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatePost(PostDto postDto)
        {
            try
            {
                var user = _userRepository.GetUser(postDto.CreatedBy);
                var post = _postRepository.Add(postDto, user);
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Replaces an existing post with a new todo. All properties will be rpalced with the given post object. No new id will be created. It will be conserved.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postPut"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult ReplacePost(int id, PostDto postPut)
        {
            var user = _userRepository.GetUser(id);
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound($"No todo with {id} found");
            var putPost = new Post(id, postPut, user);
            _postRepository.Update(putPost);
            return NoContent();
        }

        /// <summary>
        /// Likes or unlikes a post for a specific user.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("likes/likeorunlike/{postid:int}/{userid:int}")]
        public ActionResult LikeOrUnlikeAPost(int postid, int userid)
        {
            var post = _postRepository.GetPostWithId(postid);

            if (post.ListOfUsersThatLikedThisPost.Contains(userid))
            {
                post.ListOfUsersThatLikedThisPost.Remove(userid);
                return NoContent();
            }
            else
            {
                post.ListOfUsersThatLikedThisPost.Add(userid);
                return NoContent();
            }
        }

        /// <summary>
        /// Unlikes a post for a specific user.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("likes/remove/{postid:int}/{userid:int}")]
        public ActionResult RemoveALikeToAPost(int postid, int userid)
        {

            var post = _postRepository.GetPostWithId(postid);

            if (post.ListOfUsersThatLikedThisPost.Contains(userid))
            {
                post.ListOfUsersThatLikedThisPost.Remove(userid);
            }
            return NoContent();
        }

        /// <summary>
        /// Selectively updates properties on an existing post.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userid"></param>
        /// <param name="patches"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("edit/{postid:int}/{userid:int}")]
        public ActionResult UpdatePost(int postId, int userid, Dictionary<string, object> patches)
        {

            var post = _postRepository.GetPostWithId(postId);
            if (post is null)
                return NotFound($"No post with {postId} found");

            int myInt = Convert.ToInt32(post.CreatedBy.Id);

            if(userid == myInt)
            {
                post.HasBeenEdited = false;
                post.LastEditDate = DateTimeOffset.Now;
                _postRepository.ApplyPatch(post, patches);
                return NoContent();
            }
            else
            {
                return BadRequest("You can't change the posts of others.");
            }
        }

        /// <summary>
        /// Deletes an existing post based on id. 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{postid:int}/{userid:int}")]
        public ActionResult DeletePost(int postid, int userid)
        {
            var post = _postRepository.GetPostWithId(postid);
            if (post is null)
                return NotFound($"No post with {postid} found");

            int myInt = Convert.ToInt32(post.CreatedBy.Id);

            if (userid == myInt)
            {
                _postRepository.Delete(post);
                return NoContent();
            }
            else
            {
                return BadRequest("You can't delete the posts of others.");
            }
        }
    }
}