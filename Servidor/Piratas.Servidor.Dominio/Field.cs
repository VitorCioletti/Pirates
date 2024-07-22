namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Cartas.Duelo;
    using Cartas.Embarcacao;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Excecoes.Campo;

    public class Field
    {
        public const int MaximumCrew = 2;

        public List<Cannon> Cannons { get; private set; }

        public List<SurpriseDuel> SurpriseDuel { get; private set; }

        public List<Card> Protected { get; private set; }

        public List<BaseCrewMember> Crew { get; private set; }

        public BaseShip Ship { get; private set; }

        public event Action<Card> OnAdd;

        public event Action<Card> OnRemove;

        private const int _shipBaseDamage = 1;

        public Field()
        {
            Cannons = new List<Cannon>();
            SurpriseDuel = new List<SurpriseDuel>();
            Protected = new List<Card>();
            Crew = new List<BaseCrewMember>();

            Ship = null;
        }

        public int CalculateDuelShots()
        {
            int duelShots = 0;

            duelShots += _calculateCannonDuelShots();
            duelShots += _calculateSurpriseDuelShots();
            duelShots += _calculateCrewDuelShots();
            duelShots += _calculateShipDuelShots(duelShots);

            return duelShots;
        }

        public void DamageShip()
        {
            if (Ship == null)
                return;

            Ship.TakeDamage(_shipBaseDamage);

            if (Ship.Life == 0)
                _removeShip();
        }

        public void Add(BaseCrewMember baseCrewMember)
        {
            if (Crew.Count >= MaximumCrew)
                throw new FullCrewException();

            Crew.Add(baseCrewMember);

            OnAdd?.Invoke(baseCrewMember);
        }

        public void Add(BaseShip baseShip)
        {
            if (Ship != null)
                throw new ShipAlreadyExistsException();

            Ship = baseShip;

            OnAdd?.Invoke(baseShip);
        }

        public void Add(List<Cannon> cannons) => cannons.ForEach(Add);

        public void Add(Cannon cannon)
        {
            Cannons.Add(cannon);

            OnAdd?.Invoke(cannon);
        }

        public void Remover(Cannon cannon)
        {
            Cannons.Remove(cannon);

            OnRemove?.Invoke(cannon);
        }

        public void Add(List<SurpriseDuel> surpriseDuels) => surpriseDuels.ForEach(Add);

        public void Add(SurpriseDuel surpriseDuel)
        {
            SurpriseDuel.Add(surpriseDuel);

            OnAdd?.Invoke(surpriseDuel);
        }

        public void Remove(BaseCrewMember baseCrewMember)
        {
            if (Crew.Count == 0)
                throw new EmptyCrewException();

            if (Crew.FirstOrDefault(t => t == baseCrewMember) == null)
                throw new CrewMemberNotFoundException();

            Crew.Remove(baseCrewMember);

            OnRemove?.Invoke(baseCrewMember);
        }

        public void DrownCrew()
        {
            foreach (BaseCrewMember crew in Crew.ToList())
            {
                if (crew.Drownable)
                    Remove(crew);
            }
        }

        public void RemoveDuelCards()
        {
            Cannons.Clear();
            SurpriseDuel.Clear();
        }

        public bool IsCrewFull() => Crew.Count == MaximumCrew;

        public void ChangeShip(BaseShip baseShip)
        {
            _removeShip();
            Add(baseShip);
        }

        public void AddProtected(Card card) => Protected.Add(card);

        public List<Card> GetAllProtected() => Protected.ToList();

        private void _removeShip()
        {
            if (Ship == null)
                throw new NoShipException();

            _removeAllProtected();

            OnRemove?.Invoke(Ship);

            Ship = null;
        }

        private void _removeAllProtected()
        {
            var allProtected = GetAllProtected();

            Protected = null;

            foreach (Card protectedCard in allProtected)
                OnRemove?.Invoke(protectedCard);
        }

        private int _calculateCannonDuelShots() => Cannons.Sum(c => c.Shots);

        private int _calculateSurpriseDuelShots() => SurpriseDuel.Sum(d => d.Shots);

        private int _calculateCrewDuelShots() => Crew.Sum(t => t.Shots);

        private int _calculateShipDuelShots(int shots)
        {
            if (Ship is NavalGuerrilla navalGuerrilla)
                shots += navalGuerrilla.AdditionalShots * Cannons.Count;

            else if (Ship is HellishUrchin hellishUrchin)
            {
                if (shots != 0)
                    shots += hellishUrchin.Shots;
            }

            return shots;
        }
    }
}
