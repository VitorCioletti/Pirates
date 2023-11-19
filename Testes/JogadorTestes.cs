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
    private Jogador _jogador;

    private List<Tuple<string, Carta>> _cartasAdicionadasNaMao;

    private List<Tuple<string, Carta>> _cartasRemovidasNaMao;

    private List<Tuple<string, Carta>> _cartasAdicionadasNoCampo;

    private List<Tuple<string, Carta>> _cartasRemovidasNoCampo;

    [SetUp]
    public void Inicializacao()
    {
        string id = Guid.NewGuid().ToString();

        _cartasAdicionadasNaMao = new List<Tuple<string, Carta>>();
        _cartasRemovidasNaMao = new List<Tuple<string, Carta>>();
        _cartasAdicionadasNoCampo = new List<Tuple<string, Carta>>();
        _cartasRemovidasNoCampo = new List<Tuple<string, Carta>>();

        _jogador = new Jogador(
            id,
            AoAdicionarCartasNaMao,
            AoRemoverCartasNaMao,
            AoAdicionarCartasNoCampo,
            AoRemoverCartasNoCampo);

        void AoAdicionarCartasNaMao(string idJogador, Carta carta)
        {
            _cartasAdicionadasNaMao.Add(new Tuple<string, Carta>(idJogador, carta));
        }

        void AoRemoverCartasNaMao(string idJogador, Carta carta)
        {
            _cartasRemovidasNaMao.Add(new Tuple<string, Carta>(idJogador, carta));
        }

        void AoAdicionarCartasNoCampo(string idJogador, Carta carta)
        {
            _cartasAdicionadasNoCampo.Add(new Tuple<string, Carta>(idJogador, carta));
        }

        void AoRemoverCartasNoCampo(string idJogador, Carta carta)
        {
            _cartasRemovidasNoCampo.Add(new Tuple<string, Carta>(idJogador, carta));
        }
    }

    [Test]
    public void DeveResetarAcoesDisponiveis()
    {
        for (int i = 0; i < _jogador.AcoesDisponiveis; i++)
        {
            _jogador.SubtrairAcoesDisponiveis();
        }

        const int acoes = 10;

        _jogador.ResetarAcoesDisponiveis(acoes);

        Assert.AreEqual(acoes, _jogador.AcoesDisponiveis);
    }

    [Test]
    public void DeveSubtrairAcoesDisponviveis()
    {
        int acoesEsperadas = _jogador.AcoesDisponiveis - 1;

        _jogador.SubtrairAcoesDisponiveis();

        Assert.AreEqual(acoesEsperadas, _jogador.AcoesDisponiveis);
    }

    [Test]
    public void DeveCalcularTesouros()
    {
        var pirataNobre = new PirataNobre();

        const int tesourosMao = 2;
        const int tesourosProtegidos = 1;
        int tesourosPirataNobre = pirataNobre.Tesouros;
        const int tesourosMeioAmuleto = 2;

        int tesourosEsperados = tesourosMao + tesourosProtegidos + tesourosPirataNobre + tesourosMeioAmuleto;

        _jogador.Mao.Adicionar(new Tesouro(tesourosMao));
        _jogador.Mao.Adicionar(new MeioAmuleto());
        _jogador.Mao.Adicionar(new MeioAmuleto());

        _jogador.Campo.AdicionarProtegida(new Tesouro(tesourosProtegidos));

        _jogador.Campo.Adicionar(pirataNobre);

        Assert.AreEqual(tesourosEsperados, _jogador.CalcularTesouros());
    }

    [Test]
    public void DeveInvocarFuncaoAoAdicionarCartaMao()
    {
        var rum = new Rum();

        _jogador.Mao.Adicionar(rum);

        Assert.AreEqual(rum, _cartasAdicionadasNaMao[0].Item2);
        Assert.AreEqual(_jogador.Id, _cartasAdicionadasNaMao[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoRemoverCartaMao()
    {
        var rum = new Rum();

        _jogador.Mao.Adicionar(rum);
        _jogador.Mao.Remover(rum);

        Assert.AreEqual(rum, _cartasRemovidasNaMao[0].Item2);
        Assert.AreEqual(_jogador.Id, _cartasRemovidasNaMao[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoAdicionarCartaNoCampo()
    {
        var cascoAco = new CascoAco();

        _jogador.Campo.Adicionar(cascoAco);

        Assert.AreEqual(cascoAco, _cartasAdicionadasNoCampo[0].Item2);
        Assert.AreEqual(_jogador.Id, _cartasAdicionadasNoCampo[0].Item1);
    }

    [Test]
    public void DeveInvocarFuncaoAoRemoverCartaNoCampo()
    {
        var cascoAco = new CascoAco();

        _jogador.Campo.Adicionar(cascoAco);

        int vidaTotal = cascoAco.Vida;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _jogador.Campo.DanificarEmbarcacao();
        }

        Assert.AreEqual(cascoAco, _cartasRemovidasNoCampo[0].Item2);
        Assert.AreEqual(_jogador.Id, _cartasRemovidasNoCampo[0].Item1);
    }
}
