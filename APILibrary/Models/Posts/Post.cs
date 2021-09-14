using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APILibrary.Models.Users;

namespace APILibrary.Models.Todos
{
    public class Post
    {
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";

        public Post(int id)
        {
            Id = id;
        }

        public Post(int id, PostDto postDto, User user)
        {
            Id = id;
            Text = postDto.Text;
            HasBeenEdited = postDto.HasBeenEdited;
            CreatedBy = user;
            ListOfUsersThatLikedThisPost = postDto.ListOfUsersThatLikedThisPost;
        }


        /// <summary>e
        /// IS unique for each post
        /// </summary>
        public int Id { get; private set; }

        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Text { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        public bool HasBeenEdited { get; set; }
        public DateTimeOffset LastEditDate { get; set; }
        public List<int> ListOfUsersThatLikedThisPost { get; set; }

    }
}