using Microsoft.EntityFrameworkCore.Query.Internal;

namespace SalesWebMvc1.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message): base(message)
        {

        }
     }
}
