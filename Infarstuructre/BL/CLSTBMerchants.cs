

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIMerchants
    {
        List<TBViewMerchants> GetAll();
        TBMerchants GetById(int IdMerchants);
        bool saveData(TBMerchants savee);
        bool UpdateData(TBMerchants updatss);
        bool deleteData(int IdMerchants);
        List<TBMerchants> GetAllv(int IdMerchants);
        bool DELETPHOTO(int IdMerchants);
        bool DELETPHOTOWethError(string PhotoNAme);
        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBViewMerchants>> GetAllAsync();
        Task<List<TBMerchants>> GetAllActiveAsync();
        Task<TBMerchants> GetByIdAsync(int IdMerchants);
        Task<bool> AddDataAsync(TBMerchants savee);
        Task<bool> DeleteDataAsync(int IdMerchants);
        Task<bool> UpdateDataAsync(TBMerchants update);
        Task<bool> DELETPHOTOASYNC(int IdMerchants);
        Task<bool> DELETPHOTOWethErrorAsync(string PhotoNAme);
    }
    public class CLSTBMerchants: IIMerchants
    {
        MasterDbcontext dbcontext;
        public CLSTBMerchants(MasterDbcontext dbcntext1)
        {
            dbcontext = dbcntext1;
        }
        public List<TBViewMerchants> GetAll()
        {
            List<TBViewMerchants> MySlider = dbcontext.ViewMerchants.OrderByDescending(n => n.IdMerchants).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBMerchants GetById(int IdMerchants)
        {
            TBMerchants sslid = dbcontext.TBMerchantss.FirstOrDefault(a => a.IdMerchants == IdMerchants);
            return sslid;
        }
        public bool saveData(TBMerchants savee)
        {
            try
            {
                dbcontext.Add<TBMerchants>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBMerchants updatss)
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
        public bool deleteData(int IdMerchants)
        {
            try
            {
                var catr = GetById(IdMerchants);
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
        public List<TBMerchants> GetAllv(int IdMerchants)
        {
            List<TBMerchants> MySlider = dbcontext.TBMerchantss.OrderByDescending(n => n.IdMerchants == IdMerchants).Where(a => a.IdMerchants == IdMerchants).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public bool DELETPHOTO(int IdMerchants)
        {
            try
            {
                var catr = GetById(IdMerchants);
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

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBViewMerchants>> GetAllAsync()
        {
            var myDatd = await dbcontext.ViewMerchants.OrderByDescending(n => n.IdMerchants).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBMerchants>> GetAllActiveAsync()
        {
            var MySlider = await dbcontext.TBMerchantss.OrderByDescending(n => n.IdMerchants).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBMerchants> GetByIdAsync(int IdMerchants)
        {
            var sslid = await dbcontext.TBMerchantss.FirstOrDefaultAsync(a => a.IdMerchants == IdMerchants);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBMerchants savee)
        {
            try
            {
                await dbcontext.AddAsync<TBMerchants>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdMerchants)
        {
            try
            {
                var merchant = await GetByIdAsync(IdMerchants);
                merchant.CurrentState = false;
                dbcontext.Entry(merchant).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBMerchants update)
        {
            try
            {
                dbcontext.Entry(update).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DELETPHOTOASYNC(int IdMerchants)
        {
            try
            {
                var catr = await GetByIdAsync(IdMerchants);
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
        public async Task<bool> DELETPHOTOWethErrorAsync(string PhotoNAme)
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
