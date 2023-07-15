namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using Protocolo.Sala.Servidor;

    public abstract class BaseEstado
    {
        protected MaquinaEstados MaquinaEstados { get; set; }

        protected BaseEstado(MaquinaEstados maquinaEstados)
        {
            MaquinaEstados = maquinaEstados;
        }

        public virtual void Inicializar()
        {
        }

        public virtual BaseResultadoEstado Limpar()
        {
            return null;
        }

        public virtual void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
        {
        }

        public virtual void AoReceberTexto(string texto)
        {
        }

        protected void Remover()
        {
            MaquinaEstados.Remover(this);
        }

        public virtual void AoCriarSala(MensagemSalaServidor mensagemSalaServidor)
        {
        }

        public virtual void AoSairSala(MensagemSalaServidor mensagemSalaServidor)
        {
        }

        public virtual void AoIniciarPartida(MensagemSalaServidor mensagemSalaServidor)
        {
        }

        public virtual void AoEntrarSala(MensagemSalaServidor mensagemSalaServidor)
        {
        }
    }
}
