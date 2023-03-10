using AutoMapper;
using System;
using Zip.InstallmentsService.DTOs;
using Zip.nstallmentsService.Repo.Entities;

namespace Zip.InstallmentsService.BusinessLogic.Mapper
{
    public class InstallmentProfile : Profile
    {
        public InstallmentProfile()
        {
            CreateMap<InstallmentDto, Installment>()
                .ForMember(
                    dest => dest.CreatedTimeStamp,
                    opt => opt.MapFrom(src => DateTime.Now)
                ).ForMember(
                    dest => dest.ModifiedTimeStamp,
                    opt => opt.MapFrom(src => DateTime.Now)
                );
        }
    }
}
