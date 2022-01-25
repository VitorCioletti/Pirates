namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class DistribuirCartas : Resultante
    {
        public List<Carta> CartasOpcoes { get; private set; }

        public Dictionary<Jogador, Carta> CartasPorJogador { get; private set; }

        public DistribuirCartas(
            Acao origem, Jogador realizador, List<Jogador> jogadores, List<Carta> cartas) : base(origem, realizador)
        {
            CartasPorJogador = new Dictionary<Jogador, Carta>();

            // TODO: Como substituir isso por ToDictionary?
            foreach (var jogador in jogadores)
                CartasPorJogador.Add(jogador, null);

            CartasOpcoes = cartas;
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            foreach ((var jogador, var carta) in CartasPorJogador)
            {
                if (carta == null)
                    throw new Exception($"Jogador \"{jogador}\" não possui carta escolhida");

                if (!CartasOpcoes.Contains(carta))
                    throw new Exception($"Carta \"{carta}\" do jogador \"{jogador}\" não é uma opção.");

                jogador.Mao.Adicionar(carta);
            }

            yield return null;
        }
    }
}
