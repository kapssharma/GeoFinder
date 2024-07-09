namespace GeoFinder.API
{
    public class BadParameterException : Exception
    {
        // For exceptiom message
         public BadParameterException(string? message) : base(message) { }
         public BadParameterException(string? message, Exception innerException) : base(message, innerException) { }
         public BadParameterException() : base() { } 
    }
}
