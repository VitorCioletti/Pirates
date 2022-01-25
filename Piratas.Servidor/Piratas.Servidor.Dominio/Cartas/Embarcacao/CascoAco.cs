namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;

    public class CascoAco : Embarcacao
    {
        public CascoAco(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao)
        {
            var realizador = acao.Realizador;

            var tesourosMao = realizador.Mao.ObterTodas<Tesouro>().OfType<Carta>().ToList();

            void aposEscolha(Carta carta)
            {
                realizador.Mao.Remover(carta);
                realizador.Campo.AdicionarProtegida(carta);
            }

            yield return new EscolherCartaMao(acao, realizador, tesourosMao, aposEscolha);
        }
    }
}
