using AutoMapper;
using DataService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Data {
    public class DataMapper {
        public static IMapper Initialize() {
            return new MapperConfiguration(cfg => {
                #region Model to Contract

                cfg.CreateMap<Consumer, ConsumerContract>()
                .ForMember(m => m.AvatarId, opt => opt.MapFrom(c => c.Avatar.Id));

                cfg.CreateMap<Contact, ContactContract>()
                .ForMember(m => m.ContactId, opt => opt.MapFrom(c => c.RelatedConsumer.Id));

                cfg.CreateMap<Dialog, DialogContract>()
                .ForMember(m => m.OwnerId, opt => opt.MapFrom(c => c.Owner.Id));

                cfg.CreateMap<DialogParticipant, ParticipantContract>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.ParticipantId, opt => opt.MapFrom(c => c.Participant.Id))
                .ForMember(m => m.InvitorId, opt => opt.MapFrom(c => c.Invitor.Id));

                cfg.CreateMap<Message, MessageContract>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.SenderId, opt => opt.MapFrom(c => c.Sender.Id));

                cfg.CreateMap<Multimedia, MultimediaContract>();

                #endregion

                #region Contract to Model

                cfg.CreateMap<ConsumerContract, Consumer>();

                cfg.CreateMap<ContactContract, Contact>();

                cfg.CreateMap<DialogContract, Dialog>();

                cfg.CreateMap<ParticipantContract, DialogParticipant>();

                cfg.CreateMap<MessageContract, Message>();

                cfg.CreateMap<MultimediaContract, Multimedia>();

                #endregion
            }).CreateMapper();
        }
    }
}