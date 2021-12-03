using System;

namespace LightsOut.Application.Exceptions
{
    public class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {
            
        }
    }
}