using Faker;
using Microsoft.AspNetCore.Mvc;
using MyAPITemplate.Database;
using MyAPITemplate.Logic;
using MyAPITemplate.Model;
using MyAPITemplate.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyAPITemplate.Controller
{
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

        [HttpDelete]
        [Route("{_todoId}")]
        public void Delete(Guid _todoId) {
            if (!DBManager.TryGetTodo(_todoId, out Todo _c)) {
                Responses.Respond(Response, "Element not found", 404);
                return;
            }

            DBManager.DeleteTodo(_todoId);

            Responses.Respond(Response, 204);
        }

        public class CreateTodoRequest 
        {
            [Required, StringLength(255)] public string Name { get; set; }
            [Required] public Guid UserId { get; set; }
        }
    }
}
