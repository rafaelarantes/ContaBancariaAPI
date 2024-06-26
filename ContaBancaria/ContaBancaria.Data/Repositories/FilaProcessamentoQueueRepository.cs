﻿using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace ContaBancaria.Data.Repositories
{
    public class FilaProcessamentoQueueRepository : QueueRepository, IFilaProcessamentoQueueRepository
    {
        public FilaProcessamentoQueueRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void Publicar(FilaProcessamento filaProcessamento)
        {
            Publish(filaProcessamento.Dados, filaProcessamento.TipoComandoFila);
        }

        public void Receber(TipoComandoFila tipoComandoFila, 
                            Action<TipoComandoFila, string> callback)
        {
            Received(tipoComandoFila, (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                
                callback.Invoke(tipoComandoFila, json);
            });
        }
    }
}
