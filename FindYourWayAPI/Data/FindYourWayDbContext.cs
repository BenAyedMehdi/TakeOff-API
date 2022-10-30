using FindYourWayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Data
{
    public class FindYourWayDbContext: DbContext
    {
        public FindYourWayDbContext(DbContextOptions<FindYourWayDbContext> options): base(options)
        {
        }

        public DbSet<Company> Companies{ get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
