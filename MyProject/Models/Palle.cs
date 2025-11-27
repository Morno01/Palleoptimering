using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Palle")]
    public class Palle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Bruger")]
        public int BrugerId { get; set; }

        [MaxLength]
        public string Beskrivelse { get; set; }

        [Required]
        public int Længde { get; set; }

        [Required]
        public int Bredde { get; set; }

        [Required]
        public int Højde { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MaxVægt { get; set; }

        [Required]
        public int MaxHøjde { get; set; }

        [Required]
        public bool Bool_Aktiv { get; set; } = true;

        // Navigation properties
        public virtual Bruger Bruger { get; set; }
        public virtual ICollection<Referrings_regel> ReferringsRegler { get; set; }
        public virtual ICollection<Mellemrums_regel> MellemrumsRegler { get; set; }
        public virtual ICollection<Stablings_regel> StablingsRegler { get; set; }
        public virtual ICollection<Placering> Placeringer { get; set; }
    }
}