using graphQLApi.Interfaces;
using graphQLApi.Models;

namespace graphQLApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly List<Employee> _employees = new();

    public EmployeeService()
    {
        _employees = GetEmployees();
    }

    private List<Employee> GetEmployees()
    {
        return new List<Employee>() { 
            new(empId:1, deptId:100, empName:"Tom"),
            new(empId:2, deptId:200, empName:"Jerry"),
            new(empId:3, deptId:300, empName:"Mickey"),
            new(empId:4, deptId:200, empName:"Minnie"),
            new(empId:5, deptId:300, empName:"Donald")
        };
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employees;
    }

    public Employee? GetById(int id) => _employees.FirstOrDefault(x=> x.empId == id);

}
