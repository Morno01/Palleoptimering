using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Bruger")]
    public class Bruger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Brugernavn { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Rolle { get; set; }

        // Navigation property
        public virtual ICollection<Palle> Paller { get; set; }
    }
}