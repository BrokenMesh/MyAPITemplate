using Microsoft.IdentityModel.Tokens;
using MyAPITemplate.Controller;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace MyAPITemplate.Logic
{
    public class Auth
    {
        public static string? GenerateJSONWebToken(Guid _user_id) {
            try {
                var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.current.SecretKey));
                var _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

                var _claims = new[] {
                    new Claim("user_id", _user_id.ToString()),
                };

                var token = new JwtSecurityToken(
                    Config.current.PageURL,
                    Config.current.PageURL,
                    _claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: _credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception _e) {
                Console.WriteLine("GenerateJSONWebToken: " + _e.Message);
                return null;
            }
        }

        public static bool VerifyToken(string _srtToken, out Guid _user_id) {
            _user_id = Guid.Empty;

            var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.current.SecretKey));

            try {
                var _claims = new JwtSecurityTokenHandler().ValidateToken(
                    _srtToken,
                    new TokenValidationParameters
                    {
                        IssuerSigningKey = _securityKey,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Config.current.PageURL,
                        ValidAudience = Config.current.PageURL,
                    },
                    out var _token); ;

                string? _userIdClaim = _claims.FindFirstValue("user_id");
                if (_userIdClaim == null) throw new Exception("Claim is missing");

                _user_id = Guid.Parse(_userIdClaim);
                return true;
            }
            catch (Exception _e) {
                Console.WriteLine("VerifyToken: " + _e);
                return false;
            }
        }

        public static bool ProtectedHandler(HttpResponse w, HttpRequest r, out Guid _user_id) {
            _user_id = Guid.Empty;

            if (!r.Headers.ContainsKey("Authorization")) {
                Responses.Unauthorized(w, "Missing authorization header");
                return false;
            }

            string _token = (string)r.Headers["Authorization"];
            _token = _token.Substring("Bearer ".Length);

            Guid _userIdClaim;
            if (!VerifyToken(_token, out _userIdClaim)) {
                Responses.Unauthorized(w, "Invalid token");
                return false;
            };

            _user_id = _userIdClaim;
            return true;
        }

        public static string HashPassword(string _password) {
            return BCrypt.Net.BCrypt.HashPassword(_password);
        }

        public static bool VerifyPassword(string _inputPassword, string _password) {
            return BCrypt.Net.BCrypt.Verify(_inputPassword, _password);
        }
    }
}
