namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;

    public class HolandesAlado : BaseEmbarcacao
    {
        private const int _tesourosParaVitoria = 4;

        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Jogador realizador = baseAcao.Realizador;

            int somaTodosTesouros = realizador.CalcularTesouros();

            if (somaTodosTesouros >= _tesourosParaVitoria)
                mesa.Finalizar(realizador);

            return null;
        }
    }
}
