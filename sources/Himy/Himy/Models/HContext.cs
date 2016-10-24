using Himy.Models.Accounts;
using Himy.Models.Logs;
using System.Data.Entity;

namespace Himy.Models
{
    public class HContext : DbContext
    {
        public HContext()
        { }

        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<Log> Logs
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
