namespace Renting.Domain.Exceptions
{
    public sealed class VehicleTooOldException : DomainException
    {
        public VehicleTooOldException()
            : base("Vehicles older than 5 years cannot be registered.")
        {
        }
    }
}

