namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class PirataNobre : Tripulante
    {
        public int Tesouros { get; private set; } = 1;

        public PirataNobre() => Tiros = 0;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Campo campoRealizador = acao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}