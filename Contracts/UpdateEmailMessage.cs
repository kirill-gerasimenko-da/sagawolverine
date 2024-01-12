namespace Contracts;

using Wolverine.Persistence.Sagas;

public record UpdateEmailMessage([property: SagaIdentity] Guid Id, string Email);