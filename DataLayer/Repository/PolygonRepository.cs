using DataLayer.EfContext;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class PolygonRepository : IRepository<Polygon>
    {
        private readonly PolygonContext _context;

        public PolygonRepository(PolygonContext context)
        {
            _context = context;
        }

        public void Create(Polygon item)
        {
            _context.PolygonItems.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Polygon item)
        {
            if (item != null)
            {
                _context.PolygonItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public Polygon Get(int id)
        {
            return _context.PolygonItems.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Polygon> GetAll()
        {
            return _context.PolygonItems.ToList();
        }
    }
}
