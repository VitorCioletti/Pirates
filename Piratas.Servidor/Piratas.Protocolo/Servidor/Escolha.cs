namespace Piratas.Protocolo.Servidor
{
    using System;
    using System.Collections.Generic;

    public class Escolha<T>
    {
        public List<T> Opcoes { get; private set; }

        public List<T> Escolhas { get; private set; }

        public Escolha(Guid idJogador, T escolhido, List<T> opcoes)
            : this(idJogador, new List<T> { escolhido }, opcoes) { }

        public Escolha(Guid idJogador, List<T> escolhas, List<T> opcoes) : base(idJogador)
        {
            Escolhas = escolhas;
            Opcoes = opcoes;
        }

        public void Adiciona(T escolha)
        {
            Escolhas.Add(escolha);
        }

        public void Adiciona(List<T> escolhas)
        {
            foreach (var escolha in escolhas)
                Adiciona(escolha);
        }
    }
}
