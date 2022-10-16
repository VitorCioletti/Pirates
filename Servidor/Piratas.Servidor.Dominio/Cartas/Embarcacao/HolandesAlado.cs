namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class HolandesAlado : Embarcacao
    {
        private const int _tesourosParaVitoria = 4;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Jogador realizador = acao.Realizador;

            int somaTodosTesouros = realizador.CalcularTesouros();

            if (somaTodosTesouros >= _tesourosParaVitoria)
                mesa.Finalizar(realizador);

            return null;
        }
    }
}
