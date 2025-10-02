using graphQLApi.Models;

namespace graphQLApi.Interfaces;

public interface IDepartmentService
{
    IEnumerable<Department> GetAll();
    Department GetById(int id);
}
