using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.DBUtility;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class HotelPictureRepository:IHotelPictureRepository
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HotelPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_hotel_pic(");
            strSql.Append("hotelid,title,sortid,picUrl,picTiaozhuan,createDate)");
            strSql.Append(" values (");
            strSql.Append("@hotelid,@title,@sortid,@picUrl,@picTiaozhuan,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@hotelid", SqlDbType.Int, 4),
                new SqlParameter("@title", SqlDbType.VarChar, 200),
                new SqlParameter("@sortid", SqlDbType.Int, 4),
                new SqlParameter("@picUrl", SqlDbType.VarChar, 200),
                new SqlParameter("@picTiaozhuan", SqlDbType.VarChar, 200),
                new SqlParameter("@createDate", SqlDbType.DateTime)
            };
            parameters[0].Value = model.hotelid;
            parameters[1].Value = model.title;
            parameters[2].Value = model.sortid;
            parameters[3].Value = model.picUrl;
            parameters[4].Value = model.picTiaozhuan;
            parameters[5].Value = model.createDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);

            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HotelPictureInfo DataRowToModel(DataRow row)
        {
            var model = new HotelPictureInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["hotelid"] != null && row["hotelid"].ToString() != "")
                {
                    model.hotelid = int.Parse(row["hotelid"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["sortid"] != null && row["sortid"].ToString() != "")
                {
                    model.sortid = int.Parse(row["sortid"].ToString());
                }
                if (row["picUrl"] != null)
                {
                    model.picUrl = row["picUrl"].ToString();
                }
                if (row["picTiaozhuan"] != null)
                {
                    model.picTiaozhuan = row["picTiaozhuan"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,hotelid,title,sortid,picUrl,picTiaozhuan,createDate ");
            strSql.Append(" FROM wx_hotel_pic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool Deletepic(int hotelid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_pic ");
            strSql.Append(" where hotelid=@hotelid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@hotelid", SqlDbType.Int, 4)
            };
            parameters[0].Value = hotelid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetList(int hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,hotelid,title,sortid,picUrl,picTiaozhuan,createDate ");
            strSql.Append(" FROM wx_hotel_pic where hotelid='" + hotelid + "' ");

            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
