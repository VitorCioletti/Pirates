namespace Pirates.Server.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using Card.Crew;
using Card.Treasure;

public class Player
{
    public string Id { get; }

    public int AvailableActions { get; private set; }

    public Hand Hand { get; }

    public Field Field { get; }

    public Player(
        string id,
        Action<string, Card.Card> onAddCardAtHand,
        Action<string, Card.Card> onRemoveCardAtHand,
        Action<string, Card.Card> onAddCardAtField,
        Action<string, Card.Card> onRemoveCardAtField)
    {
        Id = id;
        Hand = new Hand(new List<Card.Card>());
        Field = new Field();

        Hand.OnAdd += OnAddCardAtHand;
        Hand.OnRemove += OnRemoveCardAtHand;
        Field.OnAdd += OnAddCardAtField;
        Field.OnRemove += OnRemoveCardAtField;

        void OnAddCardAtHand(Card.Card card) =>
            onAddCardAtHand?.Invoke(Id, card);

        void OnRemoveCardAtHand(Card.Card card) =>
            onRemoveCardAtHand?.Invoke(Id, card);

        void OnAddCardAtField(Card.Card card) =>
            onAddCardAtField?.Invoke(Id, card);

        void OnRemoveCardAtField(Card.Card card) =>
            onRemoveCardAtField?.Invoke(Id, card);
    }

    public void ResetAvailableActions(int actions)
    {
        AvailableActions = actions;
    }

    public void SubtractAvailableActions()
    {
        AvailableActions--;
    }

    public int CalculateTreasurePoints()
    {
        int treasures = 0;

        treasures += _calculateHaldAmuletTreasures();
        treasures += _calulateTreasureCardsAtHand();
        treasures += _calculateProtectedTreasures();
        treasures += _calculateNoblePiratesTreasure();

        return treasures;
    }

    public override string ToString() => Id;

    public override bool Equals(object obj) => Equals(obj as Player);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Player jogador1, Player jogador2)
    {
        if (ReferenceEquals(jogador1, jogador2))
            return true;

        if (ReferenceEquals(jogador1, null))
            return false;

        if (ReferenceEquals(jogador2, null))
            return false;

        return jogador1.Equals(jogador2);
    }

    public static bool operator !=(Player player1, Player player2) => !(player1 == player2);

    private bool Equals(Player otherPlayer)
    {
        if (ReferenceEquals(otherPlayer, null))
            return false;

        if (ReferenceEquals(otherPlayer, this))
            return true;

        return Id == otherPlayer.Id;
    }

    private int _calculateNoblePiratesTreasure()
    {
        int noblePiratesTreasure =
            Field.Crew.Where(t => t is NoblePirate).Sum(t => ((NoblePirate)t).Treasures);

        return noblePiratesTreasure;
    }

    private int _calculateProtectedTreasures()
    {
        IEnumerable<Treasure> protectedTreasures = Field.GetAllProtected().OfType<Treasure>();

        int allProtectedTreasures = protectedTreasures.Sum(c => c.Value);

        return allProtectedTreasures;
    }

    private int _calulateTreasureCardsAtHand()
    {
        List<Treasure> treasuresAtHand = Hand.GetAll<Treasure>();

        int treasurePoints = 0;

        foreach (Treasure treasure in treasuresAtHand)
        {
            if (treasure is HalfAmulet)
                continue;

            treasurePoints = treasuresAtHand.Sum(c => c.Value);
        }

        return treasurePoints;
    }

    private int _calculateHaldAmuletTreasures()
    {
        List<HalfAmulet> allHalfAmulets = Hand.GetAll<HalfAmulet>();

        int treasurePoints = HalfAmulet.CalulateTreasurePoints(allHalfAmulets);

        return treasurePoints;
    }
}
