namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Cartas;
    using Excecoes.Acoes;
    using Tipos;

    public class DistribuirCartas : Resultante
    {
        public List<Carta> CartasOpcoes { get; private set; }

        public Dictionary<Jogador, Carta> CartasPorJogador { get; private set; }

        public DistribuirCartas(
            Acao origem,
            Jogador realizador,
            List<Jogador> jogadores,
            List<Carta> cartas) : base(origem, realizador)
        {
            CartasPorJogador = new Dictionary<Jogador, Carta>();

            // TODO: Como substituir isso por ToDictionary?
            foreach (var jogador in jogadores)
                CartasPorJogador.Add(jogador, null);

            CartasOpcoes = cartas;
        }

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            foreach ((var jogador, var carta) in CartasPorJogador)
            {
                if (carta == null)
                    throw new NaoHaCartaAtribudaException(this, jogador);

                if (!CartasOpcoes.Contains(carta))
                    throw new CartaNaoEUmaOpcaoException(this, carta);

                jogador.Mao.Adicionar(carta);
            }

            yield return null;
        }
    }
}
