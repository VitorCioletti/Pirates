﻿namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using System.Collections.Generic;
    using Enums;

    public abstract class BaseResultanteComListaEscolhaBooleana : BaseResultanteComEscolha
    {
        public List<string> Itens { get; private set; }

        protected bool EscolhaBooleana { get; private set; }

        protected BaseResultanteComListaEscolhaBooleana(
            Acao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            List<string> itens,
            Jogador alvo = null)
            : base(
                origem,
                realizador,
                tipoEscolha,
                alvo)
        {
            Itens = itens;
            EscolhaBooleana = false;
        }

        public void PreencherEscolha(bool escolhaBooleana)
        {
            EscolhaBooleana = escolhaBooleana;
        }
    }
}
