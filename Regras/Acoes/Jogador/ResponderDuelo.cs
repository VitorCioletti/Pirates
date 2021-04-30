namespace ServidorPiratas.Regras.Acoes.Tipos.Resultante
{
    using Regras;

    public class ResponderDuelo : Acao
    {
        public ResponderDuelo(Jogador realizador) : base(realizador) { }

        public override void AplicaRegra(Mesa mesa)
        {
        } 
    }
}