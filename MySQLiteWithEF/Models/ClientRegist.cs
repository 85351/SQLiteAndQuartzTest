using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class ClientRegist
    {
        [Key]
        public int Id { get; set; }
        public string customsCode { get; set; }
        public string clientId { get; set; }
        public string manufacturer { get; set; }
        public string serviceUrl { get; set; }
        public int registTime { get; set; }
        public string strRegistTime { get; set; }
    }
}
