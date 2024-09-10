

using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIOrder
    {
        List<TBViewOrder> GetAll();
        List<TBViewOrder> GetAllActive();
        TBOrder GetById(int IdPurchaseOrder);
        bool saveData(TBOrder savee);
        bool UpdateData(TBOrder updatss);
        bool deleteData(int IdPurchaseOrder);
        List<TBViewOrder> GetAllv(int IdPurchaseOrder);

        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        Task<List<TBOrder>> GetAllAsync();
        Task<List<TBOrder>> GetAllvAsync(int IdPurchaseOrder);
        Task<List<TBOrder>> GetAllDataentryAsync(string dataEntry);
        Task<TBOrder> GetByIdAsync(int IdPurchaseOrder);
        Task<List<TBOrder>> GetAllActiveAsync();
        Task<bool> AddDataAsync(TBOrder savee);
        Task<bool> DeleteDataAsync(int TBOrder);
        Task<bool> UpdateDataAsync(TBOrder update);

    }
    public class CLSTBOrder: IIOrder
    {
        MasterDbcontext dbcontext;
        public CLSTBOrder(MasterDbcontext dbcontext1)
        {
            dbcontext=dbcontext1;
        }

        public List<TBViewOrder> GetAll()
        {
            try
            {
                List<TBViewOrder> MySlider = dbcontext.ViewOrder.Where(a => a.CurrentState == true).ToList();
                return MySlider;
            }catch (Exception ex) { ex.ToString(); }
            return new List<TBViewOrder>();
        }
        public List<TBViewOrder> GetAllActive()
        {
            List<TBViewOrder> MySlider = dbcontext.ViewOrder.OrderByDescending(n => n.IdPurchaseOrder).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBOrder GetById(int IdPurchaseOrder)
        {
            TBOrder sslid = dbcontext.TBOrders.FirstOrDefault(a => a.IdPurchaseOrder == IdPurchaseOrder);
            return sslid;
        }
        public bool saveData(TBOrder savee)
        {
            try
            {
                dbcontext.Add<TBOrder>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBOrder updatss)
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
        public bool deleteData(int IdPurchaseOrder)
        {
            try
            {
                var catr = GetById(IdPurchaseOrder);
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
        public List<TBViewOrder> GetAllv(int IdPurchaseOrder)
        {
            List<TBViewOrder> MySlider = dbcontext.ViewOrder.OrderByDescending(n => n.IdPurchaseOrder == IdPurchaseOrder).Where(a => a.IdPurchaseOrder == IdPurchaseOrder).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBOrder>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBOrders.OrderByDescending(n => n.IdPurchaseOrder).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBOrder>> GetAllvAsync(int IdOrder)
        {
            var myDatd = await dbcontext.TBOrders.OrderByDescending(n => n.IdPurchaseOrder).Where(a => a.IdPurchaseOrder == IdOrder).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBOrder>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.TBOrders.Where(a => a.DataEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBOrder>> GetAllActiveAsync()
        {
            List<TBOrder> MySlider = await dbcontext.TBOrders.OrderByDescending(n => n.IdPurchaseOrder).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBOrder> GetByIdAsync(int IdPurchaseOrder)
        {
            var sslid = await dbcontext.TBOrders.FirstOrDefaultAsync(a => a.IdPurchaseOrder == IdPurchaseOrder);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBOrder savee)
        {
            try
            {
                await dbcontext.AddAsync<TBOrder>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdPurchaseOrder)
        {
            try
            {
                var email = await GetByIdAsync(IdPurchaseOrder);
                email.CurrentState = false;
                dbcontext.Entry(email).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBOrder update)
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
