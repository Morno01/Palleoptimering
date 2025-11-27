using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Placering")]
    public class Placering
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Palle")]
        public int PalleId { get; set; }

        [Required]
        [ForeignKey("Element")]
        public int ElementId { get; set; }

        [Required]
        public int Lag { get; set; }

        [Required]
        public bool ErRoteret { get; set; } = false;

        public int? MellemrumsAfstand { get; set; }

        // Navigation properties
        public virtual Palle Palle { get; set; }
        public virtual Element Element { get; set; }
    }
}