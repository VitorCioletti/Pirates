namespace Piratas.Protocolo.Servidor
{
    using System;
    using System.Collections.Generic;
    using Acoes;

    public class Mensagem : Pacote
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public List<Primaria> PrimariasDisponiveis { get; private set; }

        // TODO: Adicionar resultante Escolha.

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public Mensagem(
            Guid idJogador,
            int acoesRestantes,
            int tesouros,
            List<Primaria> primariasDisponiveis,
            Dictionary<Guid, List<Evento>> eventos) : base(idJogador)
        {
            AcoesRestantes = acoesRestantes;
            PrimariasDisponiveis = primariasDisponiveis;
            Eventos = eventos;
            Tesouros = tesouros;
        }
    }
}
