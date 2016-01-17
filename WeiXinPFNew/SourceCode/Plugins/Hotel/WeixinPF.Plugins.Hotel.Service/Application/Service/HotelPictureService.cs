using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class HotelPictureService
    {

        public IHotelPictureRepository _repository;
        public HotelPictureService(IHotelPictureRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(HotelPictureInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HotelPictureInfo> GetModelList(string strWhere)
        {
            DataSet ds = this._repository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HotelPictureInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<HotelPictureInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HotelPictureInfo model = null;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this._repository.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public bool Deletepic(int id)
        {

            return this._repository.Deletepic(id);
        }
        public DataSet GetList(int hotelid)
        {
            return this._repository.GetList(hotelid);
        }
    }
}
