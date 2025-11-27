using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Element")]
    public class Element
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Laengde { get; set; }

        [Required]
        public int Bredde { get; set; }

        [Required]
        public int Højde { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Vægt { get; set; }

        [Required]
        [MaxLength(255)]
        public string Mærke { get; set; }

        [Required]
        [MaxLength(255)]
        public string Serie { get; set; }

        // Navigation property
        public virtual ICollection<Placering> Placeringer { get; set; }
    }
}