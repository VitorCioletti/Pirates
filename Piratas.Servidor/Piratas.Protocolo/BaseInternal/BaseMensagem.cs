namespace Piratas.Protocolo.BaseInternal
{
    using System;

    public abstract class BaseMensagem
    {
        public Guid Id { get; private set; }

        public BaseMensagem() => Id = Guid.NewGuid();
    }
}
