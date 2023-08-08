namespace Piratas.Servidor.Testes.Cartas.ResolucaoImediata;

using System;
using System.Collections.Generic;
using Dominio;
using Dominio.Acoes;
using Dominio.Baralhos;
using Dominio.Cartas;
using Dominio.Cartas.ResolucaoImediata;
using NSubstitute;
using NUnit.Framework;

public class RumTestes
{
    private Mesa _mesa;

    public RumTestes()
    {
        var configuracaoCartas = new List<Tuple<string, int>> {new(nameof(Rum), 1)};

        GeradorCartas.Configurar(configuracaoCartas);
    }

    [SetUp]
    public void Inicializacao()
    {
        var jogadores = new List<Jogador>();

        var jogador1 = new Jogador(
            "jogador1",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var jogador2 = new Jogador(
            "jogador2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var jogador3 = new Jogador(
            "jogador3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        jogadores.Add(jogador1);
        jogadores.Add(jogador2);
        jogadores.Add(jogador3);

        _mesa = new Mesa(jogadores);
    }

    [Test]
    public void AplicarEfeitoDeveComprarCartasParaJogador()
    {
        var cartasNaMao = new List<Carta>();

        var jogadorRealizador = new Jogador(
            string.Empty,
            null,
            null,
            null,
            null);

        var cartasNoBaralhoCentral = new List<Carta> {Substitute.For<Carta>(), Substitute.For<Carta>(),};

        _mesa.BaralhoCentral.InserirTopo(cartasNoBaralhoCentral);
        jogadorRealizador.Mao.Adicionar(cartasNaMao);

        var acao = Substitute.For<BaseAcao>(jogadorRealizador, null);

        var rum = new Rum();

        rum.AplicarEfeito(acao, _mesa);

        foreach (Carta carta in cartasNoBaralhoCentral)
            Assert.IsTrue(jogadorRealizador.Mao.Possui(carta));

        Assert.IsNull(_mesa.BaralhoCentral.ObterTopo());
    }
}
