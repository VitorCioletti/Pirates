namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Regras;
    using Tipos;

    public class ComprarCarta : Resultante
    {
        public ComprarCarta(Acao origem, Jogador jogador) : base(origem, jogador) { }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(mesa.BaralhoCentral.ObterTopo());

            return null;
        }
    }
}