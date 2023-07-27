using AutoMapper;
using Microservices.Shared.Models;

namespace Microservices.Reporter
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Report, ReportDto>().ReverseMap();

        }
    }
}
