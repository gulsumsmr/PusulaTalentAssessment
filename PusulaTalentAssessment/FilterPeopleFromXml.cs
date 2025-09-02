using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public static class PeopleFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
        {
            return JsonSerializer.Serialize(new
            {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        XDocument doc;
        try { doc = XDocument.Parse(xmlData); }
        catch
        {
            return JsonSerializer.Serialize(new
            {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        var filtered = doc.Descendants("Person")
            .Select(p => new
            {
                Name = (string)p.Element("Name") ?? string.Empty,
                Age = (int?)p.Element("Age") ?? 0,
                Department = (string)p.Element("Department") ?? string.Empty,
                Salary = (decimal?)p.Element("Salary") ?? 0m,
                HireDate = (DateTime?)p.Element("HireDate") ?? DateTime.MinValue
            })
            .Where(p => p.Age > 30
                        && p.Department == "IT"
                        && p.Salary > 5000m
                        && p.HireDate < new DateTime(2019, 1, 1)) 
            .ToList();

        var names = filtered.Select(x => x.Name).OrderBy(x => x).ToArray();
        var count = filtered.Count;
        var total = filtered.Sum(x => x.Salary);
        var avg = count > 0 ? Math.Round(filtered.Average(x => x.Salary), 2) : 0m;
        var max = count > 0 ? filtered.Max(x => x.Salary) : 0m;

        var result = new
        {
            Names = names,
            TotalSalary = total,
            AverageSalary = avg,
            MaxSalary = max,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}
