namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Tipos;
    using System.Collections.Generic;

    public class Mergulhador : ResolucaoImediata
    {
        public Mergulhador(string nome) : base(nome) {}

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            yield return 
                new EscolherCartaBaralho(acao, acao.Realizador, pilhaDescarte, pilhaDescarte.ObterTodas<Carta>());
        }
    }
}