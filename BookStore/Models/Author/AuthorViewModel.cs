using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore.Models.Author
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "Firstname is required!")]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required!")]
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "BirthDate is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
