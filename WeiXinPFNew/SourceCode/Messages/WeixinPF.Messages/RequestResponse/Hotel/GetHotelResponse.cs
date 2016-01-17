using System;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CoverSrc { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Introduction { get; set; }

        public int? Wid { get; set; }
        public string hotelPhone { get; set; }
        public string noticeEmail { get; set; }
        public string emailPws { get; set; }
        public string smtp { get; set; }

        public string topPic { get; set; }
        public int? orderLimit { get; set; }
        public bool listMode { get; set; }
        public int? messageNotice { get; set; }
        public string pwd { get; set; }
        public string orderRemark { get; set; }
        public DateTime? createDate { get; set; }
        public int? sortid { get; set; }
        public decimal? xplace { get; set; }
        public decimal? yplace { get; set; }
        public string Operator { get; set; }
        public string HotelLevel { get; set; }
        public bool? Recommend { get; set; }

    }
}
