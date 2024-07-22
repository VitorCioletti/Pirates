namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas.Duelo;
    using Cartas.Extensao;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Resultante;
    using Resultante.Enums;

    public class Duel : BasePrimaryAction
    {
        public Cartas.Tipos.Duel StarterCard { get; private set; }

        public Duel(Player starter, Player target, Cartas.Tipos.Duel starterCard) : base(starter, target) =>
            StarterCard = starterCard;

        public override List<BaseAction> ApplyRule(Table table)
        {
            List<Cannon> cannons = Starter.Hand.GetAll<Cannon>();

            if (cannons.Count == 0)
                throw new DoesNotHaveDuelCardException(this);

            table.EnterDuelMode();

            var chooseCannonToStartDuelling = new ChooseCannonToStartDuelling(
                this,
                Starter,
                ChoiceType.Card,
                cannons.GetIds()
            );

            var resultantActions = new List<BaseAction> {chooseCannonToStartDuelling};

            return resultantActions;
        }
    }
}
