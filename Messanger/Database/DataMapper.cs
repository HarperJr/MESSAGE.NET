using AutoMapper;
using Messanger.Database.Models;
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
                .ForMember(m => m.LastTimeOnline, opt => opt.MapFrom(c => new DateTime(c.LastTimeOnline)));

                cfg.CreateMap<Contact, Messanger.Data.Models.Contact>()
                .ForMember(m => m.InitTime, opt => opt.MapFrom(c => new DateTime(c.InitTime)));

                cfg.CreateMap<Dialog, Messanger.Data.Models.Dialog>()
                .ForMember(m => m.InitTime, opt => opt.MapFrom(c => new DateTime(c.InitTime)));

                cfg.CreateMap<DialogParticipant, Messanger.Data.Models.Participant>();

                cfg.CreateMap<Message, Messanger.Data.Models.Message>()
                .ForMember(m => m.Time, opt => opt.MapFrom(c => new DateTime(c.Time)));

                cfg.CreateMap<Multimedia, Messanger.Data.Models.Multimedia>();

                #endregion

                #region Contract to Model

                cfg.CreateMap<Messanger.Data.Models.Consumer, Consumer>()
                .ForMember(m => m.LastTimeOnline, opt => opt.MapFrom(c => c.LastTimeOnline.Ticks));

                cfg.CreateMap<Messanger.Data.Models.Contact, Contact>()
                .ForMember(m => m.InitTime, opt => opt.MapFrom(c => c.InitTime.Ticks)); ;

                cfg.CreateMap<Messanger.Data.Models.Dialog, Dialog>()
                .ForMember(m => m.InitTime, opt => opt.MapFrom(c => c.InitTime.Ticks)); ;

                cfg.CreateMap<Messanger.Data.Models.Participant, DialogParticipant>();

                cfg.CreateMap<Messanger.Data.Models.Message, Message>()
                .ForMember(m => m.Time, opt => opt.MapFrom(c => c.Time.Ticks));

                cfg.CreateMap<Messanger.Data.Models.Multimedia, Multimedia>();

                #endregion
            }).CreateMapper();
        }
    }
}