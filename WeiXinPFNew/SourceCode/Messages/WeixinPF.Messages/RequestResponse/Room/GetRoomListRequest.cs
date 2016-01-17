using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetRoomListRequest
    {
        public int Wid { get; set; }
        public string Openid { get; set; }
        public int HotelId { get; set; }
    }
}
