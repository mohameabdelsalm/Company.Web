using Company.Data.Context;
using Company.Data.Entites;
using Company.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenricRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        
          =>  _context.Set<T>().Add(entity);
            
        


        public void Delete(T entity)
        
        =>  _context.Set<T>().Remove(entity);
            
        
        public IEnumerable<T> GetAll()
        => _context.Set<T>().ToList();

        public T GetById(int id)
      => _context.Set<T>().Find(id);

        public void Update(T entity)
        
          =>  _context.Set<T>().Update(entity);
            
        
    }
}
