using System;

namespace Renting.Domain.Exceptions
{
    public class RentalAlreadyReturnedException : Exception
    {
        public RentalAlreadyReturnedException()
            : base("The rental has already been returned.")
        {
        }
    }
}


