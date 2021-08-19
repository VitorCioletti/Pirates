namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;
    using System.Linq;
    using System;
    using System.Collections.Generic;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public ConvocarTripulacao(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            var realizador = acao.Realizador;

            if (realizador.Campo.TripulacaoCheia())
                throw new Exception("Tripulação do jogador está cheia.");

            var tripulacoesDescartadas = pilhaDescarte.ObterTodas<Tripulacao>().OfType<Carta>().ToList();

            if (tripulacoesDescartadas.Count == 0)
                throw new Exception("Não existe tripualção na pilha de descarte.");

            yield return new EscolherCartaBaralho(acao, realizador, pilhaDescarte, tripulacoesDescartadas);
        }
    }
}