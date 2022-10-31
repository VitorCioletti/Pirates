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

            var cartasNoBaralhoCentral = new List<Carta> { Substitute.For<Carta>(), Substitute.For<Carta>(), };

            mesa.BaralhoCentral.InserirTopo(cartasNoBaralhoCentral);
            jogadorRealizador.Mao.Adicionar(cartasNaMao);

            var acao = Substitute.For<Acao>(jogadorRealizador, null);

            var rum = new Rum();

            rum.AplicarEfeito(acao, mesa);

            foreach (Carta carta in cartasNoBaralhoCentral)
                Assert.IsTrue(jogadorRealizador.Mao.Possui(carta));

            Assert.IsNull(mesa.BaralhoCentral.ObterTopo());
        }
    }
}
