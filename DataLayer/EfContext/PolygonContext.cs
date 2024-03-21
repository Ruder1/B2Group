using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfContext
{
    public class PolygonContext : DbContext
    {
        public DbSet<Polygon> PolygonItems { get; set; }

        public PolygonContext(DbContextOptions<PolygonContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
