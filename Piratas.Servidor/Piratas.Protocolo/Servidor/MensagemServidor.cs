namespace Piratas.Protocolo.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemServidor : Mensagem
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public EscolhaServidor EscolhaServidor { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public string IdErro { get; private set; }

        public bool PossuiErro => !string.IsNullOrWhiteSpace(IdErro);

        public MensagemServidor(
            Guid idJogador,
            Guid idMesa,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
            EscolhaServidor escolhaServidor,
            string idErro
        ) : base(idJogador, idMesa)
        {
            EscolhaServidor = escolhaServidor;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
            IdErro = idErro;
        }

        public MensagemServidor(string idErro) : this(
            Guid.Empty,
            Guid.Empty,
            0,
            0,
            null,
            null,
            idErro)
        {
        }
    }
}
