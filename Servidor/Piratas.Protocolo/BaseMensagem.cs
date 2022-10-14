namespace Piratas.Protocolo
{
    using System;

    public abstract class BaseMensagem
    {
        public Guid Id { get; private set; }

        public string IdErro { get; private set; }

        public string DescricaoErro { get; private set; }

        public bool PossuiErro => !string.IsNullOrWhiteSpace(IdErro);

        protected BaseMensagem(string idErro = null, string descricaoErro = null)
        {
            Id = Guid.NewGuid();
            IdErro = idErro;
            DescricaoErro = descricaoErro;
        }
    }
}
