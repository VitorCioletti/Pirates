namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class LookAtPlayerCards : BaseResultantWithBooleanListChoice
    {
        public LookAtPlayerCards(
            BaseAction origin,
            Player starter,
            List<Card> cards)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cards.GetIds()) {}

        public override List<BaseAction> ApplyRule(Table table) => null;
    }
}
