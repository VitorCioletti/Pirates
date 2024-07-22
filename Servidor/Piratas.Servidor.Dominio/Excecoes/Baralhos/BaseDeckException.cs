namespace Piratas.Servidor.Dominio.Baralhos.Excecoes
{
    using Dominio.Excecoes;

    public class BaseDeckException : BaseDomainException
    {
        public BaseDeckException(string id, string message) : base(id, message)
        {
        }
    }
}
