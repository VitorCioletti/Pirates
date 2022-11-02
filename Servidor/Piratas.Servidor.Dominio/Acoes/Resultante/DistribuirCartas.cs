namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class DistribuirCartas : BaseResultanteComDicionarioEscolhas
    {
        public DistribuirCartas(
            BaseAcao origem,
            Jogador realizador,
            IEnumerable<Jogador> jogadores,
            IEnumerable<Carta> cartas)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                TipoEscolha.Jogador,
                TipoEscolha.Carta,
                2,
                cartas.ObterIds(),
                jogadores.Select(j => j.Id.ToString()).ToList())
        {
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            foreach ((string idJogador, string idCarta) in Escolhas)
            {
                Jogador jogador = mesa.Jogadores.First(j => j.Id.ToString() == idJogador);
                Carta carta = jogador.Mao.ObterPorId(idCarta);

                jogador.Mao.Adicionar(carta);
            }

            return null;
        }
    }
}
