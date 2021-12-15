using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationsApi.Services
{
    public class ReservationService : IReservation
    {
        private ApiDbContext dbContext;
        public ReservationService()
        {
            dbContext = new ApiDbContext();
        }
        public async Task<List<Reservation>> GetReservations()
        {
            string connectionString = "Endpoint=sb://vehicletestdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=d1HezUv9AW1aImoFYrtn/dgAOwNXphvdYZQFmyXsNEA=";
            string queueName = "azureorderqueue";
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);
            if (receivedMessages == null)
            {
                return null;
            }

            foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
            {
                string body = receivedMessage.Body.ToString();
                var messageCreated = JsonConvert.DeserializeObject<Reservation>(body);
                await dbContext.Reservations.AddAsync(messageCreated);
                await dbContext.SaveChangesAsync();
                await receiver.CompleteMessageAsync(receivedMessage);
            }
            return await dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            var reservationResult = await dbContext.Reservations.FindAsync(id);
            if (reservationResult != null && reservationResult.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.live.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("pass your email address", "your password"),
                    EnableSsl = true,
                };
                smtpClient.Send("pass your email address", reservationResult.Email, "Vehicle Test Drive", "Your test drive is reserved");
                reservationResult.IsMailSent = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
