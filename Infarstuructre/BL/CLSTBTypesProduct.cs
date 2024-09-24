
using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IITypesProduct
    {
        List<TBTypesProduct> GetAll();
        TBTypesProduct GetById(int IdTypesProduct);
        bool saveData(TBTypesProduct savee);
        bool UpdateData(TBTypesProduct updatss);
        bool deleteData(int IdTypesProduct);
        List<TBTypesProduct> GetAllv(int IdTypesProduct);
        // ////////////////////APIs//////////////////////////////////////
        Task<List<TBTypesProduct>> GetAllAsync();
        Task<List<TBTypesProduct>> GetAllActiveAsync();
        Task<TBTypesProduct> GetByIdAsync(int IdTypesProduct);
        Task<bool> AddDataAsync(TBTypesProduct savee);
        Task<bool> DeleteDataAsync(int IdTypesProduct);
        Task<bool> UpdateDataAsync(TBTypesProduct update);

    }

    public class CLSTBTypesProduct: IITypesProduct
    {
        MasterDbcontext dbcontext;
        public CLSTBTypesProduct(MasterDbcontext dbcontext1)
        {
            dbcontext= dbcontext1;
        }
        public List<TBTypesProduct> GetAll()
        {
            List<TBTypesProduct> MySlider = dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypesProduct GetById(int IdTypesProduct)
        {
            TBTypesProduct sslid = dbcontext.TBTypesProducts.FirstOrDefault(a => a.IdTypesProduct == IdTypesProduct);
            return sslid;
        }
        public bool saveData(TBTypesProduct savee)
        {
            try
            {
                dbcontext.Add<TBTypesProduct>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypesProduct updatss)
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
        public bool deleteData(int IdTypesProduct)
        {
            try
            {
                var catr = GetById(IdTypesProduct);
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
        public List<TBTypesProduct> GetAllv(int IdTypesProduct)
        {
            List<TBTypesProduct> MySlider = dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct == IdTypesProduct).Where(a => a.IdTypesProduct == IdTypesProduct).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBTypesProduct>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBTypesProduct>> GetAllActiveAsync()
        {
            var MySlider = await dbcontext.TBTypesProducts.OrderByDescending(n => n.IdTypesProduct).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTypesProduct> GetByIdAsync(int IdTypesProduct)
        {
            var sslid = await dbcontext.TBTypesProducts.FirstOrDefaultAsync(a => a.IdTypesProduct == IdTypesProduct);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTypesProduct savee)
        {
            try
            {
                await dbcontext.AddAsync<TBTypesProduct>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdTypesProduct)
        {
            try
            {
                var product = await GetByIdAsync(IdTypesProduct);
                product.CurrentState = false;
                dbcontext.Entry(product).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBTypesProduct update)
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
    }
}
