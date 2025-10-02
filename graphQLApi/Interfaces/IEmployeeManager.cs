using graphQLApi.Models;

namespace graphQLApi.Interfaces;

public interface IEmployeeManager
{
    IEnumerable<EmployeeDetails> GetAllEmployees();
    EmployeeDetails? GetEmployeeById(int id);
}
