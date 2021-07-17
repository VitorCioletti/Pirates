namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;
    using System;

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
            var posicaoCarta = new Random().Next(0, _cartas.Count);

            return _cartas[posicaoCarta];
        }

        public void Remover(int posicao) => _cartas.RemoveAt(posicao);

        public bool Possui(Carta carta) => _cartas.Contains(carta);
    }
}