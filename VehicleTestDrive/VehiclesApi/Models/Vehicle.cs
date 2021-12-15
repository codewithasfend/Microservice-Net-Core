namespace VehiclesApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Displacement { get; set; }
        public string MaxSpeed { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
