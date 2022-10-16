namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class CascoAco : Embarcacao
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            var realizador = acao.Realizador;

            var tesourosMao = realizador.Mao.ObterTodas<Tesouro>().OfType<Carta>().ToList();

            var escolherCartaMao = new EscolherCartaMao(acao, realizador, tesourosMao, AposEscolha);
            var acoesResultantes = new List<Acao> { escolherCartaMao };

            return acoesResultantes;

            void AposEscolha(Carta carta)
            {
                realizador.Mao.Remover(carta);
                realizador.Campo.AdicionarProtegida(carta);
            }
        }
    }
}
