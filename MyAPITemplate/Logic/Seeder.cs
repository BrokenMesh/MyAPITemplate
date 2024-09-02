using MyAPITemplate.Database;
using MyAPITemplate.Resources;

namespace MyAPITemplate.Logic
{
    public class Seeder
    {
        public static UserResource userResource = new UserResource();
        public static TodoResource todoResource = new TodoResource();

        internal static void Seed() {
            DBManager.SetForeignKeyChecks(false); // allow truncating of tables with relations

            DBManager.TruncateUsers();
            userResource.Generate(10);
            DBManager.CreateUser("BrokenMesh", "Hallosaid1", "elkordhicham@gmail.com");

            DBManager.TruncateTodos();
            todoResource.Generate(10);

            DBManager.SetForeignKeyChecks(true);
        }
        
    }
}
