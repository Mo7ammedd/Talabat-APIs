using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}