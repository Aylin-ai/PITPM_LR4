using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PITPM_LR4.Models
{
    public class Context : DbContext
    {
        public Context() : base("PITPM_LR4") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Good> Goods { get; set; }
    }
}
