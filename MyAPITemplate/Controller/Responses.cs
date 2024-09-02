using System.ComponentModel.DataAnnotations;

namespace MyAPITemplate.Controller
{
    public class Responses
    {

        public async static void JsonOk(HttpResponse r, object _value) {
            r.StatusCode = StatusCodes.Status200OK;
            await r.WriteAsJsonAsync(_value);
        }

        public async static void ValidationError(HttpResponse r, string _msg, Dictionary<string, string> _errors) {
            r.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await r.WriteAsJsonAsync(_errors);
        }

        public static void InternalServerError(HttpResponse r) {
            Respond(r, "Internale Server Error", StatusCodes.Status500InternalServerError);
        }

        public static void ServerError(HttpResponse r, string _msg) {
            Respond(r, "Server Error: " + _msg, StatusCodes.Status400BadRequest);
        }

        public static void Unauthorized(HttpResponse r, string _msg) {
            Respond(r, "Server Error: " + _msg, StatusCodes.Status401Unauthorized);
        }

        public async static void Respond(HttpResponse r, string _msg, int _statusCode) {
            r.StatusCode = _statusCode;

            var _response = new Dictionary<string, object> {
            { "message", _msg },
        };

            await r.WriteAsJsonAsync(_response);
        }

        public static void Respond(HttpResponse r, int _statusCode) {
            r.StatusCode = _statusCode;
        }

    }

    public class ValidationError
    {
        [Required] public string Message { get; set; }
        [Required] public Dictionary<string, string> Errors { get; set; }

        public ValidationError(string message, Dictionary<string, string> errors) {
            Message = message;
            Errors = errors;
        }
    }
}
