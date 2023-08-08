namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Excecoes.Mao;

    public class Mao
    {
        private const int _limiteCartas = 10;

        private readonly List<Carta> _cartas;

        public event Action<Carta> AoAdicionar;

        public event Action<Carta> AoRemover;

        public Mao(List<Carta> cartas)
        {
            _cartas = cartas;
        }

        public void Adicionar(Carta carta)
        {
            if (_cartas.Count == _limiteCartas)
                throw new LimiteCartasMaoAtingidoExcecao();

            _cartas.Add(carta);

            AoAdicionar?.Invoke(carta);
        }

        public List<Carta> ObterTodas() => _cartas.ToList();

        public void Adicionar(List<Carta> cartas) => cartas.ForEach(Adicionar);

        public Carta ObterPorId(string idCarta) => _cartas.FirstOrDefault(c => c.Id == idCarta);

        public void Remover(Carta carta)
        {
            if (!Possui(carta))
                throw new CartaNaoExisteNaMaoExcecao(carta);

            if (_cartas.Count == 0)
                throw new MaoVaziaExcecao();

            _cartas.Remove(carta);

            AoRemover?.Invoke(carta);
        }

        public Carta ObterQualquer()
        {
            int posicaoCarta = new Random().Next(0, ObterQuantidadeCartas());

            return _cartas[posicaoCarta];
        }

        public int ObterQuantidadeCartas() => _cartas.Count;

        public List<T> ObterTodas<T>() where T : Carta => _cartas.OfType<T>().ToList();

        public bool Possui(Carta carta) => _cartas.Contains(carta);

        public bool Possui<T>() where T : Carta => _cartas.Any(c => c is T);
    }
}
