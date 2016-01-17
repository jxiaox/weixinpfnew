using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.Command;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class ModifyHotelInfoHandler : IHandleMessages<ModifyHotelInfoCommand>
    {
        private readonly IBus _bus;

        public ModifyHotelInfoHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(ModifyHotelInfoCommand message)
        {
            var result = 0;
            var hotelInfo = message?.HotelInfo?.MapTo<HotelInfo>();

            if (hotelInfo != null)
            {
                try
                {
                    result = int.Parse(new Application.Service.HotelService().Update(hotelInfo).ToString());
                }
                catch (Exception)
                {
                    // 操作日志
                }
            }

            this._bus.Reply(result);
        }
    }
}
