using MyAPITemplate.Logic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPITemplate.Model
{
    [Table("tbl_user")]
    public class User : DBElement
    {
        [Key, Column(TypeName = "binary(16)")]
        public Guid User_id { get; set; } = new Guid();
        
        [Required, MaxLength(255)]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        public string Password {
            get => password;
            set => password = Auth.HashPassword(value);
        }

        private string password;

        [MaxLength(255)]
        public string Email { get; set; }
    }
}
