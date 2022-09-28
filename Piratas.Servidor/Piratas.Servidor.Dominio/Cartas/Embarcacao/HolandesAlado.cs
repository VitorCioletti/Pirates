namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class HolanderAlado : Embarcacao
    {
        private readonly int _tesourosParaVitoria = 4;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
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