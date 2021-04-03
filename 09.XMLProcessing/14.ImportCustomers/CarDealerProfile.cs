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

            CreateMap<ImportPartIdDto, Part>();

            CreateMap<ImportCarDto, Car>()
                .ForMember(x => x.TravelledDistance, x => x.MapFrom(x => x.TraveledDistance));

            CreateMap<ImportCustomerDto, Customer>();
        }
    }
}
