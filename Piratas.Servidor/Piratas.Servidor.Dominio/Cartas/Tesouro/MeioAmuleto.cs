namespace Piratas.Servidor.Dominio.Cartas.Tesouro
{
    using Tipos;
    using System.Collections.Generic;

    public class MeioAmuleto : Tesouro
    {
        private static readonly int _quantidadeParaCompletar = 2;

        private static readonly int _valorAmuletoCompleto = 2;

        public MeioAmuleto(int valor) : base(valor)
        {
        }

        public static int CalcularPontosTesouro(List<MeioAmuleto> amuletos)
        {
            var amuletosCompletos = amuletos.Count / _quantidadeParaCompletar;

            return amuletosCompletos * _valorAmuletoCompleto;
        }
    }
}
