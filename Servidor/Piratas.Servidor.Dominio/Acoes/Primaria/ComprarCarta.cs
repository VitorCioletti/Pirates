namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas.Tipos;
    using Resultante.Base;

    public class ComprarCarta : Primaria
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            var cartaComprada = mesa.BaralhoCentral.ObterTopo();

            // TODO: Como avisar que foi uma carta evento comprada?
            if (cartaComprada is Evento)
                return cartaComprada.AplicarEfeito(this, mesa);

            Realizador.Mao.Adicionar(cartaComprada);

            return null;
        }
    }
}
