using LINQTut09.Shared;
using System;
using System.Linq;

namespace LINQTut09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RunJoin();
            //RunJoinQuerySyntax();
            //RunGroupJoin();
            RunGroupJoinQuerySyntax();
            Console.ReadKey();
        }

        private static void RunJoin()
        {
            var employees = Repository.LoadEmployees();
            var departments = Repository.LoadDepartment();
            var query = from emp in employees
                        join dept in departments on emp.DepartmentId equals dept.Id
                        select new { emp.FullName, dept.Name };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.FullName} [{item.Name}]");
            }
        }

        private static void RunJoinQuerySyntax()
        {
            var employees = Repository.LoadEmployees();
            var departments = Repository.LoadDepartment();
            var query = employees.Join(departments, emp => emp.DepartmentId, dept => dept.Id, (emp, dept) => new { emp.FullName, dept.Name });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.FullName} [{item.Name}]");
            }
        }


        private static void RunGroupJoin()
        {
            var employees = Repository.LoadEmployees();
            var departments = Repository.LoadDepartment();
            var empGroups = from dept in departments 
                            join emp in employees
                            on dept.Id equals emp.DepartmentId into empGroup
                            select empGroup;


            foreach (var group in empGroups)
            {
                Console.WriteLine("--------------------------------");
                foreach (var item in group)
                {
                    Console.WriteLine($"{item.FullName}");
                }
            } 
        }

        private static void RunGroupJoinQuerySyntax()
        {
            var employees = Repository.LoadEmployees();
            var departments = Repository.LoadDepartment();
            var query = departments.GroupJoin
                (employees, 
                  dept => dept.Id, 
                  emp => emp.DepartmentId, 
                  (dept, emps) => new 
                  {   
                      Department =dept.Name,
                      Employees = emps 
                  });

            foreach (var group in query)
            {
                Console.WriteLine();
                Console.WriteLine($"********************** { group.Department } ***********************");
                Console.WriteLine();
                foreach (var item in group.Employees)
                {
                    Console.WriteLine($"{item.FullName}");
                }
            }
        }
    }
}
