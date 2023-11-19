namespace Piratas.Servidor.Testes;

using System.Collections.Generic;
using Dominio;
using Dominio.Cartas;
using Dominio.Cartas.ResolucaoImediata;
using Dominio.Excecoes.Mao;
using NUnit.Framework;

public class MaoTestes
{
    private Mao _mao;

    [SetUp]
    public void Inicializacao()
    {
        var cartas = new List<Carta>();

        _mao = new Mao(cartas);

        for (int i = 0; i < Mao.LimiteCartas; i++)
        {
            var rum = new Rum();

            _mao.Adicionar(rum);
        }
    }

    [Test]
    public void DeveAdicionarCarta()
    {
        var rum = new Rum();

        _mao.Adicionar(rum);

        Assert.IsTrue(_mao.Possui(rum));
    }

    [Test]
    public void DeveAdicionarCartas()
    {
        var cartas = new List<Carta> {new Rum(), new Rum(), new Rum()};

        _mao.Adicionar(cartas);

        Assert.AreEqual(cartas.Count, _mao.ObterTodas().Count);
    }

    [Test]
    public void DeveLevantarErroLimiteCartasAtingidoAoAdicionar()
    {
        Assert.Throws<LimiteCartasMaoAtingidoExcecao>(AdicionarCarta);

        void AdicionarCarta()
        {
            var rum = new Rum();

            _mao.Adicionar(rum);
        }
    }

    [Test]
    public void DeveObterTodasCartas()
    {

        Assert.AreEqual(Mao.LimiteCartas, _mao.ObterTodas().Count);
    }

    [Test]
    public void DeveObterCartaPorId()
    {
        Carta cartaObtida = _mao.ObterPorId(new Rum().Id);

        Assert.IsTrue(cartaObtida is not null);
    }

    [Test]
    public void DeveRemoverCarta()
    {
        Assert.AreEqual(Mao.LimiteCartas, _mao.ObterTodas().Count);

        Carta cartaObtida = _mao.ObterQualquer();

        _mao.Remover(cartaObtida);

        Assert.AreEqual(Mao.LimiteCartas - 1, _mao.ObterTodas().Count);
    }

    [Test]
    public void DeveObterQualquerCarta()
    {

        Carta cartaObtida = _mao.ObterQualquer();

        Assert.IsTrue(cartaObtida is not null);
    }

    [Test]
    public void DeveObterTodasCartasDeUmTipo()
    {
        List<Rum> cartasObtidas = _mao.ObterTodas<Rum>();

        Assert.AreEqual(Mao.LimiteCartas, cartasObtidas.Count);
    }

    [Test]
    public void DevePossuirCartaPorTipo()
    {
        Assert.IsTrue(_mao.Possui<Rum>());
    }

    [Test]
    public void NaoDevePossuirCartaPorTipo()
    {
        Assert.IsFalse(_mao.Possui<Papagaio>());
    }

    [Test]
    public void DevePossuirCarta()
    {
        Carta cartaObtida = _mao.ObterQualquer();

        Assert.IsTrue(_mao.Possui(cartaObtida));
    }

    [Test]
    public void NaoDevePossuirCarta()
    {
        Carta papagaio = new Papagaio();

        Assert.IsFalse(_mao.Possui(papagaio));
    }
}
