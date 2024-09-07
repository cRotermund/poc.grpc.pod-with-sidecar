// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GenericServices.Proto;

//TODO: how to exchange the address??
var channel = GrpcChannel.ForAddress("http://localhost:8888");
var client = new WorkDispatchChannel.WorkDispatchChannelClient(channel);

var cts = new CancellationTokenSource();
var session = client.Connect();

//Loop session messages
try
{
    //Listen loop
    while (true)
    {
        //Receive message and echo
        if (await session.ResponseStream.MoveNext(cts.Token))
        {
            var input = session.ResponseStream.Current;
            
            Console.WriteLine($"Received: {input.Message}");
            
            await session.RequestStream.WriteAsync(new WorkOutput
            {
                Message = input.Message
            });
            
            Console.WriteLine($"Sent: {input.Message}");
        }
        else
        {
            break;
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"We blew up: {e}");
}

