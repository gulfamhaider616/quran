using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.Business
{
    public class QuranBA
    {
        public async Task<List<SuraNamesDO>> GetAllSuraNamesAsync()
        {
            List<SuraNamesDO> list = new List<SuraNamesDO>();
            DataSet dataset = await new QuranDA().GetAllSuraNamesAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    SuraNamesDO sura = new SuraNamesDO();
                    sura.ChapterID = dr.Field<int>("ChapterID");
                    sura.SuraName = dr.Field<string>("SuraName");
                    sura.EnglishName = dr.Field<string>("EnglishName");
                    sura.TotalVerses = dr.Field<int>("TotalVerses");
                    list.Add(sura);
                }
            }

            return list;
        }

        public async Task<SuraDetailContract> GetSuraByIDAsync(int chapterId)
        {
            SuraDetailContract suraDetail = new SuraDetailContract();
            List<AyatDO> list = new List<AyatDO>();
            DataSet dataset = await new QuranDA().GetSuraByIDAsync(chapterId);

            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataset.Tables[0].Rows[0];
                    SuraNamesDO sura = new SuraNamesDO();
                    sura.ChapterID = dr.Field<int>("ChapterID");
                    sura.SuraName = dr.Field<string>("SuraName");
                    sura.EnglishName = dr.Field<string>("EnglishName");
                    sura.TotalVerses = dr.Field<int>("TotalVerses");
                    suraDetail.SuraDetail = sura;
                }

                foreach (DataRow dr in dataset.Tables[1].Rows)
                {
                    AyatDO ayat = new AyatDO();
                    ayat.ChapterID = dr.Field<int>("ChapterID");
                    ayat.VerseID = dr.Field<int>("VerseID");
                    ayat.AyatText = dr.Field<string>("AyahText");
                    ayat.EnglishTranslation = dr.Field<string>("EnglishTranslation");
                    ayat.IndonasianTranslation = dr.Field<string>("IndonasianTranslation");
                    ayat.UrduTranslation = dr.Field<string>("UrduTranslation");
                    list.Add(ayat);
                }

                suraDetail.AyatList = list;
            }

            return suraDetail;
        }
    }
}