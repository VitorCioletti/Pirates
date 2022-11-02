namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tesouro;

    public class CascoAco : BaseEmbarcacao
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Jogador realizador = baseAcao.Realizador;

            List<string> tesourosMao = realizador.Mao
                .ObterTodas<Tesouro>()
                .OfType<Carta>()
                .Select(c => c.Id)
                .ToList();

            var escolherCartaMao = new EscolherCartaMao(baseAcao, realizador, tesourosMao, AposEscolha);
            var acoesResultantes = new List<BaseAcao> {escolherCartaMao};

            return acoesResultantes;

            void AposEscolha(Carta carta)
            {
                realizador.Mao.Remover(carta);
                realizador.Campo.AdicionarProtegida(carta);
            }
        }
    }
}
