using Microsoft.EntityFrameworkCore;
using OliSaude.API.Models;

namespace OliSaude.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Patient> Patients => Set<Patient>();
}