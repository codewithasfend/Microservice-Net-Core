using System.ComponentModel.DataAnnotations;

namespace ReservationsApi.Models
{
    public class Vehicle
    {
        [Key]
        public int VId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
