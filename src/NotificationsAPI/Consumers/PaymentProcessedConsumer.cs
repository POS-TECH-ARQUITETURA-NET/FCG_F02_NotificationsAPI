
using MassTransit;
using NotificationsAPI.Events;

namespace NotificationsAPI.Consumers {
    public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent> {
        public Task Consume(ConsumeContext<PaymentProcessedEvent> context) {
            var msg = context.Message;
            if (msg.Status == "Approved") {
                Console.WriteLine($"[Notifications] Compra confirmada! User: {msg.UserId}, Game: {msg.GameId}, Order: {msg.OrderId}");
            } else {
                Console.WriteLine($"[Notifications] Compra rejeitada. User: {msg.UserId}, Game: {msg.GameId}, Order: {msg.OrderId}");
            }
            return Task.CompletedTask;
        }
    }
}
