namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Cartas.Tipos;
    using Dominio;
    using System.Collections.Generic;

    public class CalcularResultadoDuelo : Imediata
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public Embarcacao Embarcacao { get; private set; }

        public CalcularResultadoDuelo(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {

        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            Vitorioso = Realizador.Campo.CalcularPontosDuelo() > Alvo.Campo.CalcularPontosDuelo() ? Realizador : Alvo;
            Perdedor = Vitorioso == Realizador ? Alvo : Realizador;

            Vitorioso.Campo.RemoverCartasDuelo();
            Perdedor.Campo.RemoverCartasDuelo();

            Perdedor.Campo.AfogarTripulacao();
            Perdedor.Campo.DanificarEmbarcacao();

            mesa.SairModoDuelo();

            yield return new RoubarCarta(this, Vitorioso, Perdedor);
        }
    }
}
