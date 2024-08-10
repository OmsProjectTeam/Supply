

namespace Infarstuructre.BL
{
    public interface IIProductInformation
    {
        List<TBViewProductInformation> GetAll();
        TBProductInformation GetById(int IdProductInformation);
        bool saveData(TBProductInformation savee);
        bool UpdateData(TBProductInformation updatss);
        bool deleteData(int IdProductInformation);
        List<TBViewProductInformation> GetAllv(int IdProductInformation);
        bool DELETPHOTO(int IdProductInformation);
        bool DELETPHOTOWethError(string PhotoNAme);
    }
    public class CLSTBProductInformation: IIProductInformation
    {
        MasterDbcontext dbcontext;
        public CLSTBProductInformation(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBViewProductInformation> GetAll()
        {
            List<TBViewProductInformation> MySlider = dbcontext.ViewProductInformation.OrderByDescending(n => n.IdProductInformation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBProductInformation GetById(int IdProductInformation)
        {
            TBProductInformation sslid = dbcontext.TBProductInformations.FirstOrDefault(a => a.IdProductInformation == IdProductInformation);
            return sslid;
        }
        public bool saveData(TBProductInformation savee)
        {
            try
            {
                dbcontext.Add<TBProductInformation>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBProductInformation updatss)
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
        public bool deleteData(int IdProductInformation)
        {
            try
            {
                var catr = GetById(IdProductInformation);
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
        public List<TBViewProductInformation> GetAllv(int IdProductInformation)
        {
            List<TBViewProductInformation> MySlider = dbcontext.ViewProductInformation.OrderByDescending(n => n.IdProductInformation == IdProductInformation).Where(a => a.IdProductInformation == IdProductInformation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdProductInformation)
        {
            try
            {
                var catr = GetById(IdProductInformation);
                //using (FileStream fs = new FileStream(catr.Photo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                if (!string.IsNullOrEmpty(catr.Photo))
                {
                    // إذا كان هناك صورة قديمة، قم بمسحها من الملف
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Product", catr.Photo);
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
                    var oldFilePath = Path.Combine(@"wwwroot/Images/Product", PhotoNAme);
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
