using DataLayer.EfContext;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private PolygonContext _dbContext;
        private PolygonRepository _polygonRepository;

        public EfUnitOfWork(DbContextOptions<PolygonContext> options)
        {
            _dbContext = new PolygonContext(options);
        }

        public IRepository<Polygon> Polygons
        {
            get
            {
                if (_polygonRepository == null)
                {
                    _polygonRepository = new PolygonRepository(_dbContext);
                }
                return _polygonRepository;
            }
        }
    }
}
