namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using System.Collections.Generic;
    using Resultante;

    public class CalculateDuelResult : BaseImmediateAction
    {
        public Player Winner { get; private set; }

        public Player Loser { get; private set; }

        public CalculateDuelResult(Player starter, Player target) : base(starter, target)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            int starterDuelShots = Starter.Field.CalculateDuelShots();
            int targetDuelShots = Target.Field.CalculateDuelShots();

            if (starterDuelShots > targetDuelShots)
            {
                Winner = Starter;
                Loser = Target;
            }
            else
            {
                Winner = Target;
                Loser = Starter;
            }

            Winner.Field.RemoveDuelCards();
            Loser.Field.RemoveDuelCards();

            Loser.Field.DrownCrew();
            Loser.Field.DamageShip();

            table.EndDuelMode();

            var stealCard = new StealCard(this, Winner, Loser);
            var resultantActions = new List<BaseAction> {stealCard};

            return resultantActions;
        }
    }
}
