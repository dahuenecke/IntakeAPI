using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntakeApi.Models
{
    public class IntakeContext : DbContext
    {
        public IntakeContext(DbContextOptions<IntakeContext> options) 
            : base(options)
        {
        }

        public DbSet<IntakeItem> IntakeItems { get; set; }
    }
}
