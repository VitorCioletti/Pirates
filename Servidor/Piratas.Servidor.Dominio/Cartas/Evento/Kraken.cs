namespace Piratas.Servidor.Dominio.Cartas.Evento
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tripulacao;

    public class Kraken : BaseEvento
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            var acoesResultantes = new List<Acao>();

            foreach (Jogador jogador in jogadoresNaMesa)
            {
                bool possuiEmbarcacao = jogador.Campo.BaseEmbarcacao != null;
                bool possuiTripulacao = jogador.Campo.Tripulacao.Count == 0;

                var resultanteAfogarTripulacao = new AfogarTripulante(acao, jogador, jogador);
                var resultanteDanificarEmbarcacao = new DanificarEmbarcacao(acao, jogador);

                if (!possuiEmbarcacao && !possuiTripulacao)
                    continue;

                if (possuiEmbarcacao && possuiTripulacao)
                {
                    var escolherResultante = new EscolherResultante(
                        acao,
                        jogador,
                        resultanteAfogarTripulacao,
                        resultanteDanificarEmbarcacao);

                    acoesResultantes.Add(escolherResultante);
                }
                else if (!possuiEmbarcacao)
                {
                    List<BaseTripulante> afogaveis = jogador.Campo.Tripulacao.Where(t => t.Afogavel).ToList();

                    if (afogaveis.Count == 0)
                        continue;

                    if (afogaveis.Count == 1)
                        jogador.Campo.AfogarTripulacao();

                    else
                        acoesResultantes.Add(resultanteAfogarTripulacao);
                }
                else
                {
                    jogador.Campo.DanificarEmbarcacao();
                }
            }

            return acoesResultantes;
        }
    }
}
