namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;
    using Excecoes.Cartas;
    using Tripulacao;

    public class CallCrew : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            DiscardDeck discardDeck = table.DiscardDeck;
            Player playerExecuting = action.Starter;

            if (playerExecuting.Field.IsCrewFull())
                throw new FullCrewException(this, playerExecuting);

            List<Card> discardedCrewMembers = discardDeck.GetAll<BaseCrewMember>().OfType<Card>().ToList();

            if (discardedCrewMembers.Count == 0)
                throw new NoCrewMemberInDiscardDeckException(this);

            var chooseCardInDeck = new ChooseCardInDeck(
                action,
                playerExecuting,
                discardDeck,
                discardedCrewMembers);

            var resultantActions = new List<BaseAction> { chooseCardInDeck };

            return resultantActions;
        }
    }
}
