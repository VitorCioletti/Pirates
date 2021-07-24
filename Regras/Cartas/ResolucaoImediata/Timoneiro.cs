namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using System;
    using Tipos;

    public class Timoneiro : ResolucaoImediata
    {
        public Timoneiro(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao _, Mesa mesa) => _aplicarEfeito(mesa);

        internal Resultante _aplicarEfeito(Mesa mesa)
        {
            if (!mesa.EmDuelo)
                throw new Exception("A mesa não está em duelo");

            mesa.EmDuelo = false;
            mesa.Duelistas = null;

            return null;
        }
    }
}