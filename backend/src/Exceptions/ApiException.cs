using System.Net;

namespace ContactListAPI.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public ApiException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public ApiException(string message, HttpStatusCode statusCode, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
