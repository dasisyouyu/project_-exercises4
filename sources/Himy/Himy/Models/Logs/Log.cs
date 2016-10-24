using Himy.Models.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himy.Models.Logs
{
    [Table("Logs")]
    public class Log
    {
        [Column("Id")]
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Column("LogTypeId")]
        [Required]
        public MstLogTypes LogTypeId
        {
            get;
            set;
        }

        [Column("Message")]
        [Required]
        [StringLength(255)]
        public string Message
        {
            get;
            set;
        }

        [Column("Contents")]
        public string Contents
        {
            get;
            set;
        }

        [Column("DateCreated")]
        [Required]
        public DateTime DateCreated
        {
            get;
            set;
        }
    }
}
