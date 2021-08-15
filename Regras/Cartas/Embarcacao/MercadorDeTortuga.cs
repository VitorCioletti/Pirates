namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class MercadorDeTortuga : Embarcacao
    {
        public MercadorDeTortuga(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao); 

        internal Resultante _aplicarEfeito(Acao acao)
        {
            var comprarCarta = new ComprarCarta(acao.Realizador);

            return new CopiarPrimaria(acao, acao.Realizador, comprarCarta);
        }
    }
}