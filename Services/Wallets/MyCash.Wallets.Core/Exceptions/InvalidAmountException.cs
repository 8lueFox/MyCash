using MyCash.Micro.Exceptions;

namespace MyCash.Wallets.Core.Exceptions;

internal class InvalidAmountException : CustomException
{
    public decimal Count { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }

    public InvalidAmountException(decimal count, decimal price, string currency)
        : base($"Cannot create transaction with count: {count} price: {price} currency: {currency}.")
        => (Count, Price, Currency) = (count, price, currency);
}