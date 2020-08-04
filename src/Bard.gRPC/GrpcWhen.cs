using System;
using Bard.Infrastructure;
using Bard.Internal.When;
using Grpc.Core;

namespace Bard.gRPC
{
    internal class GrpcWhen<TGrpcClient> : When, IGrpcWhen<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        private readonly Func<TGrpcClient> _grpcClientFactory;

        internal GrpcWhen(Func<TGrpcClient> grpcClientFactory, Api api, LogWriter logWriter, Action preApiCall) : base(
            api, logWriter, preApiCall)
        {
            _grpcClientFactory = grpcClientFactory;
        }

        public TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall)
        {
            WriteHeader();

            var gRpcClient = _grpcClientFactory();

            var response = grpcCall(gRpcClient);

            LogWriter.WriteObjectToConsole(response);

            return response;
        }
    }
}