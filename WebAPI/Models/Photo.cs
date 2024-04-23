using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.IEntity;

namespace WebAPI.Models
{
    [Table("Photos")]
    public class Photo : IBaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string PublicId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

    }
}