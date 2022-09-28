namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes.Primaria;
    using Cartas;
    using Cartas.Tesouro;
    using Cartas.Tipos;
    using Cartas.Tripulacao;

    public class Jogador
    {
        public Guid Id { get; }

        public int AcoesDisponiveis { get; private set; }

        public Mao Mao { get; }

        public Campo Campo { get; }

        public Jogador(
            Action<Jogador, bool, Carta> aoAdicionarOuRemoverCartaNaMao,
            Action<Jogador, bool, Carta> aoAdicionarOuRemoverCartaNoCampo)
        {
            Id = Guid.NewGuid();
            Mao = new Mao(new List<Carta>());
            Campo = new Campo();

            Mao.AoAdicionarOuRemoverCarta += AoAdicionarOuRemoverCartaNaMao;
            Campo.AoAdicionarOuRemoverCarta += AoAdicionarOuRemoverCartaNoCampo;

            void AoAdicionarOuRemoverCartaNaMao(bool adicionado, Carta carta) =>
                aoAdicionarOuRemoverCartaNaMao(this, adicionado, carta);

            void AoAdicionarOuRemoverCartaNoCampo(bool adicionado, Carta carta) =>
                aoAdicionarOuRemoverCartaNoCampo(this, adicionado, carta);
        }

        public DescerCarta DescerCarta(Carta carta) => new DescerCarta(this, carta);

        public ComprarCarta ComprarCarta() => new ComprarCarta(this);

        public Duelar Duelar(Jogador jogadorAtacado, Duelo cartaIniciadora) =>
            new Duelar(this, jogadorAtacado, cartaIniciadora);

        public void ResetarAcoesDisponiveis(int acoes)
        {
            AcoesDisponiveis = acoes;
        }

        public void SubtrairAcoesDisponiveis()
        {
            AcoesDisponiveis--;
        }

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

        public override string ToString() => Id.ToString();

        public override bool Equals(object obj) => obj is Jogador jogador && this == jogador;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Jogador jogador1, Jogador jogador2)
        {
            if (jogador1 == null || jogador2 == null)
                return false;

            return jogador1.Id == jogador2.Id;
        }

        public static bool operator !=(Jogador jogador1, Jogador jogador2)
        {
            if (jogador1 == null || jogador2 == null)
                return false;

            return jogador1.Id != jogador2.Id;
        }
    }
}
