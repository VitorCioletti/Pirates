namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Enums;
    using Imediata;

    public class ChooseCannonToStartDuelling : BaseResultantWithChoiceList
    {
        public ChooseCannonToStartDuelling(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            List<string> options)
            : base(
                origin,
                starter,
                choiceType,
                options)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            var starterCanon = (Cannon)Starter.Hand.GetById(Options[0]);

            starterCanon.ApplyEffect(this, table);

            table.EnterDuelMode();

            BaseAction nextAction = !Target.Hand.Exists<Duel>()
                ? new CalculateDuelResult(Starter, Target)
                : new DrawDuelAnswerCard(this, Target, Starter);

            var resultantActions = new List<BaseAction> {nextAction};

            return resultantActions;
        }
    }
}
