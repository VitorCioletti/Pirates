namespace ServidorPiratas.Entidades.Acoes.Tipos.Resultante
{
    using Entidades;

    public class ResponderDuelo : Acao
    {
        public ResponderDuelo(Jogador realizador) : base(realizador) { }

        public override void AplicaRegra(Mesa mesa)
        {
        } 
    }
}