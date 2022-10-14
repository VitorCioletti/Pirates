namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public abstract class Canhao : Duelo
    {
        public int Tiros { get; private set; }

        public Canhao(int tiros) => Tiros = tiros;

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao.Realizador.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            yield return null;
        }
    }
}
