using GraphiQl;
using GraphQL;
using GraphQL.Types;
using graphQLApi.CustomSchema;
using graphQLApi.Interfaces;
using graphQLApi.Manager;
using graphQLApi.Query;
using graphQLApi.Services;
using graphQLApi.Types;

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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseGraphiQl("/graphql");
app.UseGraphQL<ISchema>();

app.MapControllers();

app.Run();
