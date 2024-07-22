namespace Piratas.Servidor.Dominio.Cartas.Duelo
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Excecoes.Cartas;
    using Tipos;

    public class Helmsman : Duel
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            if (action is not DrawDuelAnswerCard)
                throw new CanOnlyBeUsedInDuelException(this);

            table.EndDuelMode();

            return null;
        }
    }
}
