using graphQLApi.Interfaces;
using graphQLApi.Models;

namespace graphQLApi.Services;

public class DepartmentService : IDepartmentService
{
    private readonly List<Department> _departments = new();

    public DepartmentService()
    {
        _departments = GetDepartments();
    }

    public IEnumerable<Department> GetAll()
    {
        return _departments;
    }

    public Department? GetById(int id) => _departments.FirstOrDefault(x=> x.deptId == id);


    private List<Department> GetDepartments()
    {
        return new List<Department>() {
            new(deptId:100, deptName:"Sales"),
            new(deptId:200, deptName:"IT"),
            new(deptId:300, deptName:"Finance")
        };
    }

}
