using System.Threading.Tasks;
using Bard.Infrastructure;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Bard.gRPC.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class BardClientInterceptor : Interceptor
    {
        private readonly LogWriter _logWriter;

        public BardClientInterceptor(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logWriter.LogMessage($"REQUEST: {context.Method.FullName}");
            _logWriter.LogObject(request);
            var response = base.BlockingUnaryCall(request, context, continuation);
            
            //_logWriter.LogMessage(string.Empty);
            // _logWriter.LogMessage("RESPONSE:");
            // _logWriter.LogObject(response);
            return response;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logWriter.LogMessage($"REQUEST: {context.Method}");
            _logWriter.LogObject(request);
            var response = base.UnaryServerHandler(request, context, continuation);
            // _logWriter.LogMessage(string.Empty);
            // _logWriter.LogMessage("RESPONSE:");
            // _logWriter.LogObject(response);
            return response;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            _logWriter.LogMessage($"REQUEST: {context.Method.FullName}");
            _logWriter.LogObject(request);
            var response = base.AsyncUnaryCall(request, context, continuation);
            
            // _logWriter.LogMessage(string.Empty);
            // _logWriter.LogMessage("RESPONSE:");
            // _logWriter.LogObject(response);
            return response;
        }
    }
}