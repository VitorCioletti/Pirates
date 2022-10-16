namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class SemAcaoValidaExcecao : BaseCartaExcecao
    {
        public SemAcaoValidaExcecao(Carta cartaJogada) :
            base(cartaJogada, "sem-acao-valida", "Nenhuma ação válida encontrada.")
        {
        }
    }
}
