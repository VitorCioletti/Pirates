namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using Excecoes.Cartas;

    public abstract class Embarcacao : Carta
    {
        public int Vida { get; private set; } = 3;

        public void Danificar(int dano)
        {
            if (Vida == 0)
                throw new EmbarcacaoSemVidaExcecao(this);

            Vida -= dano;
        }
    }
}
