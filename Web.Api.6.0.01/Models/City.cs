public class City
{
    public int ID { get; set; }
    public string CityName { get; set; }

    public int PostalCode { get; set; }

    public int CountryID { get; set; }

    public List<Entry> Entries { get; set; }

    public Country Country { get; set; }

    return PhonebookContext.Applications
     .Include(a => a.Entry.Select(c => c.EntryRelationshipType));
}

