using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran
{
    public class SuraDetailContract
    {
        public SuraNamesDO SuraDetail { get; set; }
        public List<AyatDO> AyatList { get; set; }
        public List<SuraNamesDO> SuraList { get; set; }
        public string trans { get;set;} 
    }
}
