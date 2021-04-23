﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AvitoCheсker.Api.Operations;
using AvitoCheсker.Api.Operations.Parameters;
using AvitoCheсker.Api.Operations.Returns;

namespace AvitoCheсker.Api
{
    public class Client : IDisposable
    {

        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public Client(IWebProxy proxy = null)
        {
            var cookie = new CookieContainer();
            _handler = new HttpClientHandler {CookieContainer = cookie};
            if (proxy != null)
                _handler.Proxy = proxy;

            _client = new HttpClient(_handler);
        }

        public async Task<IOperationReturn> ExecuteOperation(IOperation operation, IOperationParameter parameters = null)
        {
            var result = await operation.Execute(_client, parameters);
            operation.Dispose();
            return result;
        }


        public void Dispose()
        {
            _client?.Dispose();
            _handler?.Dispose();
        }
    }
}
