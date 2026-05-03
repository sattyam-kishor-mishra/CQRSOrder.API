namespace OrdersAPI.Events
{
    public class InProcessEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InProcessEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            var tasks = handlers.Select(handler => handler.HandleAsync(@event));

            await Task.WhenAll(tasks);
        }
    }
}
