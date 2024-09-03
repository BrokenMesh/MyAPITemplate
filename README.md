<a name="readme-top"></a>

<div align="center">
<h3 align="center">My API Template</h3>

  <p align="center">
    A API Template that sets up resouces, database, Models and endpoints the way I like it. 
    <br />
    <a href="https://github.com/BrokenMesh/MyAPITemplate/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrokenMesh/MyAPITemplate/issues">Request Feature</a>
  </p>
</div>


<!-- GETTING STARTED -->
## Getting Started

### Installation
1. Click on the `Use Template` button to fork this template
2. Clone your newly created repository:
   ```sh
   git clone https://github.com/<Your-Name>/<Your-Repo>.git
   ```
3. Open the solution file:
   ```sh
   Template.sln
   ```
4. Update the `Config.txt` file.
5. Build the project.
6. Start renaming the project, namespaces, and database as needed.

<!-- USAGE EXAMPLES -->
## Adding a Model

1. Create a new class in the `Model` directory, inheriting from the DBElement class, and define all necessary fields.
   
`Model/Todo.cs`
```C#
    [Table("tbl_todo")]
    public class Todo : DBElement
    {
        [Key, Column(TypeName = "binary(16)")]
        public Guid Todo_id { get; set; } = new Guid();

        [Required, Column(TypeName = "binary(16)")]
        public Guid User_id { get; set; } = new Guid();

        [MaxLength(255)]
        public string Name { get; set; }

        public bool Done { get; set; }
    }
```

2. Create the corresponding table in your MySQL environment with the same structure:
```SQL
CREATE TABLE `tbl_todo` (
  `Todo_id` binary(16) NOT NULL,
  `User_id` binary(16) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Done` tinyint DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Todo_id`),
  KEY `todo_user_idx` (`User_id`),
  CONSTRAINT `todo_user` FOREIGN KEY (`User_id`) REFERENCES `tbl_user` (`User_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```

3. Add the required DBManager methods in the `Database` directory.
   
`Database/TodoDB.cs`
```C#
    public partial class DBManager
    {
        public static Todo CreateTodo(string _name, bool _done, Guid _userId) {
            var todo = new Todo
            {
                Name = _name,
                Done = _done,
                User_id = _userId
            };

            return DBEntityContext.DBContext.entitys.Create<Todo>(todo);
        }

        public static List<Todo> GetAllTodos()
            => (List<Todo>)DBEntityContext.DBContext.entitys.GetAll<Todo>();

        public static bool TryGetTodo(Guid todo_id, out Todo todo)
            => DBEntityContext.DBContext.entitys.TryGetById<Todo>(todo_id, out todo);

        public static Todo GetTodo(Guid todo_id)
            => DBEntityContext.DBContext.entitys.GetById<Todo>(todo_id);

        public static void TruncateTodos() {
            string _sql = "TRUNCATE tbl_todo;";
            ExecuteNonQuery(_sql, new Dictionary<string, object>());
        }
    }
```

4. Create a resource for the new model in the `Resources` directory.
   
`Resources/TodoResource.cs`
```C#
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
```
This defines how the model will be outputted. You can have multiple resources for each model, and resources can be composed to, for example, return the actual user in the todo output.

5. Finally, create the controller.
   
`Controller/TodoController.cs`
```C#
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    TodoResource resource = new TodoResource();

    [HttpGet]
    public void Index() {
        Responses.JsonOk(Response, resource.Make(DBManager.GetAllTodos()));
    }

    [HttpGet]
    [Route("{_todoId}")]
    public void Show(Guid _todoId) {

        if (!DBManager.TryGetTodo(_todoId, out Todo _c)) {
            Responses.Respond(Response, "Element not found", 404);
            return;
        }

        Responses.JsonOk(Response, resource.Make(_c)!);
    }

    [HttpPost]
    public void Store(CreateTodoRequest _request) {
        if (!DBManager.UserExists(_request.UserId)) {
            Responses.Respond(Response, $"User '{_request.UserId}' not found", 422);
            return;
        }

        Todo _newTodo = DBManager.CreateTodo(_request.Name, false, _request.UserId);
        Responses.JsonOk(Response, resource.Make(_newTodo)!);
    }
}
```

Now you can test your new endpoints. For testing, you can use the example data generated by the `Generate()` method in `TodoResource.cs`.

6. Use the seeder to populate your database with test data.

`Logic/Seeder.cs`
```C#
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
```

<!-- LICENSE -->
## License

Distributed under the MIT License. See LICENSE.txt for more information.

<!-- CONTACT -->
## Contact

Hicham El-Kord - elkordhicham@gmail.com

Project Link: [https://github.com/BrokenMesh/MyAPITemplate](https://github.com/BrokenMesh/MyAPITemplate)


<p align="right">(<a href="#readme-top">back to top</a>)</p>
