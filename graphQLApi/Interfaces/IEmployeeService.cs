using graphQLApi.Models;

namespace graphQLApi.Interfaces;

public interface IEmployeeService
{
    IEnumerable<Employee> GetAll();
    Employee GetById(int id);
}
