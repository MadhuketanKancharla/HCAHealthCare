using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HCAHealthcare.Models
{
    public class PatientDto
    {
        [Required,MaxLength(50)]
        public string Name { get; set; } = "";
        [Required,Range(0, 123)]
        public int Age { get; set; }
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public decimal Due { get; set; }
     
        public IFormFile? Image { get; set; }
    }
}

