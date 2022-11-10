namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class VossaAlteza : BaseEmbarcacao
    {
        private const int _cartasMinimasNaMao = 5;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Jogador realizador = acao.Realizador;
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            List<Jogador> jogadoresOpcao =
                jogadoresNaMesa.Where(j => j.Mao.QuantidadeCartas() >= _cartasMinimasNaMao && j != realizador).ToList();

            List<string> idsJogadores = jogadoresOpcao.Select(j => j.Id.ToString()).ToList();

            var escolherJogador = new EscolherJogador(
                acao,
                realizador,
                idsJogadores,
                RoubarCarta);

            var acoesResultantes = new List<BaseAcao> {escolherJogador};

            return acoesResultantes;

            List<BaseAcao> RoubarCarta(BaseAcao acaoEscolhida, Jogador alvo)
            {
                var roubarCarta = new RoubarCarta(acaoEscolhida, realizador, alvo);

                var acoesResultantesRoubarCarta = new List<BaseAcao> {roubarCarta};

                return acoesResultantesRoubarCarta;
            }
        }
    }
}
