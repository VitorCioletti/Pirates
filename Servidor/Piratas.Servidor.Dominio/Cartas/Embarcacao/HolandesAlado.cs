namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;
    using Tipos;

    public class HolandesAlado : Embarcacao
    {
        private readonly int _tesourosParaVitoria = 4;

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao.Realizador, mesa);

        internal IEnumerable<Resultante> _aplicarEfeito(Jogador realizador, Mesa mesa)
        {
            var somaTodosTesouros = realizador.CalcularTesouros();

            if (somaTodosTesouros >= _tesourosParaVitoria)
                mesa.Finalizar(realizador);

            yield return null;
        }
    }
}
