Mundo.GerarJogadores(500, 250, 250);

while (Mundo.Rodada < 50000)
{
    Mundo.Jogada();
}
Console.WriteLine($"Rodada: {Mundo.Rodada}");
Console.WriteLine($"Falidos: {Mundo.Falidos}");
Console.WriteLine($"Total de Moedas: {Mundo.TotalMoedas}");