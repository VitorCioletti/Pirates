namespace Piratas.Servidor.Dominio.Cartas.Evento
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;

    public class Kraken : Evento
    {
        public Kraken(string nome) : base(nome)
        {

        }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.Jogadores);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, List<Jogador> jogadores)
        {
            foreach (var jogador in jogadores)
            {
                var possuiEmbarcacao = jogador.Campo.Embarcacao != null;
                var possuiTripulacao = jogador.Campo.Tripulacao.Count == 0;

                var resultanteAfogarTripulacao = new AfogarTripulacao(acao, jogador, jogador);
                var resultanteDanificarEmbarcacao = new DanificarEmbarcacao(acao, jogador);

                if (!possuiEmbarcacao && !possuiTripulacao)
                    continue;

                else if (possuiEmbarcacao && possuiTripulacao)
                {
                    yield return new EscolherResultante(
                        acao, jogador, resultanteAfogarTripulacao, resultanteDanificarEmbarcacao);
                }

                else if (!possuiEmbarcacao && possuiTripulacao)
                {
                    var afogaveis = jogador.Campo.Tripulacao.Where(t => t.Afogavel).ToList();

                    if (afogaveis.Count == 0)
                        continue;

                    else if (afogaveis.Count == 1)
                        jogador.Campo.AfogarTripulacao();

                    else
                        yield return resultanteAfogarTripulacao;
                }
                else
                {
                    jogador.Campo.DanificarEmbarcacao();

                    continue;
                }
            }
        }
    }
}
