using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models.Database;
using Common.Models.Enumerations;
using Common.Models.Inbound;
using Common.Models.Outbound;

namespace BusinessLogicLayer
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<RegisterUser, UserModel>()
                    .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => Enum.Parse(typeof(UserType), src.UserType)))
                    .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                    .ForMember(dest => dest.Verified, opt => opt.MapFrom(src => 0));

            CreateMap<UserModel, LogedInUser>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Verified, opt => opt.MapFrom(src => src.Verified == 1 ? "Your account is verified."
                        : (src.Verified == null ? "Verification request is denied."
                        : "Your account is not verified.")));
        }
    }
}
