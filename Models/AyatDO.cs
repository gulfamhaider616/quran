using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran
{
    public class AyatDO
    {
        public int ChapterID { get; set; }
        public int VerseID { get; set; }
        public string AyatText { get; set; }
        public string EnglishTranslation { get; set; }
        public string UrduTranslation { get; set; }
        public string IndonasianTranslation { get; set; }
        public string TurkishTranslation { get; set; }
        public string ChineseTranslation { get; set; }
        public string SpanishTranslation { get; set; }
        public int TotalVerses { get; set; }
    }
}
