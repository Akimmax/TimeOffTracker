using System;

namespace TOT.Business.Exceptions
{
    public class ApprovalsNotFoundException : Exception
    {
        public ApprovalsNotFoundException(string entryName)
            : base($"{entryName} has not found")
        {

        }
    }
}


