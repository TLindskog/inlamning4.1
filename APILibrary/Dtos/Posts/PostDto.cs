using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APILibrary.Models.Todos
{
    /// <summary>
    /// Used when creating a new post. 
    /// Some properties have been simplified such as CreatedBy that ony accepts an Id instead of an User object.
    /// </summary>
    public class PostDto
    {
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";

        /// <summary>
        /// A post posted by a user to the social network.
        /// </summary>
        /// <example>Today I took a shower and now I feel fresh!</example>
        [Required]
        [StringLength(400, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Text { get; set; }

        /// <summary>
        /// Id of the user that is creating this post. Must be an existing user.
        /// </summary>
        /// <example>2</example>
        [Required]
        public int CreatedBy { get; set; }

        /// <summary>
        /// A check whether or not a post has been edited. If it has been edited, this property will always be true.
        /// </summary>
        /// <example>false</example>
        public bool HasBeenEdited { get; set; }

        /// <summary>
        /// A List of ints of all the userId:s that have liked this post.
        /// <example>2,3</example>
        /// </summary>
        public List<int> ListOfUsersThatLikedThisPost { get; set; }
    }
}