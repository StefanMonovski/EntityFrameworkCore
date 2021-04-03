using AutoMapper;
using CarDealer.DataTransferObjects;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<PartCar, Part>();

            CreateMap<PartCar, Car>();

            CreateMap<ImportSupplierDto, Supplier>();

            CreateMap<ImportPartDto, Part>();
        }
    }
}
