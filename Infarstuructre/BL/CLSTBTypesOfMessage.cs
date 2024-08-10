

using Domin.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IITypesOfMessage
    {
        List<TBTypesOfMessage> GetAll();
        TBTypesOfMessage GetById(int IdTypesOfMessage);
        bool saveData(TBTypesOfMessage savee);
        bool UpdateData(TBTypesOfMessage updatss);
        bool deleteData(int IdTypesOfMessage);
        List<TBTypesOfMessage> GetAllv(int IdTypesOfMessage);

        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBTypesOfMessage>> GetAllAsync();
        Task<List<TBTypesOfMessage>> GetAllActiveAsync();
        Task<TBTypesOfMessage> GetByIdAsync(int IdTypesOfMessage);
        Task<bool> AddDataAsync(TBTypesOfMessage savee);
        Task<bool> DeleteDataAsync(int IdTypesOfMessage);
        Task<bool> UpdateDataAsync(TBTypesOfMessage update);
    }
    public class CLSTBTypesOfMessage: IITypesOfMessage
    {
        MasterDbcontext dbcontext;
        public CLSTBTypesOfMessage(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }
        public List<TBTypesOfMessage> GetAll()
        {
            List<TBTypesOfMessage> MySlider = dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTypesOfMessage GetById(int IdTypesOfMessage)
        {
            TBTypesOfMessage sslid = dbcontext.TBTypesOfMessages.FirstOrDefault(a => a.IdTypesOfMessage == IdTypesOfMessage);
            return sslid;
        }
        public bool saveData(TBTypesOfMessage savee)
        {
            try
            {
                dbcontext.Add<TBTypesOfMessage>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTypesOfMessage updatss)
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
        public bool deleteData(int IdTypesOfMessage)
        {
            try
            {
                var catr = GetById(IdTypesOfMessage);
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
        public List<TBTypesOfMessage> GetAllv(int IdTypesOfMessage)
        {
            List<TBTypesOfMessage> MySlider = dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage == IdTypesOfMessage).Where(a => a.IdTypesOfMessage == IdTypesOfMessage).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBTypesOfMessage>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBTypesOfMessage>> GetAllActiveAsync()
        {
            var MySlider = await dbcontext.TBTypesOfMessages.OrderByDescending(n => n.IdTypesOfMessage).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTypesOfMessage> GetByIdAsync(int IdTypesOfMessage)
        {
            var sslid = await dbcontext.TBTypesOfMessages.FirstOrDefaultAsync(a => a.IdTypesOfMessage == IdTypesOfMessage);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTypesOfMessage savee)
        {
            try
            {
                await dbcontext.AddAsync<TBTypesOfMessage>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdTypesOfMessage)
        {
            try
            {
                var message = await GetByIdAsync(IdTypesOfMessage);
                message.CurrentState = false;
                dbcontext.Entry(message).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBTypesOfMessage update)
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
