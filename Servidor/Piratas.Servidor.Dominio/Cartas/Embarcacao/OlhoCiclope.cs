namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Tipos;

    public class OlhoCiclope : Embarcacao
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Jogador realizador = acao.Realizador;

            List<Jogador> outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            var escolherJogador = new EscolherJogador(acao, realizador, outrosJogadoresMesa, OlharCartas);
            var acoesResultantes = new List<Acao> { escolherJogador };

            return acoesResultantes;

            List<Acao> OlharCartas(Acao acaoEscolhida, Jogador jogador)
            {
                var olharCartasJogador =
                    new OlharCartasJogador(acaoEscolhida, realizador, jogador.Mao.ObterTodas());

                var acoesResultantesOlharCartas = new List<Acao> { olharCartasJogador };

                return acoesResultantesOlharCartas;
            }
        }
    }
}