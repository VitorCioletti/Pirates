namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class RoubarCarta : BaseResultanteComListaEscolhas
    {
        public RoubarCarta(Acao origem, Jogador realizador, Jogador alvo)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                alvo.Mao.ObterTodas<Carta>().ObterIds())
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            Carta cartaRoubada = Alvo.Mao.ObterPorId(escolha);

            Realizador.Mao.Adicionar(cartaRoubada);
            Alvo.Mao.Remover(cartaRoubada);

            return null;
        }
    }
}
