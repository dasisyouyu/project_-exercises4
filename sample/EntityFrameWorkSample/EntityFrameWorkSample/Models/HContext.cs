using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EntityFrameWorkSample.Models
{
    public class HContext : DbContext
    {
        public HContext()
        {
        }

        public DbSet<User> Users
        {
            get;
            set;
        }

        public static HContext Create()
        {
            return new HContext();
        }
    }
}