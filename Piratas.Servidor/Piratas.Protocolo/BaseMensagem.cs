namespace Piratas.Protocolo
{
    using System;

    public abstract class BaseMensagem
    {
        public Guid Id { get; private set; }

        public BaseMensagem() => Id = Guid.NewGuid();
    }
}
