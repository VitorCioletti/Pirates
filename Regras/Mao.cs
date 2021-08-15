namespace Piratas.Servidor.Regras
{
    using Cartas;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public class Mao
    {
        private int _limiteCartas = 10;

        private List<Carta> _cartas;

        public Mao(List<Carta> cartas) => _cartas = cartas;

        public void Adicionar(Carta carta)
        {
            if (_cartas.Count == _limiteCartas)
                throw new Exception("Limite de cartas atingido.");

            _cartas.Add(carta);
        }

        public List<Carta> ObterTodas() => _cartas.ToList();

        public void Adicionar(List<Carta> cartas) => cartas.ForEach(c => Adicionar(c));

        public void Remover(Carta carta)
        {
            if (!Possui(carta))
                throw new Exception($"Carta \"{carta}\" não existe na mão.");

            if (_cartas.Count == 0)
                throw new Exception("Mão está vazia.");

            _cartas.Remove(carta);
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