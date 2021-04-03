using AutoMapper;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<PartCar, Part>();

            CreateMap<PartCar, Car>();
        }
    }
}
