namespace Piratas.Servidor.Dominio.Cartas.Evento
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class Kraken : Evento
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            var acoesResultantes = new List<Acao>();

            foreach (var jogador in jogadoresNaMesa)
            {
                bool possuiEmbarcacao = jogador.Campo.Embarcacao != null;
                bool possuiTripulacao = jogador.Campo.Tripulacao.Count == 0;

                var resultanteAfogarTripulacao = new AfogarTripulante(acao, jogador, jogador);
                var resultanteDanificarEmbarcacao = new DanificarEmbarcacao(acao, jogador);

                if (!possuiEmbarcacao && !possuiTripulacao)
                    continue;

                if (possuiEmbarcacao && possuiTripulacao)
                {
                    var escolherResultante = new EscolherResultante(
                        acao, jogador, resultanteAfogarTripulacao, resultanteDanificarEmbarcacao);

                    acoesResultantes.Add(escolherResultante);
                }
                else if (!possuiEmbarcacao)
                {
                    var afogaveis = jogador.Campo.Tripulacao.Where(t => t.Afogavel).ToList();

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
