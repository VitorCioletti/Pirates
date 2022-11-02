namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Evento;

    public class ComprarCarta : BasePrimaria
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            Carta cartaComprada = mesa.BaralhoCentral.ObterTopo();

            // TODO: Como avisar que foi uma carta evento comprada?
            if (cartaComprada is BaseEvento)
                return cartaComprada.AplicarEfeito(this, mesa);

            Realizador.Mao.Adicionar(cartaComprada);

            return null;
        }
    }
}
