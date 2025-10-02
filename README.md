# basicGraphQLDemo



<br> 

* #### Install Required Packages:

    In your .NET Core project, install the following 3 packages: <br>
  
    **graphiql Version="2.0.0"** <br>
    **GraphQL.Server.All" Version="8.3.1"** <br>
    **GraphQL.Server.Transports.AspNetCore" Version="8.3.1"**
  
  <br>


* #### Define the necessary models:


       public record Employee(int empId, string empName, int deptId);

       public record Department(int deptId, string deptName);

       public class EmployeeDetails
       {
       public int EmpId { get; set; }

       public string EmpName { get; set; }

       public int DeptId { get; set; }
       public string DeptName { get; set; }


       }



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

  <br> 

* #### Create the EmployeeService and DepartmentService which uses the hard-coded data, below is the code:


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


  <br> 


* #### Create the EmployeeManager Manager class, which uses the EmployeeService and DepartmentService and the below code to it:


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

        
       }
       }


  <br> 

* #### Create the GraphQL Query which is the key for GraphQL APIs. Add a class EmployeeDetailsQuery.cs and the below code to it:


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


       public class EmployeeDetailsSchema : Schema
       {
       public EmployeeDetailsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
       {
        Query = serviceProvider.GetRequiredService<EmployeeDetailsQuery>();
       }
       }

<br>

* #### Register the services and types including GraphQL to the dependency container in Program.cs class:

       var builder = WebApplication.CreateBuilder(args);

       // Add services to the container.
       builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
       builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
       builder.Services.AddSingleton<IEmployeeManager, EmployeeManager>();


       //graphql
       builder.Services.AddSingleton<EmployeeDetailsType>();
       builder.Services.AddSingleton<EmployeeDetailsQuery>();
       builder.Services.AddSingleton<ISchema, EmployeeDetailsSchema>();
       builder.Services.AddGraphQL( b=> b.AddAutoSchema<EmployeeDetailsSchema>().AddSystemTextJson() );

       

  <br> 


* #### Register the GraphQL endpoint to the application:

       app.UseGraphiQl("/graphql");
       app.UseGraphQL<ISchema>();

       

  <br> 



