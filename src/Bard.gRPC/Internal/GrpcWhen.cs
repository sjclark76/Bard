using System;
using Bard.Infrastructure;
using Bard.Internal.Then;
using Bard.Internal.When;
using Grpc.Core;

namespace Bard.gRPC.Internal
{
    internal class When<TGrpcClient> : When, IWhen<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        private readonly Func<TGrpcClient> _grpcClientFactory;
        private readonly EventAggregator _eventAggregator;

        internal When(Func<TGrpcClient> grpcClientFactory, EventAggregator eventAggregator, Api api,
            LogWriter logWriter, Action preApiCall) : base(
            api, eventAggregator, logWriter, preApiCall)
        {
            _grpcClientFactory = grpcClientFactory;
            _eventAggregator = eventAggregator;
        }

        public TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall)
        {
            PreApiCall?.Invoke();

            WriteHeader();

            var gRpcClient = _grpcClientFactory();

            var response = grpcCall(gRpcClient);

            _eventAggregator.PublishGrpcResponse(new GrpcResponse(response));
            
            return response;
        }
    }
}