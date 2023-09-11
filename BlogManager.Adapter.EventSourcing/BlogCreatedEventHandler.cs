using BlogManager.Core.Events;
using MediatR;

namespace BlogManager.Adapter.EventSourcing;

public class BlogCreatedEventHandler : IRequestHandler<BlogCreatedEvent>
{
    // You can inject any necessary dependencies here, such as repositories or services.
    

    public async Task Handle(BlogCreatedEvent @event, CancellationToken cancellationToken)
    {
        // Handle the BlogCreatedEvent here.
        // You can perform actions in response to the event, such as updating read models or sending notifications.

        Console.WriteLine($"Blog created - BlogId: {@event.Id}, Title: {@event.Title}, AuthorId: {@event.AuthorId}");

        // You can perform additional logic, update read models, or trigger other actions as needed.
        // For example, you might update a query model to reflect the newly created blog.

        // If you're using a message broker like RabbitMQ or Kafka, you can also publish this event for further processing by other components.
    }
}