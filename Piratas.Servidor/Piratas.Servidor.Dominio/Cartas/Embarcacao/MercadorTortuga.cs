namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class MercadorTortuga : Embarcacao
    {
        public MercadorTortuga(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao); 

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao)
        {
            var comprarCarta = new ComprarCarta(acao.Realizador);

            yield return new CopiarPrimaria(acao, acao.Realizador, comprarCarta);
        }
    }
}