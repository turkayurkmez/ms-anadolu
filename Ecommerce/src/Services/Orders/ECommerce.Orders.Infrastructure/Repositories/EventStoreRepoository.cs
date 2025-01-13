using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ECommerce.Orders.Infrastructure.Repositories
{
    public class EventStoreRepoository 
    {
        private EventStoreClient _client;

        public EventStoreRepoository(EventStoreClient client)
        {
            _client = client;
        }

        public async Task AppendEventAsync<T>(string streamName, T @event) where T : class
        {
            var eventData = new EventData(
                Uuid.NewUuid(),
                typeof(T).Name,
                JsonSerializer.SerializeToUtf8Bytes(@event)
            );
            await _client.AppendToStreamAsync(streamName, StreamState.Any, new[] { eventData });
        }

        public async Task<IEnumerable<T>> GetEventsAsync<T>(string streamName) where T : class
        {
            //var result = new List<T>();
            var read = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

            var events = new List<T>();

            await foreach (var @event in read)
            {
                var eventData = JsonSerializer.Deserialize<T>(@event.Event.Data.Span);
                events.Add(eventData);
            }
            return events;
        }
    }
}
