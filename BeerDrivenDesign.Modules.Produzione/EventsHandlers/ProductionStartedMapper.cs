using System.Text;
using BeerDrivenDesign.Modules.Produzione.Events;
using Muflone.Messages;
using Muflone.Messages.Enums;
using Newtonsoft.Json;

namespace BeerDrivenDesign.Modules.Produzione.EventsHandlers;

public class ProductionStartedMapper : MessageMapper<ProductionStarted>
{
    public override Message MapToMessage(ProductionStarted request)
    {
        var message = new Message(new MessageHeader(request.MessageId, string.Empty, MessageType.MtNone), new MessageBody(JsonConvert.SerializeObject((object)request)));
        return base.MapToMessage(request);
    }

    public override ProductionStarted MapToRequest(Message message)
    {
        var request = JsonConvert.DeserializeObject<ProductionStarted>(Encoding.UTF8.GetString(message.Body.Bytes));
        return base.MapToRequest(message);
    }
}