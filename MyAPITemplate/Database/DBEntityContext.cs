using Microsoft.EntityFrameworkCore;
using MyAPITemplate.Logic;
using MyAPITemplate.Model;
using System.Reflection;
using MySql.Data;

namespace MyAPITemplate.Database
{
    public class DBEntityContext : DbContext
    {
        public static DBEntityContext DBContext => new DBEntityContext();
        public EntityManager entitys => new EntityManager(this);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(
                $"server={Config.current.DB_host};userid={Config.current.DB_username};password={Config.current.DB_password};database={Config.current.DB_database};",
                new MySqlServerVersion(new Version(8, 0, 21)) 
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes().Where(t => t.IsSubclassOf(typeof(DBElement)) && !t.IsAbstract);

            foreach (var type in entityTypes) {
                modelBuilder.Entity(type);
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> Set<T>() where T : DBElement {
            return base.Set<T>();
        }

    }
}
