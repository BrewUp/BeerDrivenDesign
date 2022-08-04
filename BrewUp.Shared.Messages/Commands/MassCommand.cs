using System.Text.Json.Serialization;
using BrewUp.Shared.Messages.CustomTypes;
using Muflone.Core;
using Muflone.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Shared.Messages.Commands;

public abstract class MassCommand : ICommand
{
    public DomainId AggregateId { get; set; }
    public Guid MessageId { get; set; }
    public Dictionary<string, object> UserProperties { get; set; }
    public Account Who { get; }
    public When When { get; }

    [JsonConstructor]
    protected MassCommand()
    {
        MessageId = Guid.NewGuid();
        UserProperties = new Dictionary<string, object>();
        Who = new Account(Guid.NewGuid().ToString(), "Anonymous");
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId, Guid correlationId, string who = "anonymous")
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = Guid.NewGuid();
        UserProperties = new Dictionary<string, object>
        {
            { "CorrelationId", correlationId }
        };
        Who = new Account(Guid.NewGuid().ToString(), who);
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = Guid.NewGuid();
        UserProperties = new Dictionary<string, object>();
        Who = new Account(Guid.NewGuid().ToString(), "Anonymous");
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId, Guid commitId)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = commitId;
        UserProperties = new Dictionary<string, object>();
        Who = new Account(Guid.NewGuid().ToString(), "Anonymous");
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId, Account who)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = Guid.NewGuid();
        UserProperties = new Dictionary<string, object>();
        Who = who;
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId, Account who, When when)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = Guid.NewGuid();
        UserProperties = new Dictionary<string, object>();
        Who = who;
        When = when;
    }

    protected MassCommand(Guid aggregateId, Guid commitId, Account who)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = commitId;
        UserProperties = new Dictionary<string, object>();
        Who = who;
        When = new When(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    protected MassCommand(Guid aggregateId, Guid commitId, Account who, When when)
    {
        AggregateId = new BeerId(aggregateId);
        MessageId = commitId;
        UserProperties = new Dictionary<string, object>();
        Who = who;
        When = when;
    }
}