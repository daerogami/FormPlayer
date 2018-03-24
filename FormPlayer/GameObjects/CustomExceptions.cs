using System;
using System.Collections.Generic;

// LOW: This shouldn't be a permanent home for these. Should be broken into their own files and moved to most closely related folder under Exceptions folder
namespace QuantumGate.GameObjects
{

    public class InvalidGameDataException : Exception
    {
        public IEnumerable<string> BadFiles { get; private set; }

        public InvalidGameDataException(IEnumerable<string> badFiles, string message = "", Exception innerException = null)
            : base(message, innerException)
        {
            BadFiles = badFiles;
        }
    }


    public class FailedToInitializeException : Exception
    {
        public FailedToInitializeException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {
        }
    }

}