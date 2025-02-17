using System.ComponentModel.DataAnnotations;

namespace selemstore_test.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        // خصائص إضافية لو محتاج تضيف أكتر
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
