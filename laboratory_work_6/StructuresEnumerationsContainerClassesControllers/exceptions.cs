using System;

public class GiftException : Exception
{
    public GiftException(string message) : base(message) { }

    public GiftException(string message, Exception innerException)
        : base(message, innerException) { }
}

public class InvalidProductDetailsException : GiftException
{
    public InvalidProductDetailsException(string message) : base(message) { }
}
public class ProductNotFoundException : GiftException
{
    public ProductNotFoundException(string message) : base(message) { }
}
public class IndexOutOfRangeGiftException : GiftException
{
    public IndexOutOfRangeGiftException(string message) : base(message) { }
}