namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;
    using ServidorPiratas.Regras.Acoes.Tipos;

    public abstract class Tripulacao : Carta
    {
        public Tripulacao(string nome) : base(nome) { }

        public int Tiros { get; protected set; }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador.Campo);

        internal Resultante _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            return null;
        }
    }
}