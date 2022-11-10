namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class Explosivos : BaseResolucaoImediata
    {
        private const int _cartasObtidas = 3;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            List<Carta> cartas = baralhoCentral.ObterTopo(_cartasObtidas);

            var distribuirCartas = new DistribuirCartas(acao, acao.Realizador, jogadoresNaMesa, cartas);
            var acoesResultantes = new List<BaseAcao> { distribuirCartas };

            return acoesResultantes;
        }
    }
}
