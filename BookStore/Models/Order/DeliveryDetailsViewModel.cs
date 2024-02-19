using BookStore.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore.Models.Order
{
    public class DeliveryDetailsViewModel
    {
        [Required(ErrorMessage = "Firstname is required!")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required!")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Street is required!")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number is required!")]
        public int? Number { get; set; }
        [Required(ErrorMessage = "Zip is required!")]
        public int? Zip { get; set; }
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }
    }
}
