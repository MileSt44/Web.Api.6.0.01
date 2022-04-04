public class Country
{
    public int ID { get; set; }
    public string CountryName { get; set; }

    public int CallNumber { get; set; }

    public List<City> Cities { get; set; }

    return PhonebookContext.Applications
     .Include(a => a.City.Select(c => c.CityRelationshipType));    //ovo me navjise muci, izgubia sam masu vremena na ovome pls halp XD
}

