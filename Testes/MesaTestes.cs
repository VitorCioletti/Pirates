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

    [Test]
    public void JogadorInicialDeveConseguirComprarCarta()
    {
        Jogador jogadorAtual = _mesa.JogadorAtual;

        int quantidadeCartasAntesCompra = jogadorAtual.Mao.ObterQuantidadeCartas();
        int quantidadeAcoesDisponiveisAntesCompra = jogadorAtual.AcoesDisponiveis;

        int quantidadeCartasEsperada = quantidadeCartasAntesCompra + 1;
        int quantidadeAcoesDisponiveisEsperada = quantidadeAcoesDisponiveisAntesCompra - 1;

        List<BaseAcao> acoesDisponiveis = _mesa.AcoesDisponiveisJogadores[jogadorAtual];

        BaseAcao comprarCarta = acoesDisponiveis.First(a => a is ComprarCarta);

        _mesa.ProcessarAcao(comprarCarta);

        Assert.AreEqual(quantidadeCartasEsperada, jogadorAtual.Mao.ObterQuantidadeCartas());
        Assert.AreEqual(quantidadeAcoesDisponiveisEsperada, jogadorAtual.AcoesDisponiveis);
    }

    [Test]
    public void JogadorNaoExecutaAcaoPrimariaForaTurno()
    {
        Jogador proximoJogador = _mesa.Jogadores[1];

        Assert.Throws<TurnoDeOutroJogadorExcecao>(ProcessarAcao);

        void ProcessarAcao()
        {
            _mesa.ProcessarAcao(new ComprarCarta(proximoJogador));
        }
    }

    [Test]
    public void JogadorNaoExecutaResultanteNaoEsperada()
    {
        Jogador jogadorAtual = _mesa.Jogadores[0];

        var acaoOrigem = new DescerCarta(jogadorAtual, new Canhao());

        var resultante = new DescartarCarta(
            acaoOrigem,
            jogadorAtual,
            jogadorAtual,
            new List<string>());

        Assert.Throws<ResultanteNaoEsperadaExcecao>(ProcessarAcao);

        void ProcessarAcao()
        {
            _mesa.ProcessarAcao(resultante);
        }
    }

    [Test]
    public void DeveMudarJogadorAtualAposAnteriorJogar()
    {
        foreach (Jogador jogador in _mesa.Jogadores)
        {
            while (jogador.AcoesDisponiveis > 0)
            {
                List<BaseAcao> acoesDisponiveis = _mesa.AcoesDisponiveisJogadores[jogador];

                BaseAcao comprarCarta = acoesDisponiveis.First(a => a is ComprarCarta);

                jogador.Mao.Remover(jogador.Mao.ObterQualquer());

                _mesa.ProcessarAcao(comprarCarta);
            }
        }

        Assert.Pass();
    }

    [Test]
    public void JogadorDeveGanharSePossuirTesourosSuficientes()
    {
        Jogador primeiroJogador = _mesa.JogadorAtual;
        Jogador jogadorVencedor = _mesa.Jogadores[1];

        jogadorVencedor.Mao.Adicionar(new Tesouro(5));

        while (primeiroJogador.AcoesDisponiveis > 0)
        {
            List<BaseAcao> acoesDisponiveis = _mesa.AcoesDisponiveisJogadores[primeiroJogador];

            BaseAcao comprarCarta = acoesDisponiveis.First(a => a is ComprarCarta);

            primeiroJogador.Mao.Remover(primeiroJogador.Mao.ObterQualquer());

            _mesa.ProcessarAcao(comprarCarta);
        }

        Assert.AreEqual(jogadorVencedor, _mesa.Vencedor);
    }

    [Test]
    public void DeveEntrarEmModoDuelo()
    {
        Assert.IsFalse(_mesa.EmDuelo);

        _mesa.EntrarModoDuelo();

        Assert.IsTrue(_mesa.EmDuelo);
    }

    [Test]
    public void DeveSairEmModoDuelo()
    {
        _mesa.EntrarModoDuelo();

        Assert.IsTrue(_mesa.EmDuelo);

        _mesa.SairModoDuelo();

        Assert.IsFalse(_mesa.EmDuelo);
    }

    [Test]
    public void DeveLancarExcecaoSeEmDuelo()
    {
        _mesa.EntrarModoDuelo();

        Assert.Throws<EmDueloExcecao>(_mesa.EntrarModoDuelo);
    }

    [Test]
    public void DeveLancarExcecaoSeNaoEstaEmDuelo()
    {
        Assert.Throws<SemDueloExcecao>(_mesa.SairModoDuelo);
    }

    [Test]
    public void JogadorInicialDeveConseguirExecutarAcaoPrimaria()
    {
        Jogador jogadorInicial = _mesa.JogadorAtual;

        int quantidadeCartas = jogadorInicial.Mao.ObterQuantidadeCartas();
        int quantidadeAcoesDisponiveis = jogadorInicial.AcoesDisponiveis;

        var comprarCarta = new ComprarCarta(jogadorInicial);

        _mesa.ProcessarAcao(comprarCarta);

        Assert.IsTrue(quantidadeCartas < jogadorInicial.Mao.ObterQuantidadeCartas());
        Assert.IsTrue(quantidadeAcoesDisponiveis > jogadorInicial.AcoesDisponiveis);
    }

    [Test]
    public void ExecutarTodasPrimariasDeveMudarJogadorAtual()
    {
        Jogador jogadorInicial = _mesa.JogadorAtual;
        int turnoInicial = _mesa.Turno;

        int quantidadeAcoesDisponiveis = jogadorInicial.AcoesDisponiveis;

        for (int i = 0; i < quantidadeAcoesDisponiveis; i++)
        {
            var comprarCarta = new ComprarCarta(jogadorInicial);

            _mesa.ProcessarAcao(comprarCarta);
        }

        Assert.AreNotEqual(jogadorInicial, _mesa.JogadorAtual);
        Assert.IsTrue(turnoInicial < _mesa.Turno);
    }

    [Test]
    public void DeveLevantarErroAoJogarForaDoTurno()
    {
        Assert.Throws<TurnoDeOutroJogadorExcecao>(ComprarCartaForaTurno);

        void ComprarCartaForaTurno()
        {
            Jogador jogador = _mesa.Jogadores[1];

            var comprarCarta = new ComprarCarta(jogador);

            _mesa.ProcessarAcao(comprarCarta);
        }
    }

    [Test]
    public void DeveRegistrarEExecutarImediata()
    {
        Jogador jogadorAtual = _mesa.JogadorAtual;

        bool primariaExecutada = false;
        bool imediataExecutada = false;

        var primaria = Substitute.For<BasePrimaria>(jogadorAtual, null);
        var imediata = Substitute.For<BaseImediata>(jogadorAtual, null);

        primaria.When(i => i.AplicarRegra(_mesa)).Do(AoAplicarRegraPrimaria);
        imediata.When(i => i.AplicarRegra(_mesa)).Do(AoAplicarRegraImediata);

        _mesa.ProcessarAcao(primaria);

        Assert.IsTrue(primariaExecutada && imediataExecutada);

        void AoAplicarRegraPrimaria(CallInfo _)
        {
            _mesa.RegistrarImediataAposResultantes(imediata);

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
        Jogador jogadorAtual = _mesa.JogadorAtual;

        bool primariaExecutada = false;
        bool resultanteExecutada = false;

        var primaria = Substitute.For<BasePrimaria>(jogadorAtual, null);

        var resultante = Substitute.For<BaseResultante>(
            primaria,
            jogadorAtual,
            TipoEscolha.Acao,
            null);

        var acoesResultantesEsperadas = new List<BaseAcao> {resultante};

        primaria.AplicarRegra(_mesa).Returns(acoesResultantesEsperadas).AndDoes(AoAplicarRegraPrimaria);
        resultante.When(i => i.AplicarRegra(_mesa)).Do(AoAplicarRegraResultante);

        Dictionary<Jogador, List<BaseAcao>> resultado = _mesa.ProcessarAcao(primaria);

        Assert.IsTrue(resultado.Count > 0);

        BaseAcao resultanteObtida = resultado[jogadorAtual].Single();

        Assert.AreEqual(acoesResultantesEsperadas[0], resultanteObtida);

        Dictionary<Jogador, List<BaseAcao>> resultadoAcaoResultante = _mesa.ProcessarAcao(resultanteObtida);

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
