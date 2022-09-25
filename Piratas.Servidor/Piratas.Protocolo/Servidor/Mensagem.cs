namespace Piratas.Protocolo.Servidor
{
    using System;
    using System.Collections.Generic;

    public class Mensagem : Pacote
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public Escolha Escolha { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public string IdErro { get; private set; }

        public bool PossuiErro => !string.IsNullOrWhiteSpace(IdErro);

        public Mensagem(
            Guid idJogador,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
            Escolha escolha,
            string idErro
        ) : base(idJogador)
        {
            Escolha = escolha;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
            IdErro = idErro;
        }
    }
}
