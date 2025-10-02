using GraphQL;
using GraphQL.Types;
using graphQLApi.Interfaces;
using graphQLApi.Types;

namespace graphQLApi.Query;

public class EmployeeDetailsQuery : ObjectGraphType
{
    public EmployeeDetailsQuery(IEmployeeManager employeeManager)
    {

        Field<ListGraphType<EmployeeDetailsType>>("employees").Resolve(context => { return employeeManager.GetAllEmployees(); });

        Field<EmployeeDetailsType>("employee").Arguments(
            new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "id" }
                )
            ).Resolve( 
                context =>
                {
                    return employeeManager.GetEmployeeById(context.GetArgument<int>("id"));
                }
           );

    }
}
