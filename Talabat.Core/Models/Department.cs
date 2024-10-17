namespace Talabat.Core.Models;

public class Department : BaseModel
{

    public string Name { get; set; }

    public DateOnly DateOfCreation { get; set; }
    
}