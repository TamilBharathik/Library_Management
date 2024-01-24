using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class TLib
    {
        [Key]
        public string? Username { get; set; }

        public string? Pwd { get; set; }
    }
}
