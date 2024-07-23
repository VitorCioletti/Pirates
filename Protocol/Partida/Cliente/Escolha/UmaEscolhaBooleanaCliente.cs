namespace Piratas.Protocolo.Partida.Cliente.Escolha
{
    public class UmaEscolhaBooleanaCliente : BaseEscolha
    {
        public bool Escolha { get; private set; }

        public UmaEscolhaBooleanaCliente(TipoEscolha tipo, bool escolha) : base(tipo)
        {
            Escolha = escolha;
        }
    }
}
