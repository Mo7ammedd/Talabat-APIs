namespace Talabat.Core.Models;

public class Employee : BaseModel
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public int? Age { get; set; }
    
   public Department Department { get; set; }
    
    public int DepartmentId { get; set; }
}