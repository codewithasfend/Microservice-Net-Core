using Microsoft.AspNetCore.Mvc;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private IReservation _reservationService;
        public ReservationsController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/<ReservationsController>
        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            var reservations = await _reservationService.GetReservations();
            return reservations;
        }

        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _reservationService.UpdateMailStatus(id);
        }
    }
}
