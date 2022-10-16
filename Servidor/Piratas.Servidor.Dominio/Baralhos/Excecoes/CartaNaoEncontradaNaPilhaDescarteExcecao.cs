namespace Piratas.Servidor.Dominio.Baralhos.Excecoes;

public class CartaNaoEncontradaNaPilhaDescarteExcecao : BaseBaralhoExcecao
{
    public CartaNaoEncontradaNaPilhaDescarteExcecao(string idCarta) :
        base("carta-nao-encontrada-na-pilha-descarte", $"Carta \"{idCarta}\" não encontrada na pilha de descarte.")
    {
    }
}
