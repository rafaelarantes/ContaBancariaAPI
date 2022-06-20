using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Dominio.Entidades
{
    public class FilaProcessamento : Entity
    {
        public string Dados { get; private set; }

        public TipoComandoFila TipoComandoFila { get; private set; }

        public SituacaoFilaProcessamento Situacao { get; private set; }

        public DateTime DataGeracao { get; private set; }

        public DateTime? DataProcessamento { get; private set; }

        public FilaProcessamento(TipoComandoFila tipoComandoFila, string dados)
        {
            TipoComandoFila = tipoComandoFila;
            DataGeracao = DateTime.Now;
            Situacao = SituacaoFilaProcessamento.Enfileirado;
            Dados = dados;
        }

        public void Finalizado()
        {
            DataProcessamento = DateTime.Now;
            Situacao = SituacaoFilaProcessamento.Finalizado;
        }

        public void ProcessadoComErro()
        {
            Situacao = SituacaoFilaProcessamento.Erro;
        }
    }
}
