

namespace Infarstuructre.BL
{
    public interface IIScrapingHtmlTitle
    {
        List<TBScrapingHtmlTitle> GetAll();
        TBScrapingHtmlTitle GetById(int IdScrapingHtmlTitle);
        bool saveData(TBScrapingHtmlTitle savee);
        bool UpdateData(TBScrapingHtmlTitle updatss);
        bool deleteData(int IdScrapingHtmlTitle);
        List<TBScrapingHtmlTitle> GetAllv(int IdScrapingHtmlTitle);
    }
    public class CLSTBScrapingHtmlTitle: IIScrapingHtmlTitle
    {
        MasterDbcontext dbcontext;
        public CLSTBScrapingHtmlTitle(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBScrapingHtmlTitle> GetAll()
        {
            List<TBScrapingHtmlTitle> MySlider = dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBScrapingHtmlTitle GetById(int IdScrapingHtmlTitle)
        {
            TBScrapingHtmlTitle sslid = dbcontext.TBScrapingHtmlTitles.FirstOrDefault(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle);
            return sslid;
        }
        public bool saveData(TBScrapingHtmlTitle savee)
        {
            try
            {
                dbcontext.Add<TBScrapingHtmlTitle>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBScrapingHtmlTitle updatss)
        {
            try
            {
                dbcontext.Entry(updatss).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteData(int IdScrapingHtmlTitle)
        {
            try
            {
                var catr = GetById(IdScrapingHtmlTitle);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<TBScrapingHtmlTitle> GetAllv(int IdScrapingHtmlTitle)
        {
            List<TBScrapingHtmlTitle> MySlider = dbcontext.TBScrapingHtmlTitles.OrderByDescending(n => n.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.IdScrapingHtmlTitle == IdScrapingHtmlTitle).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

    }
}
