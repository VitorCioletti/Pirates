namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.EntrandoSala
{
    using Protocolo.Sala.Servidor;

    public class EntrandoSalaResultadoEstado : BaseResultadoEstado
    {
        public MensagemSalaServidor MensagemSalaServidor { get; private set; }

        public EntrandoSalaResultadoEstado(MensagemSalaServidor mensagemSalaServidor)
        {
            MensagemSalaServidor = mensagemSalaServidor;
        }
    }
}
