namespace Piratas.Servidor.Dominio.Cartas.Tesouro
{
    using System.Collections.Generic;

    public class HalfAmulet : Treasure
    {
        private const int _quantityToComplete = 2;

        private const int _valueCompleteAmulet = 2;

        public HalfAmulet() : base(0)
        {
        }

        public static int CalulateTreasurePoints(List<HalfAmulet> amulets)
        {
            int completeAmulets = amulets.Count / _quantityToComplete;

            return completeAmulets * _valueCompleteAmulet;
        }
    }
}
