namespace Piratas.Servidor.Regras.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;

    public abstract class DueloSurpresa : Duelo
    {
        public int Tiros { get; private set; }

        public DueloSurpresa(string nome) : base(nome) => Tiros = 1;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            yield return null;
        }
    }
}