namespace DispatchR.Contracts;

/// <summary>
/// Indicates request to the handler
/// </summary>
public interface IRequest
{

}

/// <summary>
/// Indicates request to the handler
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IRequest<TResult> : IRequest
{

}
