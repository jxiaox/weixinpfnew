using System.Data;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IHotelPictureRepository
    {
        int Add(HotelPictureInfo model);

        HotelPictureInfo DataRowToModel(DataRow row);

        DataSet GetList(string strWhere);

        bool Deletepic(int hotelid);

        DataSet GetList(int hotelid);
    }
}