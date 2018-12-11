using AutoMapper;
using DataService;
using Messanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data {
    public class DataMapper {
        public static IMapper Initialize() {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<ConsumerContract, Consumer>();

                cfg.CreateMap<Consumer, ConsumerContract>();
            }).CreateMapper();
        }
    }
}