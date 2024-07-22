namespace Piratas.Servidor.Testes;

using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using Dominio.Acoes;
using Dominio.Acoes.Imediata;
using Dominio.Acoes.Primaria;
using Dominio.Acoes.Resultante;
using Dominio.Acoes.Resultante.Base;
using Dominio.Acoes.Resultante.Enums;
using Dominio.Baralhos;
using Dominio.Cartas.Duelo;
using Dominio.Cartas.ResolucaoImediata;
using Dominio.Cartas.Tesouro;
using Dominio.Excecoes.Mesa;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

public class MesaTestes
{
    private Table _table;

    public MesaTestes()
    {
        var configuracaoCartas = new List<Tuple<string, int>> {new(nameof(Rum), 100)};

        CardsGenerator.Configure(configuracaoCartas);
    }

    [SetUp]
    public void Inicializacao()
    {
        var jogadores = new List<Player>();

        var jogador1 = new Player(
            "jogador1",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var jogador2 = new Player(
            "jogador2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var jogador3 = new Player(
            "jogador3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        jogadores.Add(jogador1);
        jogadores.Add(jogador2);
        jogadores.Add(jogador3);

        _table = new Table(jogadores);
    }

    [Test]
    public void TodosJogadoresDevemPossuirCartasAoCriarMesa()
    {
        bool todosPossuemCartas = _table.Players.All(j => j.Hand.GetCardQuantity() > 0);

        Assert.True(todosPossuemCartas);
    }

    [Test]
    public void BaralhoCentralDevePossuirCartasAoCriarMesa()
    {
        bool possuiCartas = _table.CentralDeck.CardsAmount > 0;

        Assert.True(possuiCartas);
    }

    [Test]
    public void PilhaDescarteDeveEstarVaziaAoCriarMesa()
    {
        bool estaVazia = _table.DiscardDeck.CardsAmount == 0;

        Assert.True(estaVazia);
    }

    [Test]
    public void JogadorInicialDevePossuirAcoesPrimariasDisponiveis()
    {
        var acoesDisponiveisEsperadas = new List<BasePrimaryAction>
        {
            new Duel(null, null, null), new DrawCard(null, null), new BuyCard(null)
        };

        List<BaseAction> acoesDisponiveis = _table.ActionsAvailableToPlayers[_table.CurrentPlayer];

        bool possuiTodasEsperadas =
            acoesDisponiveisEsperadas.All(pe => acoesDisponiveis.Exists(po => po.GetType() == pe.GetType()));

        Assert.IsTrue(possuiTodasEsperadas);
    }

    [Test]
    public void JogadorInicialDeveConseguirComprarCarta()
    {
        Player playerAtual = _table.CurrentPlayer;

        int quantidadeCartasAntesCompra = playerAtual.Hand.GetCardQuantity();
        int quantidadeAcoesDisponiveisAntesCompra = playerAtual.AvailableActions;

        int quantidadeCartasEsperada = quantidadeCartasAntesCompra + 1;
        int quantidadeAcoesDisponiveisEsperada = quantidadeAcoesDisponiveisAntesCompra - 1;

        List<BaseAction> acoesDisponiveis = _table.ActionsAvailableToPlayers[playerAtual];

        BaseAction comprarCarta = acoesDisponiveis.First(a => a is BuyCard);

        _table.ProcessAction(comprarCarta);

        Assert.AreEqual(quantidadeCartasEsperada, playerAtual.Hand.GetCardQuantity());
        Assert.AreEqual(quantidadeAcoesDisponiveisEsperada, playerAtual.AvailableActions);
    }

    [Test]
    public void JogadorNaoExecutaAcaoPrimariaForaTurno()
    {
        Player proximoPlayer = _table.Players[1];

        Assert.Throws<OtherPlayerTurnException>(ProcessarAcao);

        void ProcessarAcao()
        {
            _table.ProcessAction(new BuyCard(proximoPlayer));
        }
    }

    [Test]
    public void JogadorNaoExecutaResultanteNaoEsperada()
    {
        Player playerAtual = _table.Players[0];

        var acaoOrigem = new DrawCard(playerAtual, new Cannon());

        var resultante = new DiscardCard(
            acaoOrigem,
            playerAtual,
            playerAtual,
            new List<string>());

        Assert.Throws<UnexpectedResultantAction>(ProcessarAcao);

        void ProcessarAcao()
        {
            _table.ProcessAction(resultante);
        }
    }

    [Test]
    public void DeveMudarJogadorAtualAposAnteriorJogar()
    {
        foreach (Player jogador in _table.Players)
        {
            while (jogador.AvailableActions > 0)
            {
                List<BaseAction> acoesDisponiveis = _table.ActionsAvailableToPlayers[jogador];

                BaseAction comprarCarta = acoesDisponiveis.First(a => a is BuyCard);

                jogador.Hand.Remove(jogador.Hand.GetAny());

                _table.ProcessAction(comprarCarta);
            }
        }

        Assert.Pass();
    }

    [Test]
    public void JogadorDeveGanharSePossuirTesourosSuficientes()
    {
        Player primeiroPlayer = _table.CurrentPlayer;
        Player playerVencedor = _table.Players[1];

        playerVencedor.Hand.Add(new Treasure(5));

        while (primeiroPlayer.AvailableActions > 0)
        {
            List<BaseAction> acoesDisponiveis = _table.ActionsAvailableToPlayers[primeiroPlayer];

            BaseAction comprarCarta = acoesDisponiveis.First(a => a is BuyCard);

            primeiroPlayer.Hand.Remove(primeiroPlayer.Hand.GetAny());

            _table.ProcessAction(comprarCarta);
        }

        Assert.AreEqual(playerVencedor, _table.Winner);
    }

    [Test]
    public void DeveEntrarEmModoDuelo()
    {
        Assert.IsFalse(_table.InDuel);

        _table.EnterDuelMode();

        Assert.IsTrue(_table.InDuel);
    }

    [Test]
    public void DeveSairEmModoDuelo()
    {
        _table.EnterDuelMode();

        Assert.IsTrue(_table.InDuel);

        _table.EndDuelMode();

        Assert.IsFalse(_table.InDuel);
    }

    [Test]
    public void DeveLancarExcecaoSeEmDuelo()
    {
        _table.EnterDuelMode();

        Assert.Throws<InDuelException>(_table.EnterDuelMode);
    }

    [Test]
    public void DeveLancarExcecaoSeNaoEstaEmDuelo()
    {
        Assert.Throws<NoDuelException>(_table.EndDuelMode);
    }

    [Test]
    public void JogadorInicialDeveConseguirExecutarAcaoPrimaria()
    {
        Player playerInicial = _table.CurrentPlayer;

        int quantidadeCartas = playerInicial.Hand.GetCardQuantity();
        int quantidadeAcoesDisponiveis = playerInicial.AvailableActions;

        var comprarCarta = new BuyCard(playerInicial);

        _table.ProcessAction(comprarCarta);

        Assert.IsTrue(quantidadeCartas < playerInicial.Hand.GetCardQuantity());
        Assert.IsTrue(quantidadeAcoesDisponiveis > playerInicial.AvailableActions);
    }

    [Test]
    public void ExecutarTodasPrimariasDeveMudarJogadorAtual()
    {
        Player playerInicial = _table.CurrentPlayer;
        int turnoInicial = _table.CurrentTurn;

        int quantidadeAcoesDisponiveis = playerInicial.AvailableActions;

        for (int i = 0; i < quantidadeAcoesDisponiveis; i++)
        {
            var comprarCarta = new BuyCard(playerInicial);

            _table.ProcessAction(comprarCarta);
        }

        Assert.AreNotEqual(playerInicial, _table.CurrentPlayer);
        Assert.IsTrue(turnoInicial < _table.CurrentTurn);
    }

    [Test]
    public void DeveLevantarErroAoJogarForaDoTurno()
    {
        Assert.Throws<OtherPlayerTurnException>(ComprarCartaForaTurno);

        void ComprarCartaForaTurno()
        {
            Player player = _table.Players[1];

            var comprarCarta = new BuyCard(player);

            _table.ProcessAction(comprarCarta);
        }
    }

    [Test]
    public void DeveRegistrarEExecutarImediata()
    {
        Player playerAtual = _table.CurrentPlayer;

        bool primariaExecutada = false;
        bool imediataExecutada = false;

        var primaria = Substitute.For<BasePrimaryAction>(playerAtual, null);
        var imediata = Substitute.For<BaseImediate>(playerAtual, null);

        primaria.When(i => i.ApplyRule(_table)).Do(AoAplicarRegraPrimaria);
        imediata.When(i => i.AplicarRegra(_table)).Do(AoAplicarRegraImediata);

        _table.ProcessAction(primaria);

        Assert.IsTrue(primariaExecutada && imediataExecutada);

        void AoAplicarRegraPrimaria(CallInfo _)
        {
            _table.RegisterImmediateAfterResultants(imediata);

            primariaExecutada = true;
        }

        void AoAplicarRegraImediata(CallInfo _)
        {
            imediataExecutada = true;
        }
    }

    [Test]
    public void AcaoPrimariaDeveRetornarAcaoResultanteParaJogador()
    {
        Player playerAtual = _table.CurrentPlayer;

        bool primariaExecutada = false;
        bool resultanteExecutada = false;

        var primaria = Substitute.For<BasePrimaryAction>(playerAtual, null);

        var resultante = Substitute.For<BaseResultant>(
            primaria,
            playerAtual,
            ChoiceType.Action,
            null);

        var acoesResultantesEsperadas = new List<BaseAction> {resultante};

        primaria.ApplyRule(_table).Returns(acoesResultantesEsperadas).AndDoes(AoAplicarRegraPrimaria);
        resultante.When(i => i.ApplyRule(_table)).Do(AoAplicarRegraResultante);

        Dictionary<Player, List<BaseAction>> resultado = _table.ProcessAction(primaria);

        Assert.IsTrue(resultado.Count > 0);

        BaseAction resultanteObtida = resultado[playerAtual].Single();

        Assert.AreEqual(acoesResultantesEsperadas[0], resultanteObtida);

        Dictionary<Player, List<BaseAction>> resultadoAcaoResultante = _table.ProcessAction(resultanteObtida);

        Assert.IsTrue(resultadoAcaoResultante.Count == 0);
        Assert.IsTrue(primariaExecutada && resultanteExecutada);

        void AoAplicarRegraPrimaria(CallInfo _)
        {
            primariaExecutada = true;
        }

        void AoAplicarRegraResultante(CallInfo _)
        {
            resultanteExecutada = true;
        }
    }
}
