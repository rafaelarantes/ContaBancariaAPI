﻿using ContaBancaria.Dominio.Enums;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ContaBancaria.Data.Repositories
{
    public abstract class QueueRepository : Repository
    {
        private IConnection _connection;
        private IModel _channel;
        protected QueueRepository(IConfiguration configuration)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected void Publish(string dados, TipoComandoFila tipoComandoFila)
        {
            var body = Encoding.UTF8.GetBytes(dados);

            _channel.BasicPublish(exchange: "",
                                 routingKey: tipoComandoFila.ToString(),
                                 basicProperties: null,
                                 body: body);
        }

        protected void Received(TipoComandoFila tipoComandoFila, 
                                 EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += eventHandler;

            _channel.BasicConsume(queue: tipoComandoFila.ToString(),
                                 autoAck: true,
                                 consumer: consumer);
        }

        protected override void Commit()
        {
        }

        protected override void Rollback()
        {
            _channel.Dispose();
        }
    }
}
