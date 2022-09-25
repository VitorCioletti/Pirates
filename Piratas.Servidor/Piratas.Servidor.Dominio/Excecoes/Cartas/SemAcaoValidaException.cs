namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class SemAcaoValidaException : CartaException
    {
        public SemAcaoValidaException(Carta cartaJogada) :
            base(cartaJogada, "sem-acao-valida", "Nenhuma ação válida encontrada.")
        {
        }
    }
}
