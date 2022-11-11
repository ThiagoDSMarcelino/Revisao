using System;
using System.Linq;

var df = "Data/INFLUD21-07-11-2022.csv"
        .Open()
        .Read();

// var query = df
//             .Where(casoCovid => casoCovid.IsCovid)
//             .GroupBy(casoCovid => casoCovid.NumberVacs)
//             .Select(group => new {
//                 QtdVacs = group.Key,
//                 letalidade = group.Average(casoCovid => casoCovid.IsDead ? 1.0 : 0.0)
//             })
//             .OrderBy(v => v.QtdVacs);

// foreach (var item in query)
//     Console.WriteLine($"{item.QtdVacs} -- {item.letalidade * 100}");

var query2 = df
            .Where(c => c.NumberVacs > 0)
            .GroupBy(c => c.Vaccine)
            .Select(g => new {
                VacName = g.Key,
                QtdVac = g.Count(),
                Lethality = g.Average(c => c.IsDead ? 1.0 : 0.0)
            });

Console.WriteLine("\nVacina por morte");
foreach (var cl in query2)
    Console.WriteLine($"VacName: {cl.VacName} Quantidade de vacinas: {cl.QtdVac} Taxa de letalidade: {cl.Lethality * 100}%");