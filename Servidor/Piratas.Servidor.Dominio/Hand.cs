namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Excecoes.Mao;

    public class Hand
    {
        public const int CardLimit = 10;

        private readonly List<Card> _cards;

        public event Action<Card> OnAdd;

        public event Action<Card> OnRemove;

        public Hand(List<Card> cards)
        {
            _cards = cards;
        }

        public void Add(Card card)
        {
            if (_cards.Count == CardLimit)
                throw new HandCardLimitReachedException();

            _cards.Add(card);

            OnAdd?.Invoke(card);
        }

        public List<Card> GetAll() => _cards.ToList();

        public void Add(List<Card> cards) => cards.ForEach(Add);

        public Card GetById(string cardId) => _cards.FirstOrDefault(c => c.Id == cardId);

        public void Remove(Card card)
        {
            if (!Exists(card))
                throw new CardDoesNotExistInHandException(card);

            if (_cards.Count == 0)
                throw new EmptyHandException();

            _cards.Remove(card);

            OnRemove?.Invoke(card);
        }

        public Card GetAny()
        {
            int cardPosition = new Random().Next(0, GetCardQuantity());

            return _cards[cardPosition];
        }

        public int GetCardQuantity() => _cards.Count;

        public List<T> GetAll<T>() where T : Card => _cards.OfType<T>().ToList();

        public bool Exists(Card card) => _cards.Contains(card);

        public bool Exists<T>() where T : Card => _cards.Any(c => c is T);
    }
}
