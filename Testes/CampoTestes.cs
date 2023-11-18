namespace Piratas.Servidor.Testes;

using System.Collections.Generic;
using Dominio;
using Dominio.Cartas.Duelo;
using Dominio.Cartas.DueloSurpresa;
using Dominio.Cartas.Embarcacao;
using Dominio.Cartas.Tesouro;
using Dominio.Cartas.Tripulacao;
using Dominio.Excecoes.Campo;
using NUnit.Framework;

public class CampoTestes
{
    private Campo _campo;

    [SetUp]
    public void Inicializacao()
    {
        _campo = new Campo();
    }

    [Test]
    public void DeveDanificarEmbarcacao()
    {
        var cascoAco = new CascoAco();

        int vidaInicial = cascoAco.Vida;

        _campo.Adicionar(cascoAco);

        _campo.DanificarEmbarcacao();

        int vidaPosDano = _campo.Embarcacao.Vida;

        Assert.Greater(vidaInicial, vidaPosDano);
    }

    [Test]
    public void DeveRemoverEmbarcacaoAoZerarVida()
    {
        var cascoAco = new CascoAco();

        _campo.Adicionar(cascoAco);

        int vidaTotal = cascoAco.Vida;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _campo.DanificarEmbarcacao();
        }

        Assert.AreEqual(null, _campo.Embarcacao);
    }

    [Test]
    public void DeveAdicionarEmbarcacao()
    {
        var cascoAco = new CascoAco();

        _campo.Adicionar(cascoAco);

        Assert.AreEqual(cascoAco, _campo.Embarcacao);
    }

    [Test]
    public void DeveLevantarErroAoAdicionarEmbarcacaoEJaExistir()
    {
        var cascoAco = new CascoAco();

        _campo.Adicionar(cascoAco);

        Assert.Throws<ExisteEmbarcacaoExcecao>(Adicionar);

        void Adicionar()
        {
            _campo.Adicionar(cascoAco);
        }
    }

    [Test]
    public void DeveTrocarEmbarcacao()
    {
        var cascoAco = new CascoAco();
        var guerrilhaNaval = new GuerrilhaNaval();

        _campo.Adicionar(cascoAco);

        _campo.TrocarEmbarcacao(guerrilhaNaval);

        Assert.AreEqual(guerrilhaNaval, _campo.Embarcacao);
    }

    [Test]
    public void DeveLevantarErroAoTrocarEmbarcacaoENaoExistirNenhuma()
    {
        var guerrilhaNaval = new GuerrilhaNaval();

        Assert.Throws<SemEmbarcacaoExcecao>(TrocarEmbarcacao);

        void TrocarEmbarcacao()
        {
            _campo.TrocarEmbarcacao(guerrilhaNaval);
        }
    }

    [Test]
    public void DeveAdicionarCanhao()
    {
        var canhao = new Canhao();

        _campo.Adicionar(canhao);

        Assert.AreEqual(canhao, _campo.Canhoes[0]);
    }

    [Test]
    public void DeveAdicionarCanhoes()
    {
        var canhoes = new List<Canhao> {new(), new()};

        _campo.Adicionar(canhoes);

        Assert.AreEqual(canhoes, _campo.Canhoes);
    }

    [Test]
    public void DeveRemoverCanhao()
    {
        var canhao = new Canhao();

        _campo.Adicionar(canhao);

        _campo.Remover(canhao);

        Assert.AreEqual(0, _campo.Canhoes.Count);
    }

    [Test]
    public void DeveAdicionarTripulante()
    {
        var pirata = new Pirata();

        _campo.Adicionar(pirata);

        Assert.AreEqual(pirata, _campo.Tripulacao[0]);
    }

    [Test]
    public void DeveLevantarErroAoAdicionarETriuplacaoCheia()
    {
        for (int i = 0; i < Campo.TripulacaoMaxima; i++)
        {
            AdicionarPirata();
        }

        Assert.Throws<TripulacaoCheiaExcecao>(AdicionarPirata);

        void AdicionarPirata()
        {
            var pirata = new Pirata();

            _campo.Adicionar(pirata);
        }
    }

    [Test]
    public void DeveRemoverTripulante()
    {
        var pirata = new Pirata();

        _campo.Adicionar(pirata);
        _campo.Remover(pirata);

        Assert.AreEqual(0, _campo.Tripulacao.Count);
    }

    [Test]
    public void DeveLevantarErroAoRemoverTripulanteENaoExistir()
    {
        Assert.Throws<TripulacaoVaziaExcecao>(Remover);

        void Remover()
        {
            var pirata = new Pirata();

            _campo.Remover(pirata);
        }
    }

    [Test]
    public void DeveLevantarErroAoNaoEncontrarTripulante()
    {
        var pirata = new Pirata();

        _campo.Adicionar(pirata);
        Assert.Throws<TripulanteNaoEncontradoExcecao>(Remover);

        void Remover()
        {
            var pirataARemover = new Pirata();

            _campo.Remover(pirataARemover);
        }
    }

    [Test]
    public void DeveAfogarTripulacao()
    {
        var pirata = new Pirata();

        _campo.Adicionar(pirata);

        _campo.AfogarTripulacao();

        Assert.AreEqual(0, _campo.Tripulacao.Count);
    }

    [Test]
    public void DeveRemoverCartasDuelo()
    {
        var canhao = new Canhao();
        var ataqueSurpresa = new AtaqueSurpresa();

        _campo.Adicionar(canhao);
        _campo.Adicionar(ataqueSurpresa);

        _campo.RemoverCartasDuelo();

        Assert.AreEqual(0, _campo.Canhoes.Count);
        Assert.AreEqual(0, _campo.DuelosSurpresa.Count);
    }

    [Test]
    public void DeveAdicionarCartaProtegida()
    {
        var tesouro = new Tesouro(3);

        _campo.AdicionarProtegida(tesouro);

        Assert.AreEqual(tesouro, _campo.Protegidas[0]);
    }

    [Test]
    public void DeveRemoverProtegidasAoRemoverEmbarcacao()
    {
        var tesouro = new Tesouro(3);
        var cascoAco = new CascoAco();

        _campo.Adicionar(cascoAco);
        _campo.AdicionarProtegida(tesouro);

        int vidaTotal = cascoAco.Vida;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _campo.DanificarEmbarcacao();
        }

        Assert.AreEqual(null, _campo.Embarcacao);
    }

    [Test]
    public void DeveAdicionarDueloSurpresa()
    {
        var dueloSurpresa = new AtaqueSurpresa();

        _campo.Adicionar(dueloSurpresa);

        Assert.AreEqual(dueloSurpresa, _campo.DuelosSurpresa[0]);
    }
}
