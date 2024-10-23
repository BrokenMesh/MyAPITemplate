using MyAPITemplate.Model;

namespace MyAPITemplate.Database
{
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

        public static void DeleteTodo(Guid todo_id)
            => DBEntityContext.DBContext.entitys.Delete<Todo>(todo_id);

        public static Todo UpdateTodo(Todo todo) {
            using (var context = new DBEntityContext()) {
                var oldTodo = context.Set<Todo>().First(t => t.User_id == todo.User_id);
                oldTodo.Name = todo.Name;
                oldTodo.Done = todo.Done;
                oldTodo.UpdatedAt = DateTime.Now;
                context.SaveChanges();

                return oldTodo;
            }
        }   

        public static void TruncateTodos() {
            string _sql = "TRUNCATE tbl_todo;";
            ExecuteNonQuery(_sql, new Dictionary<string, object>());
        }
    }
}
