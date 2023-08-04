namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using System;
    using System.Threading;
    using Protocolo;

    public class ErroEstado : BaseEstado
    {
        private readonly string _idErro;

        private readonly string _descricaoErro;

        public ErroEstado(Mensagem mensagem, MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
            if (!mensagem.PossuiErro)
                throw new InvalidOperationException($"Mensagem \"{mensagem.Id}\"não possui erro.");

            _idErro = mensagem.IdErro;
            _descricaoErro = mensagem.DescricaoErro;
        }

        public override void Inicializar()
        {
            Console.WriteLine("Ocorreu um erro");

            Console.WriteLine($"Id do erro: \"{_idErro}\".");
            Console.WriteLine($"Descrição do erro: \"{_descricaoErro}\".");

            Thread.Sleep(1000);

            Remover();
        }

        public override BaseResultadoEstado Limpar()
        {
            return new ErroResultadoEstado();
        }
    }
}
