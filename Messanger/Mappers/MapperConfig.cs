using DataService;
using Messanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Mappers {
    public class MapperConfig {

        public static AutoMapper.IMapper Initialize() {
            return new AutoMapper.MapperConfiguration(cfg => {

                cfg.CreateMap<Consumer, ConsumerContract>();

                cfg.CreateMap<ConsumerContract, Consumer>();
            }).CreateMapper();
        }
    }
}