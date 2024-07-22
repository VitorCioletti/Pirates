namespace Piratas.Servidor.Testes;

using System.Collections.Generic;
using Dominio;
using Dominio.Cartas;
using Dominio.Cartas.ResolucaoImediata;
using Dominio.Excecoes.Mao;
using NUnit.Framework;

public class MaoTestes
{
    private Hand _hand;

    [SetUp]
    public void Inicializacao()
    {
        var cartas = new List<Card>();

        _hand = new Hand(cartas);

        for (int i = 0; i < Hand.CardLimit; i++)
        {
            var rum = new Rum();

            _hand.Add(rum);
        }
    }

    [Test]
    public void DeveAdicionarCarta()
    {
        var rum = new Rum();

        _hand.Add(rum);

        Assert.IsTrue(_hand.Exists(rum));
    }

    [Test]
    public void DeveAdicionarCartas()
    {
        var cartas = new List<Card> {new Rum(), new Rum(), new Rum()};

        _hand.Add(cartas);

        Assert.AreEqual(cartas.Count, _hand.GetAll().Count);
    }

    [Test]
    public void DeveLevantarErroLimiteCartasAtingidoAoAdicionar()
    {
        Assert.Throws<HandCardLimitReachedException>(AdicionarCarta);

        void AdicionarCarta()
        {
            var rum = new Rum();

            _hand.Add(rum);
        }
    }

    [Test]
    public void DeveObterTodasCartas()
    {

        Assert.AreEqual(Hand.CardLimit, _hand.GetAll().Count);
    }

    [Test]
    public void DeveObterCartaPorId()
    {
        Card cardObtida = _hand.GetById(new Rum().Id);

        Assert.IsTrue(cardObtida is not null);
    }

    [Test]
    public void DeveRemoverCarta()
    {
        Assert.AreEqual(Hand.CardLimit, _hand.GetAll().Count);

        Card cardObtida = _hand.GetAny();

        _hand.Remove(cardObtida);

        Assert.AreEqual(Hand.CardLimit - 1, _hand.GetAll().Count);
    }

    [Test]
    public void DeveObterQualquerCarta()
    {

        Card cardObtida = _hand.GetAny();

        Assert.IsTrue(cardObtida is not null);
    }

    [Test]
    public void DeveObterTodasCartasDeUmTipo()
    {
        List<Rum> cartasObtidas = _hand.GetAll<Rum>();

        Assert.AreEqual(Hand.CardLimit, cartasObtidas.Count);
    }

    [Test]
    public void DevePossuirCartaPorTipo()
    {
        Assert.IsTrue(_hand.Exists<Rum>());
    }

    [Test]
    public void NaoDevePossuirCartaPorTipo()
    {
        Assert.IsFalse(_hand.Exists<Parrot>());
    }

    [Test]
    public void DevePossuirCarta()
    {
        Card cardObtida = _hand.GetAny();

        Assert.IsTrue(_hand.Exists(cardObtida));
    }

    [Test]
    public void NaoDevePossuirCarta()
    {
        Card papagaio = new Parrot();

        Assert.IsFalse(_hand.Exists(papagaio));
    }
}
