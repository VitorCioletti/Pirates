namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Tipos;

    public class DescartarCarta : Resultante
    {
        public Carta CartaDescartada { get; private set; }

        public DescartarCarta(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (CartaDescartada == null)
                yield return null;

            if (CartaDescartada.GetType() == typeof(Tesouro))
                throw new ProibidoDescerCartaException(this, CartaDescartada);

            Alvo.Mao.Remover(CartaDescartada);
            mesa.PilhaDescarte.InserirTopo(CartaDescartada);

            yield return null;
        }
    }
}
