//using MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
//using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
//using System.Threading.Tasks;

//namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
//{
//    public class ClientCreatedIntegrationEventHandler : IIntegrationEventHandler<ClientCreatedIntegrationEvent>
//    {
//        // private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;
//        // private readonly IBasketRepository _repository;

//        public ClientCreatedIntegrationEventHandler()
//            //ILogger<ProductPriceChangedIntegrationEventHandler> logger,
//            // IBasketRepository repository)
//        {
//            // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            // _repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        public async Task Handle(ClientCreatedIntegrationEvent @event)
//        {
//            //using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
//            //{
//            //    // _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

//            //    var userIds = _repository.GetUsers();

//            //    foreach (var id in userIds)
//            //    {
//            //        var basket = await _repository.GetBasketAsync(id);

//            //        await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
//            //    }
//            //}
//        }

//        //private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
//        //{
//        //    var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == productId).ToList();

//        //    if (itemsToUpdate != null)
//        //    {
//        //        _logger.LogInformation("----- ProductPriceChangedIntegrationEventHandler - Updating items in basket for user: {BuyerId} ({@Items})", basket.BuyerId, itemsToUpdate);

//        //        foreach (var item in itemsToUpdate)
//        //        {
//        //            if (item.UnitPrice == oldPrice)
//        //            {
//        //                var originalPrice = item.UnitPrice;
//        //                item.UnitPrice = newPrice;
//        //                item.OldUnitPrice = originalPrice;
//        //            }
//        //        }
//        //        await _repository.UpdateBasketAsync(basket);
//        //    }
//        //}
//    }
//}
