namespace PhoneBook.Models
{
    public class EntryDto
    {
        public int PersonID { get; set; }
        public string NameLastName { get; set; }
        public string Street { get; set; }
        public string CompanyName { get; set; }
        public int PhoneNumber { get; set; }
    }
}