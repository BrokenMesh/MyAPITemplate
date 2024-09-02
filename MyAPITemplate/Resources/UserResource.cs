using MyAPITemplate.Database;
using MyAPITemplate.Model;

namespace MyAPITemplate.Resources
{
    public class UserResource : Resource<User>
    {
        public override User Generate() {
            string _name = Faker.Name.First();
            return DBManager.CreateUser(_name, "1234", _name + "@gmail.com");
        }

        public override Dictionary<string, object>? Make(User _user) {
            return new Dictionary<string, object> {
                { "id", _user.User_id },
                { "username", _user.Username },
                { "email", _user.Email },
            };
        }
    }
}
