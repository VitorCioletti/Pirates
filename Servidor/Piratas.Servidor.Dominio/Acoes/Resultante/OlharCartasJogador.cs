namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class OlharCartasJogador : BaseResultanteComListaEscolhaBooleana
    {
        public OlharCartasJogador(
            Acao origem,
            Jogador realizador,
            List<Carta> cartas)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartas.ObterIds()) {}

        public override List<Acao> AplicarRegra(Mesa mesa) => null;
    }
}
