
//using EventBus.Base.Standard;
//using Identity_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
//using MachineLearning_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using System.Collections.Generic;

//public static class EventBusExtension
//{
//    public static IEnumerable<IIntegrationEventHandler> GetHandlers()
//    {
//        return new List<IIntegrationEventHandler>
//        {
//            new ItemCreatedIntegrationEventHandler()
//        };
//    }

//    public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
//    {
//        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

//        eventBus.Subscribe<ItemCreatedIntegrationEvent, ItemCreatedIntegrationEventHandler>();

//        return app;
//    }
//}