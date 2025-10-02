using GraphQL.Types;
using graphQLApi.Models;

namespace graphQLApi.Types;


public class EmployeeDetailsType : ObjectGraphType<EmployeeDetails>
{
    public EmployeeDetailsType()
    {
        Field(x => x.EmpId);
        Field(x => x.EmpName);
        Field(x => x.DeptId);
        Field(x => x.DeptName);
    }

}
