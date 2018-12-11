using AutoMapper;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database {
    public class DataMapper {
        public static IMapper Initialize() {
            return new MapperConfiguration(cfg => {
                #region Model to Contract

                cfg.CreateMap<Consumer, Messanger.Data.Models.Consumer>()
                .ForMember(m => m.AvatarId, opt => opt.MapFrom(c => c.Avatar.Id));

                cfg.CreateMap<Contact, Messanger.Data.Models.Contact>()
                .ForMember(m => m.ContactId, opt => opt.MapFrom(c => c.RelatedConsumer.Id));

                cfg.CreateMap<Dialog, Messanger.Data.Models.Dialog>()
                .ForMember(m => m.OwnerId, opt => opt.MapFrom(c => c.Owner.Id));

                cfg.CreateMap<DialogParticipant, Messanger.Data.Models.Participant>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.ParticipantId, opt => opt.MapFrom(c => c.Participant.Id))
                .ForMember(m => m.InvitorId, opt => opt.MapFrom(c => c.Invitor.Id));

                cfg.CreateMap<Message, Messanger.Data.Models.Message>()
                .ForMember(m => m.DialogId, opt => opt.MapFrom(c => c.Dialog.Id))
                .ForMember(m => m.SenderId, opt => opt.MapFrom(c => c.Sender.Id));

                cfg.CreateMap<Multimedia, Messanger.Data.Models.Multimedia>();

                #endregion

                #region Contract to Model

                cfg.CreateMap<Messanger.Data.Models.Consumer, Consumer>();
                
                cfg.CreateMap<Messanger.Data.Models.Contact, Contact>();

                cfg.CreateMap<Messanger.Data.Models.Dialog, Dialog>();

                cfg.CreateMap<Messanger.Data.Models.Participant, DialogParticipant>();

                cfg.CreateMap<Messanger.Data.Models.Message, Message>();

                cfg.CreateMap<Messanger.Data.Models.Multimedia, Multimedia>();

                #endregion
            }).CreateMapper();
        }
    }
}