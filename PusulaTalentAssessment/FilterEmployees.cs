using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class EmployeeFilter
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
        {
            return JsonSerializer.Serialize(new
            {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        var threshold = new DateTime(2017, 1, 1);

        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000m && e.Salary <= 9000m)
            .Where(e => e.HireDate >= threshold)
            .ToList();

        var names = filtered.Select(e => e.Name)
                            .OrderByDescending(n => n.Length)
                            .ThenBy(n => n)
                            .ToArray();

        var count = filtered.Count;
        var total = filtered.Sum(e => e.Salary);
        var avg = count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0m;
        var min = count > 0 ? filtered.Min(e => e.Salary) : 0m;
        var max = count > 0 ? filtered.Max(e => e.Salary) : 0m;

        var result = new
        {
            Names = names,
            TotalSalary = total,
            AverageSalary = avg,
            MinSalary = min,
            MaxSalary = max,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}
