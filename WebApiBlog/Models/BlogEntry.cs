using System.ComponentModel.DataAnnotations;

namespace WebApiBlog.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }
    }
}