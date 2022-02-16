using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }
        [Required]
     

       
        public int AuthorId { get; set; }
       
        public Author Author { get; set; }
    }
}