using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomersApi.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext dbContext;
        public CustomerService()
        {
            dbContext = new ApiDbContext();
        }
        public async Task AddCustomer(Customer customer)
        {
            var vehicleInDb = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == customer.VehicleId);
            if (vehicleInDb == null)
            {
                await dbContext.Vehicles.AddAsync(customer.Vehicle);
                await dbContext.SaveChangesAsync();
            }
            customer.Vehicle = null;
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            string connectionString = "Endpoint=sb://vehicletestdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=d1HezUv9AW1aImoFYrtn/dgAOwNXphvdYZQFmyXsNEA=";
            string queueName = "azureorderqueue";
            await using var client = new ServiceBusClient(connectionString);
            var objectAsText = JsonConvert.SerializeObject(customer);

            ServiceBusSender sender = client.CreateSender(queueName);

            ServiceBusMessage message = new ServiceBusMessage(objectAsText);

            await sender.SendMessageAsync(message);


        }
    }
}
