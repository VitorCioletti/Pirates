namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class GuerrilhaNaval : Embarcacao
    {
        public int TirosAdicionais { get; private set; } = 2;
 
        public GuerrilhaNaval(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => null; 
    }
}