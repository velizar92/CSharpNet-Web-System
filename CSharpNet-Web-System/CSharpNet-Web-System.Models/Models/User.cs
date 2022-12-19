namespace CSharpNet_Web_System.Models.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    using static DataConstants.User;

    public class User : IdentityUser
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Issues = new HashSet<Issue>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string? LastName { get; set; }

        [Required]
        public string? ProfileImageUrl { get; set; }
     
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
