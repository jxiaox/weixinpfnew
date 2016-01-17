using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Messages.Command.Hotel
{
    public class ModifyHotelPictureCommand
    {
        public int HotelId { get; set; }

        public GetHotelPictureListResponse HotelPictures { get; set; }
    }
}
