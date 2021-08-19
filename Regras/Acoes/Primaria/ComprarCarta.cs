namespace Piratas.Servidor.Regras.Acoes.Primaria
{
    using Cartas.Tipos;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class ComprarCarta : Primaria
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            var cartaComprada = mesa.BaralhoCentral.ObterTopo();

            if (cartaComprada is Evento)
                return cartaComprada.AplicarEfeito(this, mesa);
            else
            {
                Realizador.Mao.Adicionar(cartaComprada);

                return null;
            }
        }
    }
}