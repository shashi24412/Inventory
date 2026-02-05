using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public DbSet<INVM_ItemMaster> ItemMasters { get; set; }

}