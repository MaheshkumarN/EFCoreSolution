using static System.Console;

using EFCoreConApp;
using EFCoreConApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;


#region Default
//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!"); 
#endregion

static IConfiguration BuildConfiguration(string[] args)
{
  IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

  return configuration;
}

static ServiceProvider BuildConfigureService(IConfiguration configuration)
{
  ServiceCollection services = new ServiceCollection();

  services.AddDbContext<MultiDbContext>(cfg => {
    cfg.UseSqlServer(configuration["ConnectionStrings:MultiDbConnection"], sqlOptions => {
      sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
    });
    cfg.UseLoggerFactory(loggerFactory: LoggerFactory.Create(cfg => cfg.AddConsole()));
    cfg.EnableSensitiveDataLogging();
  });

  //services.AddScoped();
  return services.BuildServiceProvider();
}

IConfiguration configuration = BuildConfiguration(args);
ServiceProvider serviceProvider = BuildConfigureService(configuration);

//Printer.Print($"{configuration["ConnectionStrings:MultiDbConnection"]}", "ConnectionString");

MultiDbContext context = serviceProvider.GetRequiredService<MultiDbContext>();

#region Deferred Execution
//var result = context.Dept;
//result.Print("Dept Table Data");

//var result = context.Dept.ToList();
//result.Print("Dept Table Data");
#endregion

#region Simple Queries

#region 01. Find all Employees whose Sal is between any of the range of 1000, 2000 and 3000
//var result = (from e in context.Emp
//              where e.Sal >= 1000 && e.Sal <=2000 || e.Sal == 3000
//              select e).ToList();

//var result = context.Emp.Where(e=>((e.Sal >=1000 && e.Sal<=2000) || e.Sal == 3000)).Select(e => e).ToList();

//result.Print("Employees whose Sal is between any of the range of 1000, 2000 and 3000");

#endregion

#region 02. Find all Employees who fall in any of the DeptNo 10 and 20
//var result = (from e in context.Emp
//              where e.DeptNo == 10 || e.DeptNo == 20
//              select e).ToList();

//var result1 = context.Emp.Where(e => e.DeptNo == 10 || e.DeptNo == 20).Select(e => e).ToList();

//result.Print("Employees who fall in any of the DeptNo 10 and 20");
#endregion

#region 03. Find the Employee named 'Allen' or who is working in DeptNo 10
//var result = (from e in context.Emp
//              where e.EName.Equals("Allen") || e.DeptNo == 10
//              select e).ToList();

//result.Print("Employee named 'Allen' or who is working in DeptNo 10");
#endregion

#region 04. Find the Employee Comm is greater the his Sal

//var result = (from e in context.Emp
//              where e.Comm > e.Sal
//              select e).ToList();

//result.Print("Employee Comm is greater the his Sal");

#endregion

#region 05. Find all Employees who have completed 40 yrs with the company
//var result = (from e in context.Emp
//              select new { DateOfJoining = e.HireDate, Epx = (DateTime.Now.Year - e.HireDate.Year) }).ToList();

//var result = context.Emp.Where(e => (DateTime.Now.Year - e.HireDate.Year) > 40).Select(e => e).ToList();

//result.Print("Employees who have completed 40 yrs with the company");
#endregion

#endregion

#region GroupBy

#region 01. Find the No Of Employees in each DeptNo
//var result = from e in context.Emp
//             group e by e.DeptNo into grp
//             select new { DeptNo = grp.Key, NoOfEmp = grp.Count() };

//var result = (from e in context.Emp
//             group e by e.DeptNo into grp
//             select new { DeptNo = grp.Key, NoOfEmp = grp.Count() }).ToList();


//var result = context.Emp.GroupBy(e => e.DeptNo).Select(e => new { DeptNo = e.Key, NoOfEmp = e.Count() }).ToList();

//result.Print("No Of Employees in each DeptNo");

//var result = (from e in context.Emp.ToList()
//             group e by e.DeptNo).ToList();

//Printer.Print($"{result.GetType()}", "Check");

//foreach (var item in result)
//{
//  WriteLine($"DeptNo: {item.Key} -- NoOfEmp: {item.Count()}");
//}

//foreach (var item in result)
//{
//  WriteLine($"DeptNo: {item.Key} -- NoOfEmp: {item.Count()}");
//  int ctr = 1;
//  foreach (var e in item)
//  {
//    WriteLine($"{ctr++} -- {e}");
//  }
//}


#endregion

#region 02. Find the No of Job in each DeptNo
//var result = from e in context.Emp
//             group e by new { e.DeptNo, e.Job } into grp
//             select new { DeptNo = grp.Key, NoOfJobs = grp.Count() };


//result.Print("No of Job in each DeptNo");
#endregion

#region 03. Find the Avg(Sal), Min(Sal), Max(Sal) for each DeptNo
//var resultAvg = context.Emp.Where(e => e.DeptNo == 10).Select(e => e.Sal).Average(e=>e);
//Printer.Print($"Avg Sal in DeptNo 10 is: {resultAvg}");

//var result = from e in context.Emp
//             group e by e.DeptNo into grp
//             select new { DeptNo = grp.Key, AvgSal = grp.Average(e => e.Sal), MinSal = grp.Min(e => e.Sal), MaxSal = grp.Max(e => e.Sal) };

//var result1 = context.Emp.GroupBy(e=>e.DeptNo).Select(e => new { DeptNo = e.Key, AvSal = e.Average(e=>e.Sal) });

//result.Print("Avg(Sal), Min(Sal), Max(Sal) for each DeptNo");

#endregion

#region 04. Find the Min(Sal), Max(Sal) for Job as 'Salesman' for each DeptNo

//var result = from e in context.Emp
//             where e.Job == "Salesman"
//             group e by e.DeptNo into grp
//             select new { DeptNo = grp.Key, MinSal = grp.Min(e => e.Sal), MaxSal = grp.Max(e => e.Sal) };

//result.Print("Min(Sal), Max(Sal) for Job as 'Salesman' for each DeptNo");

#endregion

#region 05. Find the total Sal payed for each DeptNo
//var result = from e in context.Emp
//             group e by e.DeptNo into grp
//             select new { DeptNo = grp.Key, Count = grp.Count(), SumSal = grp.Sum(e => e.Sal) };

//result.Print("Total Sal payed for each DeptNo");
#endregion

#region 06. Find all DeptNo which has more than one Employee having the same Job
//var result = from e in context.Emp
//             group e by new { e.DeptNo, e.Job } into grp
//             where grp.Count() > 1
//             select new { DeptNoJob =  grp.Key, Count = grp.Count()};

//result.Print("DeptNo which has more than one Employee having the same Job");
#endregion

#endregion

#region Joins
#region 01. Display EName, Job, DeptNo, DName and Loc for each Employee
//var result = from e in context.Emp
//             join d in context.Dept
//             on e.DeptNo equals d.DeptNo
//             select new { EName = e.EName, JOb = e.Job, DeptNo = e.DeptNo, DName = d.DName, Loc = d.Loc };

//var result1 = context.Emp.Join(context.Dept, e => e.DeptNo, d => d.DeptNo, (e, d) => new { EName = e.EName, JOb = e.Job, DeptNo = e.DeptNo, DName = d.DName, Loc = d.Loc });

//result.Print("EName, Job, DeptNo, DName and Loc for each Employee");
#endregion

#region 02. Display EName, DName of all Employees having a salary greater than 2000
//var result = from e in context.Emp
//             join d in context.Dept
//             on e.DeptNo equals d.DeptNo
//             where e.Sal == 2000
//             select new { EName = e.EName, JOb = e.Job, DeptNo = e.DeptNo, DName = d.DName, Loc = d.Loc };

//var result1 = context.Emp.Where(e => e.Sal > 2000).Join(context.Dept, e => e.DeptNo, d => d.DeptNo, (e, d) => new { EName = e.EName, JOb = e.Job, DeptNo = e.DeptNo, DName = d.DName, Loc = d.Loc });

//result.Print("EName, DName of all Employees having a salary greater than 2000");
#endregion

#region 03. Display all details from both Emp and Dept those matching and not matching
//var result = from d in context.Dept
//             join e in context.Emp
//             on d.DeptNo equals e.DeptNo into joinEmpDept
//             from j in joinEmpDept.DefaultIfEmpty()
//             select new { EName = j.EName != null ? j.EName : null, EDeptNo = j.DeptNo != null ? j.DeptNo : 0, DName = d.DName, Loc = d.Loc };

//result.Print("Details from both Emp and Dept those matching and not matching");
#endregion

#endregion

#region Sub Query

#region 01. Find all Employees working with 'King'
var result = from e in context.Emp
             where e.DeptNo == (from k in context.Emp where k.EName == "King" select k.DeptNo).FirstOrDefault()
             select e;

result.Print("All Employees working with 'King'");
#endregion


#region 02. Find who earns the Max(Sal) in the Company
//var result = from e in context.Emp
//             where e.Sal == (from s in context.Emp select s.Sal).Max()
//             select e;

//var result = context.Emp.Where(e => e.Sal == (context.Emp.Max(s => s.Sal))).Select(e => e);

//result.Print("Who earns the Max(Sal) in the company");
#endregion

#region 03. Display all the details for the Employees who are working in 'Chicago'
//var result = context.Dept.Select(d => d);

//var result = from e in context.Emp
//             where e.DeptNo == (from d in context.Dept where d.Loc == "Chicago" select d.DeptNo).First()
//             select e;

//var result = context.Emp.Where(e => e.DeptNo == (context.Dept.Where(d => d.Loc == "Chicago").Select(d => d.DeptNo).First()));

//var result = context.Emp.Where(e=> e.DeptNo == (context.Dept.First(d=>d.Loc == "Chicago").DeptNo));

//result.Print("Employees who are working in 'Chicago'");
#endregion
#endregion
