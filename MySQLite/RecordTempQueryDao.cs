using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MySQLite
{

    public class RecordTempQueryDao
    {
        public static void Add(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            path = path.Trim();
            var sql = "insert into RecordQueryTemp(path,addTime,uploadCount) values(@path,@addTime,0)";
            var paraters = new SQLiteParameter[] {
                    new SQLiteParameter("@path",path),
                    new SQLiteParameter("@addTime",DateTime.Now.GetSeconds()),
                };
            SQLiteHelper.ExecuteNonQuery(SQLiteDBType.HistoryRecordTemp, sql, paraters);
        }
        public static bool Exist(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;
            path = path.Trim();

            var sql = "select 1 from RecordQueryTemp where path=@path";
            var paraters = new SQLiteParameter[] {
                    new SQLiteParameter("path",path),
                };
            var result = SQLiteHelper.ExecuteScalar(SQLiteDBType.HistoryRecordTemp, sql, paraters);

            if (result == null)
                return false;
            return result.ToString() == "1";
        }

        public static List<RecordQueryTemp> GetAll()
        {
            var sql = "select path,addTime from RecordQueryTemp order by addTime desc";
            var ds = SQLiteHelper.GetDataSet(SQLiteDBType.HistoryRecordTemp, sql);

            var rqs = new List<RecordQueryTemp>();
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            var rows = ds.Tables[0].Select();
            foreach (var dr in rows)
            {
                var rq = new RecordQueryTemp();
                rq.Path = dr[0].ToString();
                rq.AddTime = int.Parse(dr[1].ToString());
                rqs.Add(rq);
            }
            return rqs;
        }
        public static RecordQueryTemp Get(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            path = path.Trim();

            var sql = "select path,addTime from RecordQueryTemp where path=@path ";
            var paraters = new SQLiteParameter[] {
                    new SQLiteParameter("path",path),
                };
            var ds = SQLiteHelper.GetDataSet(SQLiteDBType.HistoryRecordTemp, sql, paraters);

            var rq = new RecordQueryTemp();
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            var dr = ds.Tables[0].Rows[0];

            rq.Path = dr[0].ToString();
            rq.AddTime = int.Parse(dr[1].ToString());

            return rq;
        }

        public static void UpdateReasonAndCount(string path, string reason)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;
            path = path.Trim();

            var sql = "update RecordQueryTemp set uploadCount=uploadCount+1,reason=@reason where path=@path ";
            var paraters = new SQLiteParameter[] {
                    new SQLiteParameter("@path",path),
                    new SQLiteParameter("@reason",reason),
                };
            SQLiteHelper.ExecuteNonQuery(SQLiteDBType.HistoryRecordTemp, sql, paraters);
        }
        public static void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;
            path = path.Trim();

            var sql = "delete from RecordQueryTemp where path=@path ";
            var paraters = new SQLiteParameter[] {
                    new SQLiteParameter("path",path),
                };
            SQLiteHelper.ExecuteNonQuery(SQLiteDBType.HistoryRecordTemp, sql, paraters);
        }
    }
}
