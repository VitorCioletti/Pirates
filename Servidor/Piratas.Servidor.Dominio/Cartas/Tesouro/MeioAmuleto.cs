namespace Piratas.Servidor.Dominio.Cartas.Tesouro
{
    using System.Collections.Generic;

    public class MeioAmuleto : Tesouro
    {
        private const int _quantidadeParaCompletar = 2;

        private const int _valorAmuletoCompleto = 2;

        public MeioAmuleto(int valor) : base(valor)
        {
        }

        public static int CalcularPontosTesouro(List<MeioAmuleto> amuletos)
        {
            int amuletosCompletos = amuletos.Count / _quantidadeParaCompletar;

            return amuletosCompletos * _valorAmuletoCompleto;
        }
    }
}
