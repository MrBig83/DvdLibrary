namespace DvdLibrary.Application.Common.Models;

public record ErrorResponse(string Message, IReadOnlyList<string>? Errors = null);
