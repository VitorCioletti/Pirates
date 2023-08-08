namespace Piratas.Servidor.Dominio.Cartas.Evento
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Resultante;
    using Tripulacao;

    public class Kraken : BaseEvento
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            var acoesResultantes = new List<BaseAcao>();

            foreach (Jogador jogador in jogadoresNaMesa)
            {
                bool possuiEmbarcacao = jogador.Campo.Embarcacao != null;
                bool possuiTripulacao = jogador.Campo.Tripulacao.Count == 0;

                var resultanteAfogarTripulacao = new AfogarTripulante(acao, jogador, jogador);
                var resultanteDanificarEmbarcacao = new DanificarEmbarcacao(jogador);

                if (!possuiEmbarcacao && !possuiTripulacao)
                    continue;

                if (possuiEmbarcacao && possuiTripulacao)
                {
                    var escolherResultante = new EscolherAcao(
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
