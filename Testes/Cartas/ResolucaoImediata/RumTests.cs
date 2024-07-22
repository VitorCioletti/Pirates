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

public class RumTests
{
    private Table _table;

    public RumTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 1)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        var player1 = new Player(
            "player1",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player2 = new Player(
            "player2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player3 = new Player(
            "player3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        players.Add(player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustBuyCardsToPlayer()
    {
        var cardsAtHand = new List<Card>();

        var starterPlayer = new Player(
            string.Empty,
            null,
            null,
            null,
            null);

        var cardsOnCentralDeck = new List<Card> {Substitute.For<Card>(), Substitute.For<Card>(),};

        _table.CentralDeck.PushTop(cardsOnCentralDeck);
        starterPlayer.Hand.Add(cardsAtHand);

        var action = Substitute.For<BaseAction>(starterPlayer, null);

        var rum = new Rum();

        rum.ApplyEffect(action, _table);

        foreach (Card card in cardsOnCentralDeck)
            Assert.IsTrue(starterPlayer.Hand.Exists(card));

        Assert.IsNull(_table.CentralDeck.GetTop());
    }
}
