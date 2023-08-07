namespace Piratas.Servidor.Dominio.Baralhos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartas;
using Microsoft.Extensions.Configuration;

public static class GeradorCartas
{
    private static IConfigurationRoot _configuracao;

    public static void Configurar(IConfigurationRoot configuracao)
    {
        _configuracao = configuracao;
    }

    public static List<Carta> Gerar()
    {
        var cartas = new List<Carta>();

        IConfigurationSection baralho = _configuracao.GetSection("Baralho");

        IEnumerable<IConfigurationSection> tiposCartas = baralho.GetChildren();

        foreach (IConfigurationSection tipoCarta in tiposCartas)
        {
            IEnumerable<IConfigurationSection> configuracaoCartas = tipoCarta.GetChildren();

            foreach (IConfigurationSection carta in configuracaoCartas)
            {
                string nomeCarta = carta.Key;
                string quantidadeCarta = carta.Value;

                IEnumerable<Carta> novasCartas = _instanciarCartas(nomeCarta, quantidadeCarta);

                cartas.AddRange(novasCartas);
            }
        }

        return cartas;
    }

    private static IEnumerable<Carta> _instanciarCartas(string nomeCarta, string quantidadeCarta)
    {
        var cartas = new List<Carta>();

        if (int.TryParse(quantidadeCarta, out int quantidade))
        {
            if (quantidade == 0)
                return cartas;

            for (int i = 0; i < quantidade; i++)
            {
                Carta carta = _instanciar(nomeCarta);

                cartas.Add(carta);
            }
        }
        else
            throw new InvalidOperationException($"Carta \"{nomeCarta}\" mal configurada.");

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
