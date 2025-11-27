using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Mellemrums_regel")]
    public class Mellemrums_regel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Palle")]
        public int PalleId { get; set; }

        [Required]
        public int MellemrumsAfstand { get; set; }

        // Navigation property
        public virtual Palle Palle { get; set; }
    }
}