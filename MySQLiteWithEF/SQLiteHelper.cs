using MyLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class SQLiteHelper
    {
        static NewLogger Logger = new NewLogger(typeof(SQLiteHelper).FullName);

        protected static string ClientConnStr = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_Client.db")};Cache=Shared";
        protected static string HistoryRecordConnStr = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_HistoryRecord.db")};Cache=Shared";
        protected static string HistoryRecordTempConnStr = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_HistoryRecordTemp.db")};Cache=Shared";
        protected static string RecordDelayUploadImageConnStr = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_DelayUploadImage.db")};Cache=Shared";
        protected static string RecordDelayUploadVideoConnStr = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_DelayUploadVideo.db")};Cache=Shared";

        public static void InitDT()
        {
            try
            {
                string ExecRegistSql = "select 1 from sqlite_master where type = 'table' and name = 'ClientRegist' ";
                if (ExecuteScalar(SQLiteDBType.Client, ExecRegistSql) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.Client, "create table ClientRegist (Id INTEGER PRIMARY KEY AUTOINCREMENT,customsCode text,clientId text,manufacturer text,serviceUrl text,registTime integer,strRegistTime text)");
                }

                string ExecManufacturerSql = "select 1 from sqlite_master where type = 'table' and name = 'Manufacturer' ";
                if (ExecuteScalar(SQLiteDBType.Client, ExecManufacturerSql) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.Client, "create table Manufacturer (Id INTEGER PRIMARY KEY AUTOINCREMENT,Manufacturer text)");
                }

                string ExecRecordQuerySql = "select 1 from sqlite_master where type = 'table' and name = 'RecordQuery' ";
                if (ExecuteScalar(SQLiteDBType.HistoryRecord, ExecRecordQuerySql) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.HistoryRecord, "create table RecordQuery (Id INTEGER PRIMARY KEY AUTOINCREMENT,deviceId text,startDate real,endDate real,queryId text,pageSize integer,idleTime text)");
                }

                string ExecRecordQueryTempSql = "select 1 from sqlite_master where type = 'table' and name = 'RecordQueryTemp' ";
                if (ExecuteScalar(SQLiteDBType.HistoryRecordTemp, ExecRecordQueryTempSql) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.HistoryRecordTemp, "create table RecordQueryTemp(Id INTEGER PRIMARY KEY AUTOINCREMENT,path text,addTime integer,uploadCount integer,reason text)");
                }

                string ExecRecordDelayUpload = "select 1 from sqlite_master where type = 'table' and name = 'DelayUploadImageFile' ";
                if (ExecuteScalar(SQLiteDBType.DelayUploadImage, ExecRecordDelayUpload) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.DelayUploadImage, "create table DelayUploadImageFile(IdAuto INTEGER PRIMARY KEY AUTOINCREMENT,id text,localPath text,ftpPath text,isAlarm boolean,addtime integer,uploadCount integer,reason text)");
                }

                string ExecRecordDelayUploadVideo = "select 1 from sqlite_master where type = 'table' and name = 'DelayUploadVideoFile' ";
                if (ExecuteScalar(SQLiteDBType.DelayUploadVideo, ExecRecordDelayUploadVideo) == null)
                {
                    ExecuteNonQuery(SQLiteDBType.DelayUploadVideo, "create table DelayUploadVideoFile(IdAuto INTEGER PRIMARY KEY AUTOINCREMENT,id text,localPath text,ftpPath text,isAlarm boolean,addtime integer,uploadCount integer,reason text)");
                }

            }
            catch (Exception ex)
            {
                Logger.Error($"SQLiteHelper InitDT error:{ex}");
            }
        }

        public static int ExecuteNonQuery(SQLiteDBType type, string sql, SQLiteParameter[] paramters = null)
        {
            try
            {
                using (var conn = new SQLiteConnection(GetConnStr(type)))
                {
                    conn.Open();
                    var comm = conn.CreateCommand();
                    comm.CommandText = sql;
                    if (paramters != null && paramters.Count() > 0)
                        comm.Parameters.AddRange(paramters);
                    var count = comm.ExecuteNonQuery();
                    return count;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ExecuteNonQuery sql:{sql}{Environment.NewLine}err:{ex}");
                throw ex;
            }
        }
        public static object ExecuteScalar(SQLiteDBType type, string sql, SQLiteParameter[] paramters = null)
        {
            try
            {
                using (var conn = new SQLiteConnection(GetConnStr(type)))
                {
                    conn.Open();
                    var comm = conn.CreateCommand();
                    comm.CommandText = sql;
                    if (paramters != null && paramters.Count() > 0)
                        comm.Parameters.AddRange(paramters);
                    var result = comm.ExecuteScalar();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ExecuteScalar sql:{sql}{Environment.NewLine}err:{ex}");
                throw ex;
            }
        }
        public static DataSet GetDataSet(SQLiteDBType type, string sql, SQLiteParameter[] paramters = null)
        {
            try
            {
                using (var conn = new SQLiteConnection(GetConnStr(type)))
                {
                    conn.Open();
                    var comm = conn.CreateCommand();
                    comm.CommandText = sql;
                    if (paramters != null && paramters.Count() > 0)
                        comm.Parameters.AddRange(paramters);

                    SQLiteDataAdapter da = new SQLiteDataAdapter(comm);
                    var ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"GetDataSet sql:{sql}{Environment.NewLine}err:{ex}");
                throw ex;
            }
        }
        private static string GetConnStr(SQLiteDBType type)
        {
            switch (type)
            {
                case SQLiteDBType.Client:
                    return ClientConnStr;
                case SQLiteDBType.HistoryRecord:
                    return HistoryRecordConnStr;
                case SQLiteDBType.HistoryRecordTemp:
                    return HistoryRecordTempConnStr;
                case SQLiteDBType.DelayUploadImage:
                    return RecordDelayUploadImageConnStr;
                case SQLiteDBType.DelayUploadVideo:
                    return RecordDelayUploadVideoConnStr;
                default:
                    {
                        Logger.Error("need parameter SQLiteDBType");
                        throw new Exception("need SQLiteDBType");
                    }
            }
        }
    }
    public enum SQLiteDBType
    {
        /// <summary>
        /// 入网设备
        /// </summary>
        Client,

        /// <summary>
        /// 历史记录
        /// </summary>
        HistoryRecord,

        /// <summary>
        /// 历史记录
        /// </summary>
        HistoryRecordTemp,

        /// <summary>
        /// 延迟上传图片
        /// </summary>
        DelayUploadImage,
        /// <summary>
        /// 延迟上传视频
        /// </summary>
        DelayUploadVideo,
    }
}
