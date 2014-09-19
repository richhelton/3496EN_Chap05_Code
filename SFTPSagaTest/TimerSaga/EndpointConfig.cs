
namespace TimerSaga
{
    using System;
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, IWantToRunWhenBusStartsAndStops
    {


        public void Init()
        {
            Configure.With()
                .DefaultBuilder()  // Autofac Default Container
                .UseTransport<Msmq>()  // MSMQ, will create Queues, Defualt
                .MsmqSubscriptionStorage() // Create a subscription endpoint
                .UnicastBus(); // Create the default unicast Bus
        }

        public void Start()
        {
            Console.WriteLine("This is the process hosting the saga.");
        }

        public void Stop()
        {
            Console.WriteLine("Stopped.");
        }
    }
}
