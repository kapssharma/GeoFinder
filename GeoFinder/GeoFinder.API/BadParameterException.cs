namespace GeoFinder.API
{
    public class BadParameterException : Exception
    {
         public BadParameterException(string? message) : base(message) { }
         public BadParameterException(string? message, Exception innerException) : base(message, innerException) { }
         public BadParameterException() : base() { }
    }
}
