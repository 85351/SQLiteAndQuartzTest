using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class RecordQueryTempService
    {
        RecordQueryTempDbContext db ;
        public RecordQueryTempService()
        {
            db = new RecordQueryTempDbContext();
        }
        public void Add(string path)
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
        public List<RecordQueryTemp> GetAll()
        {
            return db.RecordQueryTemp.ToList();
        }
        public void UpdateReasonAndCount(string path, string reason)
        {
            if (!db.RecordQueryTemp.Any(i => i.path == path))
                return;

            var r = db.RecordQueryTemp.Where(i => i.path == path).FirstOrDefault();
            r.uploadCount = r.uploadCount + 1;
            r.reason = reason;
            db.Entry(r).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(string path)
        {
            if (!db.RecordQueryTemp.Any(i => i.path == path))
                return;

            var r = db.RecordQueryTemp.Where(i => i.path == path).FirstOrDefault();
            db.RecordQueryTemp.Remove(r);
            db.SaveChanges();

        }
    }
}
