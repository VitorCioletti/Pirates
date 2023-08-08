namespace Piratas.Servidor.Dominio.Baralhos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartas;

public static class GeradorCartas
{
    private static List<Tuple<string, int>> _configuracaoCartas;

    public static void Configurar(List<Tuple<string, int>> configuracaoCartas)
    {
        _configuracaoCartas = configuracaoCartas;
    }

    public static List<Carta> Gerar()
    {
        var cartas = new List<Carta>();

        foreach ((string nomeCarta, int quantidadeCarta) in _configuracaoCartas)
        {
            IEnumerable<Carta> novasCartas = _instanciarCartas(nomeCarta, quantidadeCarta);

            cartas.AddRange(novasCartas);
        }

        return cartas;
    }

    private static IEnumerable<Carta> _instanciarCartas(string nomeCarta, int quantidadeCarta)
    {
        var cartas = new List<Carta>();

        if (quantidadeCarta == 0)
            return cartas;

        for (int i = 0; i < quantidadeCarta; i++)
        {
            Carta carta = _instanciar(nomeCarta);

            cartas.Add(carta);
        }

        return cartas;
    }

    private static Carta _instanciar(string nomeCarta)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        Type tipoCarta = executingAssembly.GetTypes().FirstOrDefault(t => t.Name == nomeCarta);

        if (tipoCarta is null)
            throw new InvalidOperationException($"Carta \"{nomeCarta}\" não encontrada.");

        var carta = (Carta)Activator.CreateInstance(tipoCarta);

        return carta;
    }
}
