using GraphQL.Types;
using graphQLApi.Query;

namespace graphQLApi.CustomSchema;

public class EmployeeDetailsSchema : Schema
{
    public EmployeeDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<EmployeeDetailsQuery>();
    }
}
