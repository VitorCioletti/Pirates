namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;

    public abstract class DueloSurpresa : Duelo
    {
        public int Tiros { get; private set; }

        public DueloSurpresa() => Tiros = 1;

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao.Realizador.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            yield return null;
        }
    }
}
