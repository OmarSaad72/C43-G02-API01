namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"The Basket With Id {id} Isn't Found")
        {
            
        }
    }
}
