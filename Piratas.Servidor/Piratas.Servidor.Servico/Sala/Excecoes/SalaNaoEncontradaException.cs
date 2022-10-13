namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;
    using Servico.Excecoes;

    public class SalaNaoEncontradaException : BaseSalaException
    {
        public SalaNaoEncontradaException(Guid idSala) :
            base("sala-nao-encontrada", $"Sala de id \"{idSala}\" não encontrada.")
        {
        }
    }
}
