namespace Constech.API.Shared.Domain.Services.Communication;

public abstract class BaseResponse<T>
{
    protected BaseResponse(string message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }
    protected BaseResponse(T resource)
    {
        Success = true;
        Message = string.Empty;
        Resource = resource;
    }

    private bool Success { get; set; }
    private string Message { get; set; }
    private T Resource { get; set; }
}