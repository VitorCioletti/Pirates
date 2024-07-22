namespace Piratas.Servidor.Dominio.Baralhos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartas;

public static class CardsGenerator
{
    private static List<Tuple<string, int>> _configuracaoCartas;

    public static void Configure(List<Tuple<string, int>> configuracaoCartas)
    {
        _configuracaoCartas = configuracaoCartas;
    }

    public static List<Card> Generate()
    {
        var cartas = new List<Card>();

        foreach ((string nomeCarta, int quantidadeCarta) in _configuracaoCartas)
        {
            IEnumerable<Card> novasCartas = _instanciarCartas(nomeCarta, quantidadeCarta);

            cartas.AddRange(novasCartas);
        }

        return cartas;
    }

    private static IEnumerable<Card> _instanciarCartas(string nomeCarta, int quantidadeCarta)
    {
        var cartas = new List<Card>();

        if (quantidadeCarta == 0)
            return cartas;

        for (int i = 0; i < quantidadeCarta; i++)
        {
            Card card = _instanciar(nomeCarta);

            cartas.Add(card);
        }

        return cartas;
    }

    private static Card _instanciar(string nomeCarta)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        Type tipoCarta = executingAssembly.GetTypes().FirstOrDefault(t => t.Name == nomeCarta);

        if (tipoCarta is null)
            throw new InvalidOperationException($"Carta \"{nomeCarta}\" não encontrada.");

        var carta = (Card)Activator.CreateInstance(tipoCarta);

        return carta;
    }
}
