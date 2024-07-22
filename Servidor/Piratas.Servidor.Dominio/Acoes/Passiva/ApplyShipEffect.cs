namespace Piratas.Servidor.Dominio.Acoes.Passiva
{
    using System.Collections.Generic;
    using Cartas.Embarcacao;

    public class ApplyShipEffect : BasePassiveAction
    {
        private BaseShip BaseShip { get; set; }

        public ApplyShipEffect(Player starter, BaseShip baseShip) : base(starter) =>
            BaseShip = baseShip;

        public override List<BaseAction> ApplyRule(Table table)
        {
            return BaseShip.ApplyEffect(this, table);
        }
    }
}
