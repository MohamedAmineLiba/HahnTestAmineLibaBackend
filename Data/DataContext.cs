using Microsoft.EntityFrameworkCore;
using Task_FullStackDevDotNet.Entities;

namespace Task_FullStackDevDotNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
