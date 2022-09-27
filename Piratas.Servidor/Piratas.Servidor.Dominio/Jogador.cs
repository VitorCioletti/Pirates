namespace Piratas.Servidor.Dominio
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes.Primaria;
    using Cartas;
    using Cartas.Tesouro;
    using Cartas.Tipos;
    using Cartas.Tripulacao;

    public class Jogador
    {
        public string Id { get; private set; }

        public int AcoesDisponiveis { get; set; }

        public Mao Mao { get; set; }

        public Campo Campo { get; set; }

        public Jogador(string id)
        {
            Id = id;
            Mao = new Mao(new List<Carta>());
            Campo = new Campo();

            Campo.AoRemoverProtegidas += (protegidas) => Mao.Adicionar(protegidas);
        }

        public DescerCarta DescerCarta(Carta carta) => new DescerCarta(this, carta);

        public ComprarCarta ComprarCarta() => new ComprarCarta(this);

        public Duelar Duelar(Jogador jogadorAtacado, Duelo cartaIniciadora) =>
            new Duelar(this, jogadorAtacado, cartaIniciadora);

        public int CalcularTesouros()
        {
            var meiosAmuletos = Mao.ObterTodas<MeioAmuleto>();
            var somaMeiosAmuletos = MeioAmuleto.CalcularPontosTesouro(meiosAmuletos);

            var tesourosMao = Mao.ObterTodas<Tesouro>();
            var somaTesourosMao = tesourosMao.Sum(c => c.Valor);

            var tesourosProtegidos = Campo.ObterTodasProtegidas().OfType<Tesouro>();
            var somaTesourosProtegidos = tesourosProtegidos.Sum(c => c.Valor);

            var tesourosPiratasNobres =
                Campo.Tripulacao.Where(t => t is PirataNobre).Sum(t => ((PirataNobre)t).Tesouros);

            return somaTesourosMao + somaTesourosProtegidos + somaMeiosAmuletos + tesourosPiratasNobres;
        }

        public override string ToString() => Id;

        public override bool Equals(object obj) => obj is Jogador jogador && this == jogador;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Jogador jogador1, Jogador jogador2) => jogador1.Id == jogador2.Id;

        public static bool operator !=(Jogador jogador1, Jogador jogador2) => jogador1.Id != jogador2.Id;
    }
}
