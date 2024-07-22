namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using System.Collections.Generic;
    using Primaria;

    public class CopyPrimmary : BaseImmediateAction
    {
        private BasePrimaryAction _copied { get; set; }

        public CopyPrimmary(Player starter, BasePrimaryAction copied) : base(starter) => _copied = copied;

        public override List<BaseAction> ApplyRule(Table table) => _copied.ApplyRule(table);
    }
}
