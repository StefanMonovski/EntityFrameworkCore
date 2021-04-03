using AutoMapper;
using CarDealer.DataTransferObjects.Imports;
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

            CreateMap<ImportSaleDto, Sale>();
        }
    }
}
