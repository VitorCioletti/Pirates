namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Acoes.Primaria;
    using Cartas.ResolucaoImediata;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Dominio;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Tipos;

    public class AfogarTripulante : Resultante
    {
        public Tripulante TripulanteAfogado { get; private set; }

        public AfogarTripulante(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
            var tripulacao = alvo.Campo.Tripulacao;

            if (tripulacao.Count == 0)
                throw new Exception($"Jogador \"{alvo}\" não possui tripulação.");

            if (tripulacao.All(t => !t.Afogavel))
                throw new Exception($"Nenhum tripulante de \"{alvo}\" pode ser afogado.");
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (Origem is DescerCarta descerCarta)
            {
                if (descerCarta.Carta is HomemAoMar)
                {
                    if (TripulanteAfogado is PirataNobre)
                    {
                        throw new Exception(
                            $"\"{nameof(PirataNobre)}\" não pode ser afogado por \"{nameof(HomemAoMar)}\".");
                    }
                }
            }

            if (!TripulanteAfogado.Afogavel)
                throw new Exception($"Esse tripulante não pode ser afogado.");

            Alvo.Campo.Remover(TripulanteAfogado);
            mesa.PilhaDescarte.InserirTopo(TripulanteAfogado);

            yield return null;
        }
    }
}
