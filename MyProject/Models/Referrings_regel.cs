using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Referrings_regel")]
    public class Referrings_regel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Palle")]
        public int PalleId { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal? HøjdeBreddeRatio { get; set; }

        public bool? KortPåErHvileElementer { get; set; }

        // Navigation property
        public virtual Palle Palle { get; set; }
    }
}