using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Stablings_regel")]
    public class Stablings_regel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Palle")]
        public int PalleId { get; set; }

        [Required]
        public int MaxAntalLag { get; set; }

        [Required]
        public int MaxStablingHøjde { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MaxElementVægt { get; set; }

        // Navigation property
        public virtual Palle Palle { get; set; }
    }
}