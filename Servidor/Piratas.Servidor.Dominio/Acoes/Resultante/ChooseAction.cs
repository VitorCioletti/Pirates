namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Enums;

    public class ChooseAction : BaseResultantWithChoiceList
    {
        private List<BaseAction> _actionChoices { get; set; }

        public ChooseAction(BaseAction origin, Player starter, params BaseAction[] actionChoices)
            : base(
                origin,
                starter,
                ChoiceType.Action,
                actionChoices.Select(r => r.Id).ToList())
        {
            _actionChoices = actionChoices.ToList();
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            BaseAction choosenAction = _actionChoices.First(r => r.Id == choice);

            return choosenAction.ApplyRule(table);
        }
    }
}
