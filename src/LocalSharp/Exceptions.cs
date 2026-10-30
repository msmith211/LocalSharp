using System.Net;

namespace LocalSharp;

/// <summary>
/// Base type for every exception LocalSharp throws. Catch this to handle all library failures.
/// </summary>
public class LocalSharpException : Exception
{
    /// <summary>Creates an exception with no message.</summary>
    public LocalSharpException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public LocalSharpException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public LocalSharpException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// The server answered with a non-success HTTP status code.
/// </summary>
public class LocalSharpHttpException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public LocalSharpHttpException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public LocalSharpHttpException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public LocalSharpHttpException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>Creates an exception describing a failed HTTP response.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="statusCode">The HTTP status code the server returned.</param>
    /// <param name="serverMessage">The error message extracted from the response body, if any.</param>
    /// <param name="responseBody">The raw response body.</param>
    public LocalSharpHttpException(string message, HttpStatusCode statusCode, string? serverMessage, string? responseBody)
        : base(message)
    {
        StatusCode = statusCode;
        ServerMessage = serverMessage;
        ResponseBody = responseBody;
    }

    /// <summary>The HTTP status code the server returned.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>The error message from the response body, when the server sent one in a recognized shape.</summary>
    public string? ServerMessage { get; }

    /// <summary>The raw response body, useful for diagnosing backend-specific errors.</summary>
    public string? ResponseBody { get; }

    /// <summary><see langword="true"/> for 5xx responses, which usually mean the server failed rather than the request.</summary>
    public bool IsServerError => (int)StatusCode >= 500;
}

/// <summary>
/// The server could not be reached, for example because it is not running.
/// </summary>
public class LocalSharpConnectionException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public LocalSharpConnectionException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public LocalSharpConnectionException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public LocalSharpConnectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// A request took longer than <see cref="LocalSharpOptions.RequestTimeout"/>. Cancellation through the
/// caller's token throws <see cref="OperationCanceledException"/> instead.
/// </summary>
public class LocalSharpTimeoutException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public LocalSharpTimeoutException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public LocalSharpTimeoutException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public LocalSharpTimeoutException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// With <see cref="LocalSharpOptions.StrictModelMatching"/> on, the server's reply reported a different model
/// than the one requested, or reported none.
/// </summary>
public class ModelMismatchException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public ModelMismatchException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public ModelMismatchException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public ModelMismatchException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>Creates an exception describing a model mismatch.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="requestedModel">The model LocalSharp asked for.</param>
    /// <param name="reportedModel">The model the reply reported, or <see langword="null"/> if it reported none.</param>
    public ModelMismatchException(string message, string requestedModel, string? reportedModel)
        : base(message)
    {
        RequestedModel = requestedModel;
        ReportedModel = reportedModel;
    }

    /// <summary>The model LocalSharp asked for.</summary>
    public string? RequestedModel { get; }

    /// <summary>The model the reply reported, or <see langword="null"/> if it reported none.</summary>
    public string? ReportedModel { get; }
}

/// <summary>
/// No model was named on the request or in <see cref="LocalSharpOptions.DefaultModel"/>, and the server lists no models.
/// </summary>
public class NoModelAvailableException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public NoModelAvailableException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public NoModelAvailableException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public NoModelAvailableException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// <see cref="LocalSharpClient.GetAsync{T}(string, CancellationToken)"/> got no reply it could read as the
/// requested type, after every attempt. <see cref="Attempts"/> holds each reply and why it failed.
/// </summary>
public class StructuredOutputException : LocalSharpException
{
    /// <summary>Creates an exception with no message.</summary>
    public StructuredOutputException()
    {
    }

    /// <summary>Creates an exception with a message.</summary>
    /// <param name="message">What went wrong.</param>
    public StructuredOutputException(string message)
        : base(message)
    {
    }

    /// <summary>Creates an exception with a message and the exception that caused it.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="innerException">The underlying failure.</param>
    public StructuredOutputException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>Creates an exception describing failed attempts.</summary>
    /// <param name="message">What went wrong.</param>
    /// <param name="attempts">Each attempt's reply and error, in order.</param>
    /// <param name="innerException">The last attempt's parse error.</param>
    public StructuredOutputException(string message, IReadOnlyList<StructuredOutputAttempt> attempts, Exception innerException)
        : base(message, innerException)
    {
        ArgumentNullException.ThrowIfNull(attempts);
        Attempts = attempts;
    }

    /// <summary>Each attempt's raw reply and parse error, in the order they were made.</summary>
    public IReadOnlyList<StructuredOutputAttempt> Attempts { get; } = [];

    /// <summary>The raw text of the last attempt's reply, or <see langword="null"/> if no attempt was recorded.</summary>
    public string? RawText => Attempts.Count == 0 ? null : Attempts[^1].RawText;
}

/// <summary>One <see cref="LocalSharpClient.GetAsync{T}(string, CancellationToken)"/> attempt that failed to parse.</summary>
/// <param name="RawText">The reply text exactly as the server sent it, before any cleanup.</param>
/// <param name="Error">Why the reply couldn't be read as the requested type.</param>
public sealed record StructuredOutputAttempt(string RawText, string Error);
