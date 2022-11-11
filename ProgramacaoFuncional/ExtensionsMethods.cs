using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static IEnumerable<string> Open(this string path)
    {
        var stream = new StreamReader(path);

        while (!stream.EndOfStream)
            yield return stream.ReadLine();

        stream.Close();
    }
    public static IEnumerable<CasoCovid> Read(this IEnumerable<string> coll)
    {
        var it = coll.GetEnumerator();

        var header = it.MoveNext() ? it.Current.Replace("\"", "").Split(";").ToList() : null;
        if (header == null)
            throw new Exception("CSV vazio");
        
        string[][] allVacs = new string[][]
        {
            new string[] { "CORONAVAC", "CAR", "CO", "CRO", "BUTANTAN", "BU", "BT", "BI", "TANTAN" },
            new string[] { "ASTRAZENECA", "AZ", "AS", "ATRA", "FIOCRUZ", "FI", "CRUZ", "OXFORD", "OX", "0X", "ORF", "ORO", "OSF", "OKF", "EXF", "OSWALDO", "OSVALDO" },
            new string[] { "PFIZER", "PHI", "PF", "FI", "PZ", "PI", "FAI", "FHI", "PLI", "PTI", "PHY", "FEI", "PAI", "PRI" },
            new string[] { "JANSSEN", "JANS", "JAN", "JENS", "JONS", "JHAN", "JONH", "JOHS", "JOHN", "JHON", "JAHN", "JAHS" },
            new string[] { "SINOVAC", "SIN", "SIN" },
            new string[] { "INDIA" }
        };

        while (it.MoveNext())
        {
            var data = it.Current.Replace("\"", "").Split(";");
            CasoCovid newCasoCovid = new CasoCovid();

            newCasoCovid.IsCovid = data[header.IndexOf("CLASSI_FIN")] == "5";
            newCasoCovid.IsDead = data[header.IndexOf("EVOLUCAO")] == "2";

            if (data[header.IndexOf("FAB_COV_1")] != "")
            {
                var vac = allVacs.FirstOrDefault(vacs =>
                    vacs.Any(el => data[header.IndexOf("FAB_COV_1")].Contains(el, StringComparison.OrdinalIgnoreCase)));
                
                if (vac != null)
                {
                    switch (vac[0])
                    {
                        case "CORONAVAC":
                            newCasoCovid.Vaccine = Vaccines.Coronavac;
                            break;
                        
                        case "ASTRAZENECA":
                            newCasoCovid.Vaccine = Vaccines.Astrazeneca;
                            break;
                        
                        case "PFIZER":
                            newCasoCovid.Vaccine = Vaccines.Pfizer;
                            break;
                        
                        case "SINOVAC":
                            newCasoCovid.Vaccine = Vaccines.Sinovac;
                            break;
                        
                        case "JANSSEN":
                            newCasoCovid.Vaccine = Vaccines.Janssen;
                            break;
                        
                        case "INDIA":
                            newCasoCovid.Vaccine = Vaccines.India;
                            break;
                    }
                }
            }

            if (data[header.IndexOf("DOSE_1_COV")] != "")
                newCasoCovid.NumberVacs++;
            if (data[header.IndexOf("DOSE_2_COV")] != "")
                newCasoCovid.NumberVacs++;
            if (data[header.IndexOf("DOSE_REF")] != "")
                newCasoCovid.NumberVacs++;
            
            yield return newCasoCovid;
        }
    }
}