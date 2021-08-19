namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class HomemAoMar : ResolucaoImediata
    {
        public HomemAoMar(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, acao.Alvo);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, Jogador alvo)
        {
            yield return new AfogarTripulacao(acao, acao.Realizador, alvo);
        } 
    }
}