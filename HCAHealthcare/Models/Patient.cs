using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HCAHealthcare.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = "";
        [Range(0, 123)]
        public int Age { get; set; }
        [MaxLength(100)]
        public string Description { get; set; } = "";
        [Precision(16,2)]
        public decimal Due { get; set; }
        [MaxLength(100)]
        public string Image { get; set; } = "";
    }
}
