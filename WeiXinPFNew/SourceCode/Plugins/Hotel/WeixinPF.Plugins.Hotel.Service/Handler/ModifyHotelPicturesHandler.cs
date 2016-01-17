using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.Command.Hotel;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class ModifyHotelPicturesHandler :IHandleMessages<ModifyHotelPictureCommand>
    {
        private readonly IBus _bus;

        public ModifyHotelPicturesHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ModifyHotelPictureCommand message)
        {
            var operateResult = 0;

            if (message?.HotelPictures?.HotelPictureList != null && 
                message.HotelPictures.HotelPictureList.Any())
            {
                var service = new HotelPictureService(new HotelPictureRepository());
                var result = service.Deletepic(message.HotelId);

                if (result)
                {                     
                    foreach (var item in message.HotelPictures.HotelPictureList)
                    {
                        var hotelPicture = item.MapTo<HotelPictureInfo>();
                        service.Add(hotelPicture);
                    }

                    operateResult = 1;
                }
            }

            this._bus.Reply(operateResult);
        }
    }
}
