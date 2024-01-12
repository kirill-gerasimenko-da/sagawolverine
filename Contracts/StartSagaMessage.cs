namespace Contracts;

using Wolverine.Persistence.Sagas;

public record StartSagaMessage([property: SagaIdentity] Guid Id, string Username);