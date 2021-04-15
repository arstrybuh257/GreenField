using System;

namespace GreenField.Common
{
    public class GreenFieldException : Exception
    {
        public string Code { get; }

        public GreenFieldException()
        {
        }

        public GreenFieldException(string code)
        {
            Code = code;
        }

        public GreenFieldException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public GreenFieldException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public GreenFieldException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public GreenFieldException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}