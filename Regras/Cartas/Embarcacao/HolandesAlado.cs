namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class HolanderAlado : Embarcacao
    {
        private int _tesourosParaVitoria = 4;
 
        public HolanderAlado(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador, mesa);

        internal Resultante _aplicarEfeito(Jogador realizador, Mesa mesa)
        {
            var somaTodosTesouros = realizador.CalcularTesouros();

            if (somaTodosTesouros >= _tesourosParaVitoria)
                mesa.Finalizar(realizador);

            return null;
        }
    }
}