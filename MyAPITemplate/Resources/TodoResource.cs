using MyAPITemplate.Database;
using MyAPITemplate.Model;

namespace MyAPITemplate.Resources
{

    public class TodoResource : Resource<Todo>
    {
        UserResource userResource = new UserResource();
        
        public override Todo Generate() {
            var _users = DBManager.GetAllUsers();

            Guid _randomUser = _users[Faker.RandomNumber.Next(_users.Count - 1)].User_id;

            return DBManager.CreateTodo(
                Faker.Company.CatchPhrase(),
                Faker.Boolean.Random(),
                _randomUser
            );
        }

        public override Dictionary<string, object>? Make(Todo _todo) {
            return new Dictionary<string, object> {
                { "id", _todo.Todo_id },
                { "name", _todo.Name },
                { "done", _todo.Done },
                { "user", userResource.Make(DBManager.GetUser(_todo.User_id))! },
            };
        }
    }
}
