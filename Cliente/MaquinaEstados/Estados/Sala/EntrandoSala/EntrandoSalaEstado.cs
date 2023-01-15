namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.EntrandoSala
{
    using System;
    using Protocolo.Sala.Servidor;
    using Servicos;

    public class EntrandoSalaEstado : BaseEstado
    {
        private readonly string _idSala;
        private MensagemSalaServidor _mensagemSalaServidor;

        public EntrandoSalaEstado(string idSala, MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
            _idSala = idSala;
        }

        public override void Inicializar()
        {
            Console.WriteLine($"Tentando entrar na sala \"{_idSala}\".");

            MensagemSalaServidor mensagemSalaServidor = SalaServico.EntrarSala(_idSala);

            _mensagemSalaServidor = mensagemSalaServidor;

            Remover();
        }

        public override BaseResultadoEstado Limpar()
        {
            Console.Clear();

            return new EntrandoSalaResultadoEstado(_mensagemSalaServidor);
        }
    }
}
