namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Passivo;
    using Cartas.Tesouro;
    using Excecoes.Acoes;

    public class DescerCarta : BasePrimaria
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta, Jogador alvo = null) : base(jogador, alvo) => Carta = carta;

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            if (Carta is Tesouro || Carta is BasePassivo)
                throw new ProibidoDescerCartaExcecao(this, Carta);

            List<BaseAcao> resultanteEfeitoCarta = Carta.AplicarEfeito(this, mesa);

            Realizador.Mao.Remover(Carta);
            mesa.PilhaDescarte.InserirTopo(Carta);

            return resultanteEfeitoCarta;
        }
    }
}
