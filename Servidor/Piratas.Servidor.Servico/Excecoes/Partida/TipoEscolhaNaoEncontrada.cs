namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class TipoEscolhaNaoEncontrada : BasePartidaExcecao
    {
        public TipoEscolhaNaoEncontrada(int tipoEscolha) :
            base("tipo-escolha-nao-encontrada", $"Tipo escolha \"{tipoEscolha}\" não encontrada.")
        {
        }
    }
}
