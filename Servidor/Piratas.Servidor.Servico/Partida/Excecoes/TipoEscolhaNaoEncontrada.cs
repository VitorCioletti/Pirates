namespace Piratas.Servidor.Servico.Partida.Excecoes;

public class TipoEscolhaNaoEncontrada : BasePartidaException
{
    public TipoEscolhaNaoEncontrada(int tipoEscolha) :
        base("tipo-escolha-nao-encontrada", $"Tipo escolha \"{tipoEscolha}\" não encontrada.")
    {
    }
}
