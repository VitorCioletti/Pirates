namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class HellishUrchin : BaseShip
    {
        public int Shots { get; private set; } = 3;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table) => null;
    }
}
