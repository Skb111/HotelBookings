using HotelBookings.Application.Common.Interfaces;
using HotelBookings.Domain.Entities;
using HotelBookings.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookings.Persistence.Infrastructure
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        //public void Add(Villa entity)
        //{
        //    _context.Add(entity);
        //}

        //public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        //{
        //    IQueryable<Villa> query = _context.Set<Villa>();
        //    if (filter != null)
        //    {

        //        query = query.Where(filter);

        //    }
        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProp);
        //        }
        //    }
        //    return query.FirstOrDefault();
        //}

        //public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        //{
        //    IQueryable<Villa> query = _context.Set<Villa>();
        //    if (filter != null)
        //    {

        //    query = query.Where(filter); 
            
        //    }
        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProp);
        //        }
        //    }
        //    return query.ToList();
        //}

        //public void Remove(Villa entity)
        //{
        //    _context.Remove(entity);
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public void Update(Villa entity)
        {
            _context.Update(entity);
        }
    }
}
