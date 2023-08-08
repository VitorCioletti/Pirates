namespace Piratas.Servidor.Testes;

using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using Dominio.Acoes;
using Dominio.Acoes.Primaria;
using Dominio.Baralhos;
using Dominio.Cartas.ResolucaoImediata;
using NUnit.Framework;

public class MesaTestes
{
    private Mesa _mesa;

    public MesaTestes()
    {
        var configuracaoCartas = new List<Tuple<string, int>> {new(nameof(Rum), 100)};

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
    public void TodosJogadoresDevemPossuirCartasAoCriarMesa()
    {
        bool todosPossuemCartas = _mesa.Jogadores.All(j => j.Mao.ObterQuantidadeCartas() > 0);

        Assert.True(todosPossuemCartas);
    }

    [Test]
    public void BaralhoCentralDevePossuirCartasAoCriarMesa()
    {
        bool possuiCartas = _mesa.BaralhoCentral.QuantidadeCartas > 0;

        Assert.True(possuiCartas);
    }

    [Test]
    public void PilhaDescarteDeveEstarVaziaAoCriarMesa()
    {
        bool estaVazia = _mesa.PilhaDescarte.QuantidadeCartas == 0;

        Assert.True(estaVazia);
    }

    [Test]
    public void JogadorInicialDevePossuirAcoesPrimariasDisponiveis()
    {
        var acoesDisponiveisEsperadas = new List<BasePrimaria>
        {
            new Duelar(null, null, null), new DescerCarta(null, null), new ComprarCarta(null)
        };

        List<BaseAcao> acoesDisponiveis = _mesa.AcoesDisponiveisJogadores[_mesa.JogadorAtual];

        bool possuiTodasEsperadas =
            acoesDisponiveisEsperadas.All(pe => acoesDisponiveis.Exists(po => po.GetType() == pe.GetType()));

        Assert.IsTrue(possuiTodasEsperadas);
    }
}
