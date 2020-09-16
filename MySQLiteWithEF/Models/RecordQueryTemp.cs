using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class RecordQueryTemp
    {
        [Key]
        public int Id { get; set; }
        public string path { get; set; }
        public int addTime { get; set; }
        public int uploadCount { get; set; }
        public string reason { get; set; }
    }
}
