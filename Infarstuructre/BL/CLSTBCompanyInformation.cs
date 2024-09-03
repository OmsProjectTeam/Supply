

namespace Infarstuructre.BL
{
    public  interface IICompanyInformation
    {
        List<TBCompanyInformation> GetAll();
        TBCompanyInformation GetById(int IdCompanyInformation);
        bool saveData(TBCompanyInformation savee);
        bool UpdateData(TBCompanyInformation updatss);
        bool deleteData(int IdCompanyInformation);
        List<TBCompanyInformation> GetAllv(int IdCompanyInformation);
        bool DELETPHOTO(int IdCompanyInformation);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBCompanyInformation: IICompanyInformation
    {
        MasterDbcontext dbcontext;
        public CLSTBCompanyInformation(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBCompanyInformation> GetAll()
        {
            List<TBCompanyInformation> MySlider = dbcontext.TBCompanyInformations.OrderByDescending(n => n.IdCompanyInformation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBCompanyInformation GetById(int IdCompanyInformation)
        {
            TBCompanyInformation sslid = dbcontext.TBCompanyInformations.FirstOrDefault(a => a.IdCompanyInformation == IdCompanyInformation);
            return sslid;
        }
        public bool saveData(TBCompanyInformation savee)
        {
            try
            {
                dbcontext.Add<TBCompanyInformation>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBCompanyInformation updatss)
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
        public bool deleteData(int IdCompanyInformation)
        {
            try
            {
                var catr = GetById(IdCompanyInformation);
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
        public List<TBCompanyInformation> GetAllv(int IdCompanyInformation)
        {
            List<TBCompanyInformation> MySlider = dbcontext.TBCompanyInformations.OrderByDescending(n => n.IdCompanyInformation == IdCompanyInformation).Where(a => a.IdCompanyInformation == IdCompanyInformation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdCompanyInformation)
        {
            try
            {
                var catr = GetById(IdCompanyInformation);
                //using (FileStream fs = new FileStream(catr.Photo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                if (!string.IsNullOrEmpty(catr.Photo))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", catr.Photo);
                    if (System.IO.File.Exists(oldFilePath))
                    {


                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }
                //}


                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DELETPHOTOWethError(string PhotoNAme)
        {
            try
            {
                if (!string.IsNullOrEmpty(PhotoNAme))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Home", PhotoNAme);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        // استخدم FileShare.None للسماح بحذف الملف أثناء استخدامه
                        using (FileStream fs = new FileStream(oldFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            System.Threading.Thread.Sleep(200);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        System.IO.File.Delete(oldFilePath);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                // يفضل ألا تترك البرنامج يتجاوز الأخطاء بصمت، يفضل تسجيل الخطأ أو إعادة رميه
                return false;
            }
        }
    }
}
