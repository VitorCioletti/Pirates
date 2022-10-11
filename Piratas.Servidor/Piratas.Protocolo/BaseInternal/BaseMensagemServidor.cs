namespace Piratas.Protocolo.BaseInternal
{
    public abstract class BaseMensagemServidor : BaseMensagem
    {
        public string IdErro { get; private set; }

        public bool PossuiErro => !string.IsNullOrWhiteSpace(IdErro);

        protected BaseMensagemServidor(string idErro)
        {
            IdErro = idErro;
        }
    }
}
