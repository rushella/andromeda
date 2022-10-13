using Andromeda.Db.Tables;
using Microsoft.EntityFrameworkCore;

namespace Andromeda.Db;

public class AndromedaContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    public AndromedaContext(DbContextOptions<AndromedaContext> options) 
        : base(options)
    {
        
    }
}