﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelPictureRequest
    {
        public int HotelId { get; set; }
        public int Wid { get; set; }
        public string Condition { get; set; }
    }
}
