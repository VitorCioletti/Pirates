namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using System;
    using Tipos;

    public class Timoneiro : Duelo
    {
        public Timoneiro(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal Resultante _aplicarEfeito(Acao acao, Mesa mesa)
        {
            if (!(acao is ResponderDuelo))
                throw new Exception("Carta só pode ser usada em resposta a um duelo.");

            if (!mesa.EmDuelo)
                throw new Exception("A mesa não está em duelo");

            mesa.EmDuelo = false;
            mesa.Duelistas = null;

            return null;
        }
    }
}