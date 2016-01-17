using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Messages.Command
{
    public class ModifyHotelInfoCommand
    {
        public GetHotelResponse HotelInfo { get; set; }
    }
}
