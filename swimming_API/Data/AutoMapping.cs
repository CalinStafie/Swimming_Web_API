using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using proiect_final_API.Entities;
using proiect_final_API.Models.DTO_s;

namespace proiect_final_API.Data
{
    public class AutoMapping : Profile
    {
        public static Dictionary<Type, Type> mappings = new Dictionary<Type, Type>()
        {
            { typeof(Purchase), typeof(PurchaseDTO) },
            { typeof(Receptionist), typeof(ReceptionistDTO) },
            { typeof(Client), typeof(ClientDTO) },
            { typeof(Subscription), typeof(SubscriptionDTO) },
            { typeof(VacationDay), typeof(VacationDayDTO) },
            { typeof(NormalWorkDay), typeof(NormalWorkDayDTO) }
        };

        public AutoMapping()
        {
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Receptionist, ReceptionistDTO>().ReverseMap();
            CreateMap<Receptionist, ReceptionistInformationForUsersDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
            CreateMap<VacationDay, VacationDayDTO>().ReverseMap();
            CreateMap<NormalWorkDay, NormalWorkDayDTO>().ReverseMap();
        }
    }
}
