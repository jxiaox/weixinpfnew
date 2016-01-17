using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelPictureResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id
        {
            set; get; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int hotelid
        {
            set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string title
        {
            set; get; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int sortid
        {
            set; get; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string picUrl
        {
            set; get; }
        /// <summary>
        /// 图片跳转地址
        /// </summary>
        public string picTiaozhuan
        {
            set; get; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime createDate
        {
            set; get; }
    }
}
