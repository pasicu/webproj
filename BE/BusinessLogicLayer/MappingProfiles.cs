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

            CreateMap<UpdateUser, UserModel>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());

            CreateMap<UserModel, SellerView>()
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicture == null ? null
                : $"data:image/jpg;base64,{Convert.ToBase64String(src.ProfilePicture)}"));
            
            CreateMap<NewProduct, Product>()
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => new Guid(src.SellerId)))
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()));

            CreateMap<Product, ProductView>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image == null ? null : $"data:image/jpg;base64,{Convert.ToBase64String(src.Image)}"));

            CreateMap<UpdateProduct, Product>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.ProductId)));

            CreateMap<NewOrder, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.OrderDeclinedd, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.OrderedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => new List<OrderProduct>()));

            CreateMap<Order, OrderView>()
               .ForMember(dest => dest.NumberOfProducts, opt => opt.MapFrom(src => 0))
               .ForMember(dest => dest.OrderCanceled, opt => opt.MapFrom(src => src.OrderDeclinedd));
        }
    }
}