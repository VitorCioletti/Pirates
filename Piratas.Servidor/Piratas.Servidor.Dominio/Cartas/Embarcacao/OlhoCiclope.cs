namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;

    public class OlhoCiclope : Embarcacao
    {
        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, Mesa mesa)
        {
            var realizador = acao.Realizador;

            IEnumerable<Resultante> olharCartas(Acao acao, Jogador jogador)
            {
                var olharCartasJogador =
                    new OlharCartasJogador(acao, realizador, jogador.Mao.ObterTodas()) as IEnumerable<Resultante>;

                return olharCartasJogador;
            }

            var outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            yield return new EscolherJogador(acao, realizador, outrosJogadoresMesa, olharCartas);
        }
    }
}
