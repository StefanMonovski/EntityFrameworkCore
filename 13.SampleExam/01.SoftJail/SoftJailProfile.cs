namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            CreateMap<ImportDepartmentDto, Department>();

            CreateMap<ImportCellDto, Cell>();

            CreateMap<ImportPrisonerDto, Prisoner>();

            CreateMap<ImportMailDto, Mail>();

            CreateMap<ImportOfficerDto, Officer>()
                .ForMember(x => x.FullName, x => x.MapFrom(x => x.Name))
                .ForMember(x => x.Salary, x => x.MapFrom(x => x.Money));

            CreateMap<ImportPrisonerDto, Prisoner>();
        }
    }
}
