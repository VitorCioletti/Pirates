namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;

    public class SalaNaoEncontradaExcecao : BaseSalaExcecao
    {
        public SalaNaoEncontradaExcecao(Guid idSala) :
            base("sala-nao-encontrada", $"Sala de id \"{idSala}\" não encontrada.")
        {
        }
    }
}
