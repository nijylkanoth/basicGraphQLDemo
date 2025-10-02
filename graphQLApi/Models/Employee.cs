namespace graphQLApi.Models;

public record Employee(int empId, string empName, int deptId);

public record Department(int deptId, string deptName);

public class EmployeeDetails
{
    public int EmpId { get; set; }

    public string EmpName { get; set; }

    public int DeptId { get; set; }
    public string DeptName { get; set; }


}





