using System;
using Bard.Infrastructure;
using Bard.Internal.When;
using Grpc.Core;

namespace Bard.gRPC
{
    internal class GrpcWhen<TGrpcClient> : When, IGrpcWhen<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        public TResponse Grpc<TResponse>(Func<TGrpcClient, TResponse> grpcCall)
        {
            WriteHeader();
            
            var response = grpcCall(GrpcClient);

            LogWriter.WriteObjectToConsole(response);
            
            return response;
        }

        public TGrpcClient GrpcClient { get; }

        internal GrpcWhen(TGrpcClient grpcClient, Api api, LogWriter logWriter, Action preApiCall) : base(api, logWriter, preApiCall)
        {
            GrpcClient = grpcClient;
        }
    }
}