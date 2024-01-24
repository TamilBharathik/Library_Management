using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class LibManage
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int CopiesAvailable{ get; set; }
        public int TotalCopies { get; set; }
    }
}
