using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.Command.Hotel;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Web.UI;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class hotel_info : ManagePage
    {
        TextBox title;
        TextBox sortid;
        TextBox picUrl;
        TextBox picTiaozhuan;
        protected string editetype = "";
        protected static int hotelid = 0;
        private string action;

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid", GetHotelId());
            action = MyCommFun.QueryString("action");
            if (!IsPostBack)
            {
                SetLocation();
                ShowInfo(hotelid);
                if (action == MXEnums.ActionEnum.View.ToString())
                {
                    this.save_hotel.Visible = false;
                }
            }
        }

        public void ShowInfo(int hotelid)
        {
            var hotel = Global.Bus.Send<GetHotelResponse>(Constants.HotelServiceAddress, new GetHotelRequest()
            {
                HotelId = hotelid
            });

            if (!hotel.IsSuccess || hotel.Data == null)
            {
                return;
            }

            
                this.lblHotelCode.Text = hotel.Data.Code;
                this.lblHotelName.Text = hotel.Data.Name;
                this.lblOperator.Text = hotel.Data.Operator;
                this.hotelAddress.Text = hotel.Data.Address;
                this.lblHotelPhone.Text = hotel.Data.hotelPhone;
                this.mobilPhone.Text = hotel.Data.Tel;
                this.coverPic.Text = hotel.Data.CoverSrc;
                this.topPic.Text = hotel.Data.topPic;
                this.hotelIntroduct.Value = hotel.Data.Introduction;
                this.txtLatXPoint.Text = hotel.Data.xplace.ToString();
                this.txtLngYPoint.Text = hotel.Data.yplace.ToString();
                if (hotel.Data.xplace.HasValue && hotel.Data.yplace.HasValue)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message",
                        "<script language='javascript'> $(\"#baiduframe\").attr(\"src\", \"../../weixin/map/qqmap/qqmap_getLocation.html?lng=" + hotel.yplace.Value.ToString() + "&lat=" + hotel.xplace.Value.ToString() + "\");</script>");
                }

            var hotelPictures = Global.Bus.Send<GetHotelPictureListResponse>(Constants.HotelServiceAddress,
                new GetHotelPictureRequest()
                {
                    Condition = "hotelid=" + hotelid + " order by id asc"
                });

            if (!hotelPictures.IsSuccess || 
                hotelPictures.Data?.HotelPictureList == null || 
                !hotelPictures.Data.HotelPictureList.Any())
            {
                return;
            }

                for (var i = 1; i <= hotelPictures.Data.HotelPictureList.Count; i++)
                {
                    var itemEntity = hotelPictures.Data.HotelPictureList[(i - 1)];
                    title = this.FindControl("title" + i) as TextBox;
                    sortid = this.FindControl("sortid" + i) as TextBox;
                    picUrl = this.FindControl("picUrl" + i) as TextBox;
                    picTiaozhuan = this.FindControl("picTiaozhuan" + i) as TextBox;

                    if (title ==null || sortid == null || picUrl ==null || picTiaozhuan ==null)
                    {
                        continue;
                    }

                    title.Text = itemEntity.title;
                    sortid.Text = itemEntity.sortid.ToString();
                    picUrl.Text = itemEntity.picUrl;
                    picTiaozhuan.Text = itemEntity.picTiaozhuan;
                }
            
        }

        protected void save_hotel_Click(object sender, EventArgs e)
        {
            var hotel = Global.Bus.Send<GetHotelResponse>(Constants.HotelServiceAddress, new GetHotelRequest()
            {
                HotelId = hotelid
            });

            if (!hotel.IsSuccess || hotel.Data == null)
            {
                return;
            }

            //hotel.hotelName = this.hotelName.Text;
            hotel.Data.Address = this.hotelAddress.Text;
            //hotel.hotelPhone = this.hotelPhone.Text;
            hotel.Data.Tel = this.mobilPhone.Text;
            //hotel.noticeEmail = this.noticeEmail.Text;
            hotel.Data.CoverSrc = this.coverPic.Text;
            hotel.Data.topPic = this.topPic.Text;
            //hotel.orderLimit = MyCommFun.Str2Int(this.orderLimit.Text);
            //hotel.listMode = Convert.ToBoolean(this.listMode.SelectedValue);
            //hotel.messageNotice = MyCommFun.Str2Int(this.messageNotice.Text);
            //hotel.pwd = this.pwd.Text;
            hotel.Data.Introduction = this.hotelIntroduct.Value;
            //hotel.orderRemark = this.orderRemark.Value;
            hotel.Data.xplace = MyCommFun.Str2Decimal(this.txtLatXPoint.Text);
            hotel.Data.yplace = MyCommFun.Str2Decimal(this.txtLngYPoint.Text);

            var updateResult = Global.Bus.Send(Constants.HotelServiceAddress,
                new ModifyHotelInfoCommand()
                {
                    HotelInfo = hotel.Data
                });

            if (!updateResult.IsSuccess || updateResult.Data==0)
            {
                return;
            }

            var hotelPictures = new List<GetHotelPictureResponse>();

            for (var i = 1; i <= 6; i++)
            {
                title = this.FindControl("title" + i) as TextBox;
                sortid = this.FindControl("sortid" + i) as TextBox;
                picUrl = this.FindControl("picUrl" + i) as TextBox;
                picTiaozhuan = this.FindControl("picTiaozhuan" + i) as TextBox;

                if (title.Text.Trim() != "" && sortid.Text.Trim() != "")
                {
                    var hotelPicture = new GetHotelPictureResponse
                    {
                        hotelid = hotelid,
                        title = title.Text,
                        sortid = MyCommFun.Str2Int(sortid.Text),
                        picUrl = picUrl.Text,
                        picTiaozhuan = picTiaozhuan.Text,
                        createDate = DateTime.Now
                    };

                    hotelPictures.Add(hotelPicture);
                }
            }

            var modifyResult = Global.Bus.Send(Constants.HotelServiceAddress, new ModifyHotelPictureCommand()
                                {
                                    HotelId = hotelid,
                                    HotelPictures = new GetHotelPictureListResponse()
                                                    {
                                                        HotelPictureList = hotelPictures
                                                    }
                                });

            if (modifyResult.IsSuccess && modifyResult.Data>0)
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改商家设置，主键为" + hotelid); //记录日志
            }
            // JscriptMsg("修改成功！", "hotel_list.aspx", "Success");
        }

        private void SetLocation()
        {
            string location = string.Empty;
            if (action == MXEnums.ActionEnum.View.ToString())
            {
                location += "<a href = \"hotel_list.aspx\" class=\"home\"><i></i><span>商户或门店列表</span></a>";
                location += "<i class=\"arrow\"></i>";
                location += "<span>商户或门店信息查看</span>";
            }
            else
            {
                location += "<a class=\"home\"><i></i><span>商户或门店信息设置</span></a>";
            }

            divLocation.InnerHtml = location;
        }
    }
}