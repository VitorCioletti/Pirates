namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
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
                cartas.Select(c => c.Id).ToList()) {}

        public override List<Acao> AplicarRegra(Mesa mesa) => null;
    }
}
