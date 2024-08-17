

namespace Infarstuructre.BL
{
    public interface IISupportTicket
    {
        List<TBViewSupportTicket> GetAll();
        TBSupportTicket GetById(int IdSupportTicket);
        bool saveData(TBSupportTicket savee);
        bool deleteData(int IdSupportTicket);
        List<TBSupportTicket> GetAllv(int IdSupportTicket);
        bool DELETPHOTO(int IdSupportTicket);
        bool DELETPHOTOWethError(string PhotoNAme);
        bool UpdateData(TBSupportTicket updatss);
    }
    public class CLSTBSupportTicket: IISupportTicket
    {
        MasterDbcontext dbcontext;
        public CLSTBSupportTicket(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }
        public List<TBViewSupportTicket> GetAll()
        {
            List<TBViewSupportTicket> MySlider = dbcontext.ViewSupportTicket.OrderByDescending(n => n.IdSupportTicket).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBSupportTicket GetById(int IdSupportTicket)
        {
            TBSupportTicket sslid = dbcontext.TBSupportTickets.FirstOrDefault(a => a.IdSupportTicket == IdSupportTicket);
            return sslid;
        }
        public bool saveData(TBSupportTicket savee)
        {
            try
            {
                dbcontext.Add<TBSupportTicket>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBSupportTicket updatss)
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
        public bool deleteData(int IdSupportTicket)
        {
            try
            {
                var catr = GetById(IdSupportTicket);
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
        public List<TBSupportTicket> GetAllv(int IdSupportTicket)
        {
            List<TBSupportTicket> MySlider = dbcontext.TBSupportTickets.OrderByDescending(n => n.IdSupportTicket == IdSupportTicket).Where(a => a.IdSupportTicket == IdSupportTicket).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdSupportTicket)
        {
            try
            {
                var catr = GetById(IdSupportTicket);
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
