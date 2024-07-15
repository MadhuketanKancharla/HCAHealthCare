using Microsoft.EntityFrameworkCore;
using HCAHealthcare.Models;

namespace HCAHealthcare.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
