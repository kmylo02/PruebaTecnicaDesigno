using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entity
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El primer nombre no debe contener números")]
        public string PrimerNombre { get; set; }

        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El segundo nombre no debe contener números")]
        public string SegundoNombre { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El primer apellido no debe contener números")]
        public string PrimerApellido { get; set; }

        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El segundo apellido no debe contener números")]
        public string SegundoApellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
        public decimal Sueldo { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }
    }
}

