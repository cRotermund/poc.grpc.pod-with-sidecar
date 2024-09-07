using Grpc.Core;

namespace Runtime.Host.HostImpl;

using GenericServices.Proto;

public class WorkDispatchChannelService : WorkDispatchChannel.WorkDispatchChannelBase
{
    private readonly ILogger _logger;
    
    public WorkDispatchChannelService(ILogger<WorkDispatchChannelService> logger)
    {
        _logger = logger;
    }

    public override async Task Connect(IAsyncStreamReader<WorkOutput> requestStream, IServerStreamWriter<WorkInput> responseStream, ServerCallContext context)
    {
        try
        {
            int i = 0;
            while (true)
            {
                string message = $"Message # {i++}";

                _logger.LogInformation($"Sending: {message}");

                //Send a message
                responseStream.WriteAsync(new WorkInput
                {
                    Message = message
                });

                //Wait for and read the response
                if (await requestStream.MoveNext())
                {
                    var output = requestStream.Current;
                    _logger.LogInformation($"Received: {output.Message}");
                }
                
                //Wait for a bit
                Thread.Sleep(1000);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception thrown.");
        }
    }
}