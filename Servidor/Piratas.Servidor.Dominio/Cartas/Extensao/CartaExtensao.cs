namespace Piratas.Servidor.Dominio.Cartas.Extensao
{
    using System.Collections.Generic;
    using System.Linq;

    public static class CartaExtensao
    {
        public static List<string> ObterIds(this IEnumerable<Carta> cartas)
        {
            return cartas.Select(c => c.Id).ToList();
        }
    }
}
