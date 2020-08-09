namespace Bard.Internal.Then
{
    internal class GrpcResponse
    {
        public object? Response { get; }

        internal GrpcResponse(object? response)
        {
            Response = response;
        }
    }
}