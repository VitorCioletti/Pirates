namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Excecoes.Mao;

    public class Mao
    {
        private readonly int _limiteCartas = 10;

        private readonly List<Carta> _cartas;

        public event Action<bool, Carta> AoAdicionarOuRemoverCarta;

        public Mao(List<Carta> cartas)
        {
            _cartas = cartas;

            AoAdicionarOuRemoverCarta = (_, __) => { };
        }

        public void Adicionar(Carta carta)
        {
            if (_cartas.Count == _limiteCartas)
                throw new LimiteCartasMaoAtingidoException();

            _cartas.Add(carta);

            AoAdicionarOuRemoverCarta?.Invoke(true, carta);
        }

        public List<Carta> ObterTodas() => _cartas.ToList();

        public void Adicionar(List<Carta> cartas) => cartas.ForEach(Adicionar);

        public void Remover(Carta carta)
        {
            if (!Possui(carta))
                throw new CartaNaoExisteNaMaoException(carta);

            if (_cartas.Count == 0)
                throw new MaoVaziaException();

            _cartas.Remove(carta);

            AoAdicionarOuRemoverCarta?.Invoke(false, carta);
        }

        public Carta ObterQualquer()
        {
            var posicaoCarta = new Random().Next(0, QuantidadeCartas());

            return _cartas[posicaoCarta];
        }

        public int QuantidadeCartas() => _cartas.Count;

        public List<T> ObterTodas<T>() where T : Carta => _cartas.OfType<T>().ToList();

        public void Remover(int posicao) => _cartas.RemoveAt(posicao);

        public bool Possui(Carta carta) => _cartas.Contains(carta);

        public bool Possui<T>() where T : Carta => _cartas.Any(c => c is T);
    }
}