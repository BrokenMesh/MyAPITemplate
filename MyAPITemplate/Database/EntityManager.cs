using Microsoft.EntityFrameworkCore;
using MyAPITemplate.Model;

namespace MyAPITemplate.Database
{
    public class EntityManager
    {
        private readonly DbContext _context;

        public EntityManager(DbContext context) {
            _context = context;
        }

        public T GetById<T>(Guid id) where T : DBElement {
            return _context.Set<T>().Find(id);
        }

        public bool TryGetById<T>(Guid id, out T t) where T : DBElement, new() {
            t = new T();
            T? nt = _context.Set<T>().Find(id);

            if (nt != null) {
                t = nt;
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetAll<T>() where T : DBElement {
            return _context.Set<T>().ToList();
        }

        public T Create<T>(T entity) where T : DBElement {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update<T>(T entity) where T : DBElement {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : DBElement {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(Guid id) where T : DBElement {
            T t = GetById<T>(id);   
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
        }
    }
}
