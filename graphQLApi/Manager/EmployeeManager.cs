using graphQLApi.Interfaces;
using graphQLApi.Models;

namespace graphQLApi.Manager;

public class EmployeeManager : IEmployeeManager
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    public EmployeeManager(IEmployeeService employeeService, IDepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public IEnumerable<EmployeeDetails> GetAllEmployees()
    {
        return _employeeService.GetAll().Select(
                e => new EmployeeDetails()
                {
                    EmpId = e.empId,
                    EmpName = e.empName,
                    DeptId = e.deptId,
                    DeptName = _departmentService.GetById(e.deptId)?.deptName

                }
            );
    }

    public EmployeeDetails? GetEmployeeById(int id)
    {
        var e = _employeeService.GetById(id);

        return new EmployeeDetails()
        {
            EmpId = e.empId,
            EmpName = e.empName,
            DeptId = e.deptId,
            DeptName = _departmentService.GetById(e.deptId)?.deptName
        };

        //return t;
    }
}
