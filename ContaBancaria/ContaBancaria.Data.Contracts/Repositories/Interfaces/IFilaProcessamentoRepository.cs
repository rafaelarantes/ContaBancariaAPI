using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IFilaProcessamentoRepository
    {
        void Publicar(FilaProcessamento filaProcessamento);
        
        void Receber(TipoComandoFila tipoComandoFila, Action<TipoComandoFila, string> callback);
    }
}
