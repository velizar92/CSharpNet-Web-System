namespace CSharpNet_Web_System.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNet.Identity.EntityFramework;
 
    public class User : IdentityUser
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Issues = new HashSet<Issue>();
        }

        [Required]       
        public string FirstName { get; set; }

        [Required]       
        public string LastName { get; set; }

        [Required]
        public string ProfileImageUrl { get; set; }
     
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
