namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class HolanderAlado : Embarcacao
    {
        private int _tesourosParaVitoria = 4;
 
        public HolanderAlado(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador, mesa);

        internal IEnumerable<Resultante> _aplicarEfeito(Jogador realizador, Mesa mesa)
        {
            var somaTodosTesouros = realizador.CalcularTesouros();

            if (somaTodosTesouros >= _tesourosParaVitoria)
                mesa.Finalizar(realizador);

            yield return null;
        }
    }
}