using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public partial class BillModel
    {
        public string nameproduct { get; set; }
        public string price { get; set; }
        public string tax { get; set; }
        public string ship { get; set; }
        public string total { get; set; }
        public string namect { get; set; }
        public string phonect { get; set; }
        public string addressct { get; set; }
        public int idUser { get; set; }
        public int idproduct { get; set; } 
    } 
}