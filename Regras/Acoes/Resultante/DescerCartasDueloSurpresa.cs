namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Cartas.Tipos;
    using System.Collections.Generic;
    using Tipos;

    public class DescerCartasDueloSurpresa : Resultante
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public List<DueloSurpresa> DuelosSurpresa { get; private set; }

        public DescerCartasDueloSurpresa(Acao origem, Jogador realizador) : base(origem, realizador, null)
        {
            DuelosSurpresa = new List<DueloSurpresa>();
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            DuelosSurpresa.ForEach(c => c.AplicarEfeito(this, mesa));

            // TODO: Criar uma ação do tipo Imediata para separar esse tipo de regra dessa resultante?
            Vitorioso = Realizador.Campo.CalcularPontosDuelo() > Alvo.Campo.CalcularPontosDuelo() ? Realizador : Alvo;
            Perdedor = Vitorioso == Realizador ? Realizador : Alvo;

            Vitorioso.Campo.RemoverCartasDuelo();
            Perdedor.Campo.RemoverCartasDuelo();

            Perdedor.Campo.AfogarTripulacao();
            Perdedor.Campo.DanificarEmbarcacao();

            mesa.SairModoDuelo();

            yield return new RoubarCarta(this, Vitorioso, Perdedor);
        }
    }
}