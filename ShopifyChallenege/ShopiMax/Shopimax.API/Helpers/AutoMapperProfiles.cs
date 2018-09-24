using AutoMapper;
using Shopimax.API.Dtos;
using Shopimax.API.Models;

namespace Shopimax.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Order, OrderDisplayDto>()
            .ForMember(dest => dest.OrderTotal, opt => {
                    opt.ResolveUsing(d => d.LineItems.CalculateOrderTotal());
                });

            CreateMap<LineItem, LineItemDisplayDto>()
            .ForMember(dest => dest.LineItemTotalPrice, opt => {
                    opt.ResolveUsing(d => d.LineItemTotalPrice.ToString().AppendDollar());
                });

             CreateMap<Product, ProductDisplayDto>()
            .ForMember(dest => dest.ProductRate, opt => {
                    opt.ResolveUsing(d => d.ProductRate.ToString().AppendDollar());
                });
           
   
            
        }
        
        
    }
} 