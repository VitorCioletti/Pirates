namespace Piratas.Servidor.Testes;

using System;
using System.Collections.Generic;
using Dominio;
using Dominio.Cartas;
using Dominio.Cartas.Embarcacao;
using Dominio.Cartas.ResolucaoImediata;
using Dominio.Cartas.Tesouro;
using Dominio.Cartas.Tripulacao;
using NUnit.Framework;

public class JogadorTestes
{
    private Player _player;

    private List<Tuple<string, Card>> _cartasAdicionadasNaMao;

    private List<Tuple<string, Card>> _cartasRemovidasNaMao;

    private List<Tuple<string, Card>> _cartasAdicionadasNoCampo;

    private List<Tuple<string, Card>> _cartasRemovidasNoCampo;

    [SetUp]
    public void Inicializacao()
    {
        string id = Guid.NewGuid().ToString();

        _cartasAdicionadasNaMao = new List<Tuple<string, Card>>();
        _cartasRemovidasNaMao = new List<Tuple<string, Card>>();
        _cartasAdicionadasNoCampo = new List<Tuple<string, Card>>();
        _cartasRemovidasNoCampo = new List<Tuple<string, Card>>();

        _player = new Player(
            id,
            AoAdicionarCartasNaMao,
            AoRemoverCartasNaMao,
            AoAdicionarCartasNoCampo,
            AoRemoverCartasNoCampo);

        void AoAdicionarCartasNaMao(string idJogador, Card carta)
        {
            _cartasAdicionadasNaMao.Add(new Tuple<string, Card>(idJogador, carta));
        }

        void AoRemoverCartasNaMao(string idJogador, Card carta)
        {
            _cartasRemovidasNaMao.Add(new Tuple<string, Card>(idJogador, carta));
        }

        void AoAdicionarCartasNoCampo(string idJogador, Card carta)
        {
            _cartasAdicionadasNoCampo.Add(new Tuple<string, Card>(idJogador, carta));
        }

        void AoRemoverCartasNoCampo(string idJogador, Card carta)
        {
            _cartasRemovidasNoCampo.Add(new Tuple<string, Card>(idJogador, carta));
        }
    }

    [Test]
    public void DeveResetarAcoesDisponiveis()
    {
        for (int i = 0; i < _player.AvailableActions; i++)
        {
            _player.SubtractAvailableActions();
        }

        const int acoes = 10;

        _player.ResetAvailableActions(acoes);

        Assert.AreEqual(acoes, _player.AvailableActions);
    }

    [Test]
    public void DeveSubtrairAcoesDisponviveis()
    {
        int acoesEsperadas = _player.AvailableActions - 1;

        _player.SubtractAvailableActions();

        Assert.AreEqual(acoesEsperadas, _player.AvailableActions);
    }

    [Test]
    public void DeveCalcularTesouros()
    {
        var pirataNobre = new NoblePirate();

        const int tesourosMao = 2;
        const int tesourosProtegidos = 1;
        int tesourosPirataNobre = pirataNobre.Treasures;
        const int tesourosMeioAmuleto = 2;

        int tesourosEsperados = tesourosMao + tesourosProtegidos + tesourosPirataNobre + tesourosMeioAmuleto;

        _player.Hand.Add(new Treasure(tesourosMao));
        _player.Hand.Add(new HalfAmulet());
        _player.Hand.Add(new HalfAmulet());

        _player.Field.AddProtected(new Treasure(tesourosProtegidos));

        _player.Field.Add(pirataNobre);

        Assert.AreEqual(tesourosEsperados, _player.CalculateTreasurePoints());
    }

    [Test]
    public void DeveInvocarFuncaoAoAdicionarCartaMao()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);

        Assert.AreEqual(rum, _cartasAdicionadasNaMao[0].Item2);
        Assert.AreEqual(_player.Id, _cartasAdicionadasNaMao[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoRemoverCartaMao()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);
        _player.Hand.Remove(rum);

        Assert.AreEqual(rum, _cartasRemovidasNaMao[0].Item2);
        Assert.AreEqual(_player.Id, _cartasRemovidasNaMao[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoAdicionarCartaNoCampo()
    {
        var cascoAco = new IronHull();

        _player.Field.Add(cascoAco);

        Assert.AreEqual(cascoAco, _cartasAdicionadasNoCampo[0].Item2);
        Assert.AreEqual(_player.Id, _cartasAdicionadasNoCampo[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoRemoverCartaNoCampo()
    {
        var cascoAco = new IronHull();

        _player.Field.Add(cascoAco);

        int vidaTotal = cascoAco.Life;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _player.Field.DamageShip();
        }

        Assert.AreEqual(cascoAco, _cartasRemovidasNoCampo[0].Item2);
        Assert.AreEqual(_player.Id, _cartasRemovidasNoCampo[0].Item1);
    }
}
