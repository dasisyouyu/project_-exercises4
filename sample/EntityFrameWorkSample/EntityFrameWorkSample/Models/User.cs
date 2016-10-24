using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWorkSample.Models
{
    [Table("Users")]
    public class User : IUser<string>
    {
        [Column("Id")]
        [Required]
        public string Id
        {
            get;
            set;
        }

        [Column("UserName")]
        [Required]
        public string UserName
        {
            get;
            set;
        }

        [Column("Password")]
        [Required]
        public string Password
        {
            get;
            set;
        }

        [Column("MailAddress")]
        [Required]
        public string MailAddress
        {
            get;
            set;
        }

        [Column("IsValid")]
        public bool IsValid
        {
            get;
            set;
        }

        [Column("DateCreated")]
        public DateTime DateCreated
        {
            get;
            set;
        }

        [Column("LastUpdated")]
        public DateTime LastUpdated
        {
            get;
            set;
        }
    }
}