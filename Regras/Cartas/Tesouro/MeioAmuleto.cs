namespace ServidorPiratas.Regras.Cartas.Tesouro
{
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System;

    public class MeioAmuleto : Tesouro
    {
        private static int _quantidadeParaCompletar = 2;

        private static int _valorAmuletoCompleto = 2; 

        public MeioAmuleto(string nome) : base(nome, 0) { }

        public static int CalcularPontosTesouro(List<MeioAmuleto> amuletos) 
        {
            var amuletosCompletos = (int)(amuletos.Count / _quantidadeParaCompletar);

            return amuletosCompletos * _valorAmuletoCompleto;
        }
    }
}