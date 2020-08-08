using System;
using Bard.Infrastructure;
using Bard.Internal.When;
using Grpc.Core;

namespace Bard.gRPC.Internal
{
    internal class When<TGrpcClient> : When, IWhen<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        private readonly Func<TGrpcClient> _grpcClientFactory;

        internal When(Func<TGrpcClient> grpcClientFactory, Api api, LogWriter logWriter, Action preApiCall) : base(
            api, logWriter, preApiCall)
        {
            _grpcClientFactory = grpcClientFactory;
        }

        public TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall)
        {
            PreApiCall();

            WriteHeader();

            var gRpcClient = _grpcClientFactory();

            var response = grpcCall(gRpcClient);

            LogWriter.LogObject(response);

            return response;
        }
    }
}