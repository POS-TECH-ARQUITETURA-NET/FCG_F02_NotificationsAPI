
using MassTransit;
using NotificationsAPI.Events;

namespace NotificationsAPI.Consumers {
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent> {
        public Task Consume(ConsumeContext<UserCreatedEvent> context) {
            var msg = context.Message;
            Console.WriteLine($"[Notifications] Bem-vindo, {msg.Email}! (UserId: {msg.UserId})");
            return Task.CompletedTask;
        }
    }
}
