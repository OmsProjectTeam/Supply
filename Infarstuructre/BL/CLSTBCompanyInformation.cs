

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domin.Entity.Helper;

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
        /////////////////////API///////////////////////////////
        ///
        Task<List<TBCompanyInformation>> GetAllAsync();
        Task<TBCompanyInformation> GetByIdAsync(int IdMerchants);
        Task<bool> AddDataAsync(TBCompanyInformation savee);
        Task<bool> DeleteDataAsync(int IdMerchants);
        Task<bool> UpdateDataAsync(TBCompanyInformation update);
        Task<bool> DELETPHOTOASYNC(int IdMerchants);
        Task<bool> DELETPHOTOWethErrorAsync(string PhotoNAme);
    }
    public class CLSTBCompanyInformation: IICompanyInformation
    {
        MasterDbcontext dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CLSTBCompanyInformation(MasterDbcontext dbcontext1, IHttpContextAccessor httpContextAccessor)
        {
            dbcontext = dbcontext1;
            _httpContextAccessor = httpContextAccessor;
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

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBCompanyInformation>> GetAllAsync()
        {
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;


            var myDatd = await dbcontext.TBCompanyInformations.OrderByDescending(n => n.IdCompanyInformation).Where(a => a.CurrentState == true)
                .Select( n => new TBCompanyInformation
                {
                    IdCompanyInformation = n.IdCompanyInformation,
                    Photo = $"{scheme}://{host}/Images/Home/{n.Photo}",
                    CompanyName = n.CompanyName,
                    PhoneNumber = n.PhoneNumber,
                    EmailCompany = n.EmailCompany,
                    AddressEn = n.AddressEn,
                    ShortDescriptionEn = n.ShortDescriptionEn,
                    UrlFaceBook = n.UrlFaceBook,
                    UrlTwitter = n.UrlTwitter,
                    UrlInstgram = n.UrlInstgram,
                    UrlMap = n.UrlMap,
                    PhoneNumber2 = n.PhoneNumber2,
                    DateTimeEntry = n.DateTimeEntry,
                    DataEntry = n.DataEntry,
                    CurrentState = n.CurrentState,

                }).ToListAsync();

            return myDatd;
        }

        public async Task<TBCompanyInformation> GetByIdAsync(int id)
        {
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;

            var sslid = await dbcontext.TBCompanyInformations.FirstOrDefaultAsync(a => a.IdCompanyInformation == id);

            if(sslid == null)
            return null;

            var company = new TBCompanyInformation
            {
                IdCompanyInformation = sslid.IdCompanyInformation,
                Photo = $"{scheme}://{host}/Images/Home/{sslid.Photo}",
                CompanyName = sslid.CompanyName,
                PhoneNumber = sslid.PhoneNumber,
                EmailCompany = sslid.EmailCompany,
                AddressEn = sslid.AddressEn,
                ShortDescriptionEn = sslid.ShortDescriptionEn,
                UrlFaceBook = sslid.UrlFaceBook,
                UrlTwitter = sslid.UrlTwitter,
                UrlInstgram = sslid.UrlInstgram,
                UrlMap = sslid.UrlMap,
                PhoneNumber2 = sslid.PhoneNumber2,
                DateTimeEntry = sslid.DateTimeEntry,
                DataEntry = sslid.DataEntry,
                CurrentState = sslid.CurrentState,
            };

            return company;
        }

        public async Task<bool> AddDataAsync(TBCompanyInformation savee)
        {
            try
            {
                await dbcontext.AddAsync<TBCompanyInformation>(savee);
                await dbcontext.SaveChangesAsync();
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
                var merchant = await GetByIdAsync(id);
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

        public async Task<bool> UpdateDataAsync(TBCompanyInformation update)
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

        public async Task<bool> DELETPHOTOASYNC(int id)
        {
            try
            {
                var catr = await GetByIdAsync(id);
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
