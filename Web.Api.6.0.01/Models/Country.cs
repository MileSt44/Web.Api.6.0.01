public class Country
{
    public int CityID { get; set; }
    public string CountryName { get; set; }

    public int CallNumber { get; set; }

    public List<City> Cities { get; set; }

    public City City { get; set; }

}

