namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class NavalGuerrilla : BaseShip
    {
        public int AdditionalShots { get; private set; } = 2;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table) => null;
    }
}
