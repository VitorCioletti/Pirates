namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class SemAcaoValidaException : CartaException
    {
        public SemAcaoValidaException(Carta cartaJogada) : base(cartaJogada, "Nenhuma ação válida encontrada.")
        {
        }
    }
}