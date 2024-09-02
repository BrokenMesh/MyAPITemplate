using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPITemplate.Logic;

namespace MyAPITemplate.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public void Get() {
            if (Auth.ProtectedHandler(Response, Request, out Guid _user_id)) {
                string _token = (string)Request.Headers["Authorization"];
                _token = _token.Substring("Bearer ".Length);

                Responses.JsonOk(Response, new LoginController.LoginResponse(_token));
            }
        }
    }
}
