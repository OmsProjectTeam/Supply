

using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infarstuructre.BL
{
    public interface IIProductInformation
    {
        List<TBViewProductInformation> GetAll();
        TBProductInformation GetById(int IdProductInformation);
        TBProductInformation GetByName(string name);
        bool saveData(TBProductInformation savee);
        bool UpdateData(TBProductInformation updatss);
        bool deleteData(int IdProductInformation);
        List<TBViewProductInformation> GetAllv(int IdProductInformation);
        bool DELETPHOTO(int IdProductInformation);
        bool DELETPHOTOWethError(string PhotoNAme);
        // ///////////////////API///////////////////////////////////////////////
        Task<List<TBViewProductInformation>> GetAllAsync();
        Task<List<TBViewProductInformation>> GetAllvAsync(int id);
        Task<TBViewProductInformation> GetByIdFromViewAsync(int id);
        Task<TBProductInformation> GetByIdAsync(int id);
        Task<TBProductInformation> GetByNameAsync(string name);
        Task<bool> AddDataAsync(TBProductInformation data);
        Task<bool> DeleteDataAsync(int id);
        Task<bool> UpdateDataAsync(TBProductInformation data);
        Task<bool> DELETPHOTOAsync(int id);
        Task<bool> DELETPHOTOWITHERRORAsync(string name);
    }
    public class CLSTBProductInformation : IIProductInformation
    {
        MasterDbcontext dbcontext;
        public CLSTBProductInformation(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBViewProductInformation> GetAll()
        {
            List<TBViewProductInformation> MySlider = dbcontext.ViewProductInformation.OrderByDescending(n => n.IdProductInformation).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBProductInformation GetById(int idProductInformation)
        {
            TBProductInformation sslid = dbcontext.TBProductInformations.FirstOrDefault(a => a.IdProductInformation == idProductInformation);
            return sslid;
        }

        public TBProductInformation GetByName(string name)
        {
            TBProductInformation sslid = dbcontext.TBProductInformations.FirstOrDefault(a => a.ProductName.Contains(name));
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

        // ///////////////////////////APIs///////////////////////////////////////////////////////////
        public async Task<List<TBViewProductInformation>> GetAllAsync()
        {
            var allData = await dbcontext.ViewProductInformation.OrderByDescending(n => n.IdProductInformation).Where(a => a.CurrentState == true).ToListAsync();
            return allData;
        }

        public async Task<List<TBViewProductInformation>> GetAllvAsync(int id)
        {
            List<TBViewProductInformation> MySlider = await dbcontext.ViewProductInformation.OrderByDescending(n => n.IdProductInformation == id).Where(a => a.IdProductInformation == id).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBProductInformation> GetByIdAsync(int id)
        {
            TBProductInformation sslid = await dbcontext.TBProductInformations.FirstOrDefaultAsync(a => a.IdProductInformation == id);
            return sslid;
        }

        public async Task<TBProductInformation> GetByNameAsync(string name)
        {
            TBProductInformation sslid = await dbcontext.TBProductInformations.FirstOrDefaultAsync(a => a.ProductName == name);
            return sslid;
        }

        public async Task<TBViewProductInformation> GetByIdFromViewAsync(int id)
        {
            TBViewProductInformation sslid = await dbcontext.ViewProductInformation.FirstOrDefaultAsync(a => a.IdProductInformation == id);
            return sslid;
        }


        public async Task<bool> AddDataAsync(TBProductInformation data)
        {
            try
            {
                dbcontext.Add<TBProductInformation>(data);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteDataAsync(int id)
        {
            try
            {
                var catr = await GetByIdAsync(id);
                catr.CurrentState = false;
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateDataAsync(TBProductInformation data)
        {
            try
            {
                dbcontext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DELETPHOTOAsync(int id)
        {
            var result = DELETPHOTO(id);
            return result;
        }
        public async Task<bool> DELETPHOTOWITHERRORAsync(string name)
        {
            var result = DELETPHOTOWethError(name);
            return result;
        }


    }
}