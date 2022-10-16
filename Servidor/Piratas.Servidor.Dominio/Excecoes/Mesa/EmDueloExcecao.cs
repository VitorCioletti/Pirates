namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class EmDueloExcecao : BaseMesaExcecao
    {
        public EmDueloExcecao() : base("mesa-em-duelo", "Mesa já está em duelo.")
        {
        }
    }
}
