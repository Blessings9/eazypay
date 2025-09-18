namespace EazyPay.Application.Shared.Dtos;

public class BaseResponse<T>
{
    public required string RequestId { get; set; }
    public required DateTime Timestamp { get; set; }
    public required int Code { get; set; }
    public required string Message { get; set; }
    public List<object> Errors { get; set; } = [];
    public T? Data { get; set; }
}