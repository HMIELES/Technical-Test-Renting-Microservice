namespace Renting.Domain.Exceptions
{
    public sealed class VehicleAlreadyRentedException : DomainException
    {
        public VehicleAlreadyRentedException()
            : base("Vehicle is already rented.")
        {
        }
    }
}
