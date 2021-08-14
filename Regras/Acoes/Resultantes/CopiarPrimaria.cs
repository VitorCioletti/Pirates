namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Regras;
    using Tipos;

    public class CopiarPrimaria: Resultante
    {
        public Primaria Copiada { get; private set; }

        public CopiarPrimaria(Acao origem, Jogador realizador, Primaria copiada) : base(origem, realizador) => 
            Copiada = copiada;

        public override Resultante AplicarRegra(Mesa mesa) => Copiada.AplicarRegra(mesa);
    }
}