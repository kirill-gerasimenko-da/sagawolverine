namespace SagaHost;

using Wolverine;
using Wolverine.Persistence.Sagas;

public record StartSagaMessage([property: SagaIdentity] Guid Id, string Username);

public record UpdateEmailMessage([property: SagaIdentity] Guid Id, string Email);

public record Dependency;

public class SampleSaga : Saga
{
    [SagaIdentity]
    public Guid Id { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }

    public static SampleSaga Start(StartSagaMessage startMessage)
    {
        return new SampleSaga
        {
            Id = startMessage.Id,
            Username = startMessage.Username,
            Email = "no-email"
        };
    }

    public Dependency Before(UpdateEmailMessage update)
    {
        return new Dependency();
    }

    public void Handle(UpdateEmailMessage update, Dependency dependency)
    {
        Email = update.Email;
    }
}