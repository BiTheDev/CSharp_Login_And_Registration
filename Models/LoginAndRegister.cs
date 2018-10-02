using System;
using System.ComponentModel.DataAnnotations;

namespace LoginRegister.Models
{
    public class LoginAndRegister{

        [Key]
        public long id{get;set;}

        [Required]
        public string first_name{get; set;}

        [Required]
        public string last_name{get; set;}


        [Required]
        public string email {get; set;}


        [Required]
        public string password {get; set;}

        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}
    }
}