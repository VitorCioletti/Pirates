namespace Piratas.Protocolo.Partida.Servidor.Escolha
{
    using System.Collections.Generic;

    public class ListaEscolhasServidor : BaseEscolha
    {
        public List<string> Opcoes { get; private set; }

        public int LimiteQuantidadeEscolha { get; private set; }

        public ListaEscolhasServidor(
            TipoEscolha tipo,
            List<string> opcoes,
            int limiteQuantidadeEscolha = 1) : base(tipo)
        {
            Opcoes = opcoes;
            LimiteQuantidadeEscolha = limiteQuantidadeEscolha;
        }
    }
}
