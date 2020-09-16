using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class RecordQueryTempService
    {
        public static void Add(string path)
        {
            using (var db = new RecordQueryTempDbContext())
            {
                var r = new RecordQueryTemp
                {
                    path = path,
                    addTime = DateTime.Now.GetSeconds(),
                    uploadCount = 0,
                };
                db.RecordQueryTemp.Add(r);
                db.SaveChanges();
            }
        }
        public static List<RecordQueryTemp> GetAll()
        {
            using (var db = new RecordQueryTempDbContext())
            {
                return db.RecordQueryTemp.ToList();
            }
        }
        public static void UpdateReasonAndCount(string path, string reason)
        {
            using (var db = new RecordQueryTempDbContext())
            {
                if (!db.RecordQueryTemp.Any(i => i.path == path))
                    return;

                var r = db.RecordQueryTemp.Where(i => i.path == path).FirstOrDefault();
                r.uploadCount = r.uploadCount + 1;
                r.reason = reason;
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void Delete(string path)
        {
            using (var db = new RecordQueryTempDbContext())
            {
                if (!db.RecordQueryTemp.Any(i => i.path == path))
                    return;

                var r = db.RecordQueryTemp.Where(i => i.path == path).FirstOrDefault();
                db.RecordQueryTemp.Remove(r);
                db.SaveChanges();
            }
        }
    }
}
