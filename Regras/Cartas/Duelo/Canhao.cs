namespace Piratas.Servidor.Regras.Cartas.Duelo
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public abstract class Canhao : Duelo
    {
        public int Tiros { get; private set; }
 
        public Canhao(string nome, int tiros) : base(nome) { Tiros = tiros; }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            yield return null;
        }
    }
}