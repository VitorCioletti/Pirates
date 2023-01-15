namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.CriandoSala
{
    using Protocolo.Sala.Servidor;

    public class CriandoSalaResultadoEstado : BaseResultadoEstado
    {
        public MensagemSalaServidor MensagemSalaServidor { get; private set; }

        public CriandoSalaResultadoEstado(MensagemSalaServidor mensagemSalaServidor)
        {
            MensagemSalaServidor = mensagemSalaServidor;
        }
    }
}
