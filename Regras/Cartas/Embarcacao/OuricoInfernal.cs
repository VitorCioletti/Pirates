namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class OuricoInfernal : Embarcacao
    {
        public int Tiros { get; private set; } = 3;

        public OuricoInfernal(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}