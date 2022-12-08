using System;
using System.Linq;

double[] array = new double[]
{
    8.4, 8.6, 8.4, 7.0, 7.2, 10.0, 7.2, 9.4, 9.8
};
Console.WriteLine(mediaEspecial(array));

double mediaEspecial(double[] array)
{
    if (array.Length <= 3)
        return array.Average();
    
    array.OrderBy(i => i);

    var antes = array.Take(2).Sum();
    var depois = array.TakeLast(2).Sum();
    return (antes + depois) / 4;    
}