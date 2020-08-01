using System;
using Bard.gRPC;
using Bard.Infrastructure;
using Grpc.Core;

namespace Bard.Internal.When
{
    internal class GrpcWhen<TGrpcClient> : When, IGrpcWhen<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        internal GrpcWhen(TGrpcClient grpcClient, Api api, LogWriter logWriter, Action preApiCall) : base(api,
            logWriter, preApiCall)
        {
            GrpcClient = grpcClient;
        }

        public TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall)
        {
            WriteHeader();

            var response = grpcCall(GrpcClient);

            LogWriter.WriteObjectToConsole(response);

            return response;
        }

        public TGrpcClient GrpcClient { get; }
    }
}