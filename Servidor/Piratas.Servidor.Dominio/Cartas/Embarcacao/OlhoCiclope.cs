namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class OlhoCiclope : BaseEmbarcacao
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Jogador realizador = acao.Realizador;

            List<Jogador> outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            List<string> idsJogadores = outrosJogadoresMesa.Select(j => j.Id.ToString()).ToList();

            var escolherJogador = new EscolherJogador(
                acao,
                realizador,
                idsJogadores,
                OlharCartas);

            var acoesResultantes = new List<BaseAcao> {escolherJogador};

            return acoesResultantes;

            List<BaseAcao> OlharCartas(BaseAcao acaoEscolhida, Jogador jogador)
            {
                var olharCartasJogador =
                    new OlharCartasJogador(acaoEscolhida, realizador, jogador.Mao.ObterTodas());

                var acoesResultantesOlharCartas = new List<BaseAcao> {olharCartasJogador};

                return acoesResultantesOlharCartas;
            }
        }
    }
}
