namespace Renting.Domain.Exceptions
{
    public sealed class VehicleNotRentedException : DomainException
    {
        public VehicleNotRentedException()
            : base("Vehicle is not currently rented.")
        {
        }
    }
}
