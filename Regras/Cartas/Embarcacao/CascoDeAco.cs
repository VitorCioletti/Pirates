namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System;
    using System.Linq;

    public class CascoDeAco : Embarcacao
    {
        public CascoDeAco(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao);

        internal Resultante _aplicarEfeito(Acao acao)
        {
            var realizador = acao.Realizador;

            var tesourosMao = realizador.Mao.ObterTodas<Tesouro>().OfType<Carta>().ToList();

            Action<Carta> aposEscolha = (carta) =>
            {
                realizador.Mao.Remover(carta);
                realizador.Campo.AdicionarProtegida(carta);
            };

            return new EscolherCartaMao(acao, realizador, tesourosMao, aposEscolha);
        }
    }
}