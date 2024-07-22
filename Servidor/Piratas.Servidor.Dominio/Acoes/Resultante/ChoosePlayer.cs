namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Enums;

    public class ChoosePlayer : BaseResultantWithChoiceList
    {
        private Func<BaseAction, Player, List<BaseAction>> _resultantAfterChoosing { get; set; }

        public ChoosePlayer(
            BaseAction origin,
            Player starter,
            List<string> players,
            Func<BaseAction, Player, List<BaseAction>> resultantAfterChoosing)
            : base(
                origin,
                starter,
                ChoiceType.Player,
                players)
        {
            _resultantAfterChoosing = resultantAfterChoosing;
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            Player chosenPlayer = table.Players.First(j => j.Id.ToString() == choice);

            return _resultantAfterChoosing(this, chosenPlayer);
        }
    }
}
