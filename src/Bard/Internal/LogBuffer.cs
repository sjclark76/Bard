using System;
using System.Collections.Generic;

namespace Bard.Internal
{
    internal class LogBuffer
    {
        private readonly Action<string> _logMessage;
        private readonly Stack<string> _buffer;
        
        internal LogBuffer(Action<string> logMessage)
        {
            _logMessage = logMessage;
            _buffer = new Stack<string>();
        }

        internal void Push(string message)
        {
            _buffer.Push(message);
        }

        internal void Clear()
        {
            while (_buffer.Count > 0)
            {
                var message = _buffer.Pop();
                _logMessage(message);
            }
        }
    }
}