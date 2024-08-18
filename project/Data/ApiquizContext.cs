using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Data
{
    public class ApiquizContext : DbContext
    {
        public ApiquizContext(DbContextOptions<ApiquizContext> options) : base(options) { }

        public ApiquizContext() { } 
        public DbSet<product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<product>().ToTable("product");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server = "localhost";
            string database = "apiquiz";
            string uid = "root";
            string password = "12345678";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";PORT=3306;SslMode=Required;";
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}

