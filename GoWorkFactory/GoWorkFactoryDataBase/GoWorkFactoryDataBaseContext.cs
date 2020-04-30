using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryDataBase
{
    public class GoWorkFactoryDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=GoWorkFactory;Username=postgres;Password=postgres");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
