using AutoMapper;
using DataService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Data {
    public class DataMapper {
        public static void Configure() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Consumer, ConsumerContract>()
                .ForMember(m => m.AvatarId, opt => opt.MapFrom(c => c.Avatar.Id));

                cfg.CreateMap<Contact, ContactContract>()
                .ForMember(m => m.ContactId, opt => opt.MapFrom(c => c.RelatedConsumer.Id));

                cfg.CreateMap<Dialog, DialogContract>()
                .ForMember(m => m.OwnerId, opt => opt.MapFrom(c => c.Owner.Id));

                cfg.CreateMap<DialogParticipant, ParticipantContract>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.InvitorId, opt => opt.MapFrom(c => c.Invitor.Id));

                cfg.CreateMap<Message, MessageContract>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.SenderId, opt => opt.MapFrom(c => c.Sender.Id));

                cfg.CreateMap<Multimedia, MultimediaContract>();

                cfg.CreateMap<ConsumerContract, Consumer>();

                cfg.CreateMap<Contact, ContactContract>();
            });
        }
    }
}