namespace Piratas.Servidor.Testes.Cartas.ResolucaoImediata
{
    using System;
    using System.Collections.Generic;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Cartas;
    using Dominio.Cartas.ResolucaoImediata;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class RumTestes
    {
        [Test]
        public void AplicarEfeitoDeveComprarCartasParaJogador()
        {
            var jogadoresNaMesa = new List<Jogador>();
            var cartasNaMao = new List<Carta>();

            var mesa = new Mesa(jogadoresNaMesa);

            var jogadorRealizador = new Jogador(
                Guid.NewGuid(),
                null,
                null,
                null,
                null);

            var acao = Substitute.For<Acao>();

            var cartasNoBaralhoCentral = new List<Carta> {Substitute.For<Carta>(), Substitute.For<Carta>(),};

            mesa.BaralhoCentral.InserirTopo(cartasNoBaralhoCentral);
            jogadorRealizador.Mao.Adicionar(cartasNaMao);

            acao.Realizador.Returns(jogadorRealizador);

            var rum = new Rum();

            rum.AplicarEfeito(acao, mesa);

            Assert.IsTrue(jogadorRealizador.Mao.Possui(cartasNoBaralhoCentral[0]));
            Assert.IsNull(mesa.BaralhoCentral.ObterTopo());
        }
    }
}
