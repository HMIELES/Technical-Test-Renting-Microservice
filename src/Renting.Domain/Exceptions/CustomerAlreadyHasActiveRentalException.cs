namespace Renting.Domain.Exceptions
{
    public sealed class CustomerAlreadyHasActiveRentalException : DomainException
    {
        public CustomerAlreadyHasActiveRentalException()
            : base("Customer already has an active rental.")
        {
        }
    }
}
