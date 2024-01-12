namespace SagaHost;

using Contracts;
using Wolverine;
using Wolverine.Persistence.Sagas;

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

    public void Handle(UpdateEmailMessage update)
    {
        Email = update.Email;
    }
}