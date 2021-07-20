namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;
    using ServidorPiratas.Regras.Acoes.Tipos;

    public abstract class Tripulacao : Carta
    {
        public int Tiros { get; protected set; }

        public bool PermiteAfogamento { get; protected set; }

        public Tripulacao(string nome) : base(nome)
        {
            PermiteAfogamento = true;
        }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador.Campo);

        internal Resultante _aplicarEfeito(Campo campoRealizador)
        {
            campoRealizador.Adicionar(this);

            return null;
        }
    }
}