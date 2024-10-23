using MyAPITemplate.Logic;
using MyAPITemplate.Model;
using System;
using System.Security.Cryptography;

namespace MyAPITemplate.Database
{
    public partial class DBManager
    {
        public static User CreateUser(string _username, string _password, string _email) {
            var user = new User
            {
                Username = _username,
                Password = _password,
                Email = _email
            };

            return DBEntityContext.DBContext.entitys.Create<User>(user);
        }

        public static List<User> GetAllUsers() 
            => (List<User>)DBEntityContext.DBContext.entitys.GetAll<User>();

        public static bool TryGetUser(Guid user_id, out User user) 
            => DBEntityContext.DBContext.entitys.TryGetById<User>(user_id, out user);

        public static bool UserExists(Guid user_id)
            => DBEntityContext.DBContext.entitys.TryGetById<User>(user_id, out _);

        public static User GetUser(Guid user_id)
            => DBEntityContext.DBContext.entitys.GetById<User>(user_id);

        public static void DeleteUser(Guid user_id)
            => DBEntityContext.DBContext.entitys.Delete<User>(user_id);

        public static User UpdateUser(User user) {
            using (var context = new DBEntityContext()) {
                var oldUser = context.Set<User>().First(u => u.User_id ==  user.User_id);
                oldUser.Username = user.Username;
                oldUser.Password = user.Password;
                oldUser.Email = user.Email;
                oldUser.UpdatedAt = DateTime.Now;
                context.SaveChanges();

                return oldUser;
            }
        }

        public static Guid? GetUserIDFromUsername(string _username) {
            using (var context = new DBEntityContext()) {
                User? user = context.Set<User>().First(u => u.Username == _username);
                if (user != null) {
                    return user.User_id;
                }
            }

            return null;
        }

        public static bool? CheckUserPassword(Guid _id, string _password) {
            using (var context = new DBEntityContext()) {
                User? user = context.Set<User>().First(u => u.User_id == _id);
                if (user != null) {
                    return Auth.VerifyPassword(_password, user.Password);
                }
            }

            return null;
        }

        public static void TruncateUsers() {
            string _sql = "TRUNCATE tbl_user;";
            ExecuteNonQuery(_sql, new Dictionary<string, object>());
        }
    }
}
