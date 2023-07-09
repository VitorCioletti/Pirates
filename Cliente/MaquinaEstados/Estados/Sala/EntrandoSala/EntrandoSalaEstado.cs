namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.EntrandoSala
{
    using System;
    using Servicos;

    public class EntrandoSalaEstado : BaseEstado
    {
        private readonly string _idSala;

        public EntrandoSalaEstado(string idSala, MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
            _idSala = idSala;
        }

        public override void Inicializar()
        {
            Console.WriteLine($"Tentando entrar na sala \"{_idSala}\".");

            SalaServico.EntrarSala(_idSala);

            Remover();
        }

        public override BaseResultadoEstado Limpar()
        {
            Console.Clear();

            return new EntrandoSalaResultadoEstado(null);
        }
    }
}
