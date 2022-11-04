namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Cartas.Tesouro;
    using Cartas.Tripulacao;

    public class Jogador
    {
        public Guid Id { get; }

        public int AcoesDisponiveis { get; private set; }

        public Mao Mao { get; }

        public Campo Campo { get; }

        public Jogador(
            Guid id,
            Action<Guid, Carta> aoAdicionarCartaNaMao,
            Action<Guid, Carta> aoRemoverCartaNaMao,
            Action<Guid, Carta> aoAdicionarCartaNoCampo,
            Action<Guid, Carta> aoRemoverCartaNoCampo)
        {
            Id = id;
            Mao = new Mao(new List<Carta>());
            Campo = new Campo();

            Mao.AoAdicionar += AoAdicionarCartaNaMao;
            Mao.AoRemover += AoRemoverCartaNaMao;
            Campo.AoAdicionar += AoAdicionarCartaNoCampo;
            Campo.AoRemover += AoRemoverCartaNoCampo;

            void AoAdicionarCartaNaMao(Carta carta) =>
                aoAdicionarCartaNaMao?.Invoke(Id, carta);

            void AoRemoverCartaNaMao(Carta carta) =>
                aoRemoverCartaNaMao?.Invoke(Id, carta);

            void AoAdicionarCartaNoCampo(Carta carta) =>
                aoAdicionarCartaNoCampo?.Invoke(Id, carta);

            void AoRemoverCartaNoCampo(Carta carta) =>
                aoRemoverCartaNoCampo?.Invoke(Id, carta);
        }

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
            int tesouros = 0;

            tesouros += _obterTesourosMeioAmuleto();
            tesouros += _obterTesourosMao();
            tesouros += _obterTesourosProtegidos();
            tesouros += _obterTesourosPiratasNobres();

            return tesouros;
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

        private int _obterTesourosPiratasNobres()
        {
            int tesourosPiratasNobres =
                Campo.Tripulacao.Where(t => t is PirataNobre).Sum(t => ((PirataNobre)t).Tesouros);

            return tesourosPiratasNobres;
        }

        private int _obterTesourosProtegidos()
        {
            IEnumerable<Tesouro> tesourosProtegidos = Campo.ObterTodasProtegidas().OfType<Tesouro>();

            int somaTesourosProtegidos = tesourosProtegidos.Sum(c => c.Valor);

            return somaTesourosProtegidos;
        }

        private int _obterTesourosMao()
        {
            List<Tesouro> tesourosMao = Mao.ObterTodas<Tesouro>();

            int somaTesourosMao = tesourosMao.Sum(c => c.Valor);

            return somaTesourosMao;
        }

        private int _obterTesourosMeioAmuleto()
        {
            List<MeioAmuleto> meiosAmuletos = Mao.ObterTodas<MeioAmuleto>();

            int somaMeiosAmuletos = MeioAmuleto.CalcularPontosTesouro(meiosAmuletos);

            return somaMeiosAmuletos;
        }
    }
}
