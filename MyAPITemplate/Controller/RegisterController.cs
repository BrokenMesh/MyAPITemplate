using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPITemplate.Database;

namespace MyAPITemplate.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public void Post(RegisterRequest _request) {

            // TODO: Full Username check (length)
            Guid? _id = DBManager.GetUserIDFromUsername(_request.Username);
            if (_id != null) {
                var _errors = new Dictionary<string, string>();
                _errors.Add("username", "Nutzer existiert schon!");
                Responses.ValidationError(Response, "Register Validation Error", _errors);
                return;
            }

            // TODO: Full Username check (length, special chars etc)
            if (_request.Password.Length < 8) {
                var _errors = new Dictionary<string, string>();
                _errors.Add("password", "Passwort muss mindestens 8 zeichen beinhalten!");
                Responses.ValidationError(Response, "Register Validation Error", _errors);
                return;
            }

            DBManager.CreateUser(_request.Username, _request.Password, _request.Email);

            Responses.JsonOk(Response, "Registration Successful");
        }

        public class RegisterRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public RegisterRequest(string username, string password, string email) {
                Username = username;
                Password = password;
                Email = email;
            }
        }
    }
}
