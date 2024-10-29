namespace Talabat.Core.Models.Order_Aggregate;

public class Address
{
    public Address(string firstName, string lastName, string street, string city, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        Country = country;
    }

    public Address()
    {
        
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}