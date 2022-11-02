namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;

    public class Explosivos : BaseResolucaoImediata
    {
        private const int _cartasObtidas = 3;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            List<Carta> cartas = baralhoCentral.ObterTopo(_cartasObtidas);

            var distribuirCartas = new DistribuirCartas(acao, acao.Realizador, jogadoresNaMesa, cartas);
            var acoesResultantes = new List<Acao> { distribuirCartas };

            return acoesResultantes;
        }
    }
}
