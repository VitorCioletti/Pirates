namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class SemDueloExcecao : BaseMesaExcecao
    {
        public SemDueloExcecao() : base("mesa-sem-duelo", "Mesa sem duelo.")
        {
        }
    }
}
