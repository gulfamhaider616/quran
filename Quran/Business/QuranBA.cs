using Quran.DataAccess;
using Quran.Models;
using System;
using System.Collections.Generic;

namespace Quran.Business
{
    public class QuranBA
    {
        public List<SuraNamesDO> GetAllSuraNames()
        {
            List<SuraNamesDO> list = new List<SuraNamesDO>();
            foreach (IDictionary<string, object> dr in new QuranDA().GetAllSuraNames())
            {
                SuraNamesDO sura = new SuraNamesDO();
                sura.ChapterID = dr.Get<int>("ChapterID");
                sura.SuraName = dr.Get<string>("SuraName");
                sura.EnglishName = dr.Get<string>("EnglishName");
                sura.TotalVerses = dr.Get<int>("TotalVerses");
                list.Add(sura);
            }
            return list;
        }

        public SuraDetailContract GetSuraByID(int chapterId)
        {
            SuraDetailContract suraDetail = new SuraDetailContract();
            List<AyatDO> list = new List<AyatDO>();
            var (header, ayat) = new QuranDA().GetSuraByID(chapterId);

            if (header.Count > 0)
            {
                IDictionary<string, object> dr = header[0];
                SuraNamesDO sura = new SuraNamesDO();
                sura.ChapterID = dr.Get<int>("ChapterID");
                sura.SuraName = dr.Get<string>("SuraName");
                sura.EnglishName = dr.Get<string>("EnglishName");
                sura.TotalVerses = dr.Get<int>("TotalVerses");
                suraDetail.SuraDetail = sura;
            }

            foreach (IDictionary<string, object> dr in ayat)
            {
                AyatDO a = new AyatDO();
                a.ChapterID = dr.Get<int>("ChapterID");
                a.VerseID = dr.Get<int>("VerseID");
                a.AyatText = dr.Get<string>("AyahText");
                a.EnglishTranslation = dr.Get<string>("EnglishTranslation");
                a.IndonasianTranslation = dr.Get<string>("IndonasianTranslation");
                a.UrduTranslation = dr.Get<string>("UrduTranslation");
                list.Add(a);
            }
            suraDetail.AyatList = list;
            return suraDetail;
        }

        public FeaturedVerseDO GetFeaturedVerse(int position)
        {
            FeaturedVerseDO verse = new FeaturedVerseDO();
            IDictionary<string, object> dr = new QuranDA().GetFeaturedVerse(position);
            if (dr != null)
            {
                verse.ChapterID = dr.Get<int>("ChapterID");
                verse.VerseID = dr.Get<int>("VerseID");
                verse.Arabic = dr.Get<string>("AyahText");
                verse.English = dr.Get<string>("EnglishTranslation");
                verse.Urdu = dr.Get<string>("UrduTranslation");
                verse.SuraName = dr.Get<string>("SuraName");
                verse.EnglishName = dr.Get<string>("EnglishName");
                verse.Total = dr.Get<int>("Total");
            }
            return verse;
        }
    }
}
