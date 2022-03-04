using AutoMapper;

namespace ExampleAPI.Models
{
    public class ExampleItemProfile: Profile
    {
        public ExampleItemProfile()
        {
            CreateMap<ExampleItemDTO, ExampleItem>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.IsCompleted,
                    opt => opt.MapFrom(src => src.IsCompleted)
                );
        }
    }
}
