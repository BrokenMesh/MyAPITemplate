using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPITemplate.Model
{
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
}
