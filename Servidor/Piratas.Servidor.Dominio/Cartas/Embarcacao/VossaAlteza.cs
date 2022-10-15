namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class VossaAlteza : Embarcacao
    {
        private readonly int _cartasMinimasNaMao = 5;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Jogador realizador = acao.Realizador;
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            List<Jogador> jogadoresOpcao =
                jogadoresNaMesa.Where(j => j.Mao.QuantidadeCartas() >= _cartasMinimasNaMao && j != realizador).ToList();

            var escolherJogador = new EscolherJogador(acao, realizador, jogadoresOpcao, RoubarCarta);
            var acoesResultantes = new List<Acao> { escolherJogador };

            return acoesResultantes;

            // TODO: Randômico ou permite escolha?
            List<Acao> RoubarCarta(Acao acaoEscolhida, Jogador alvo)
            {
                var roubarCarta = new RoubarCarta(acaoEscolhida, realizador, alvo);

                var acoesResultantesRoubarCarta = new List<Acao> { roubarCarta };

                return acoesResultantesRoubarCarta;
            }
        }
    }
}