using System;
using System.Runtime.Serialization;

namespace CustomersData
{
    [Serializable]
    public class BankDateException : Exception
    {
        public BankDateException()
        {
        }

        public BankDateException(string message) : base(message)
        {
        }

        public BankDateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}