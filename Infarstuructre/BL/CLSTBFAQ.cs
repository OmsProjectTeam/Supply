using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{

	public interface IIFAQ
	{
		List<TBFAQ> GetAll();
		TBFAQ GetById(int IdFAQ);
		bool saveData(TBFAQ savee);
		bool UpdateData(TBFAQ updatss);
		bool deleteData(int IdFAQ);
		List<TBFAQ> GetAllv(int IdFAQ);
		List<TBFAQ> GetAllActive();
        //////////////////////////APIs/////////////////////////////////////////////////////////////////
        Task<List<TBFAQ>> GetAllAsync();
        Task<List<TBFAQ>> GetAllvAsync(int IdCustomerMessages);
        Task<List<TBFAQ>> GetAllDataentryAsync(string dataEntry);
        Task<TBFAQ> GetByIdAsync(int IdCustomerMessages);
        Task<List<TBFAQ>> GetAllActiveAsync();
        Task<bool> AddDataAsync(TBFAQ savee);
        Task<bool> DeleteDataAsync(int TBFAQ);
        Task<bool> UpdateDataAsync(TBFAQ update);
    }

	public class CLSTBFAQ : IIFAQ
	{
		MasterDbcontext dbcontext;
		public CLSTBFAQ(MasterDbcontext dbcontext1)
        {
			dbcontext	= dbcontext1;

		}
		public List<TBFAQ> GetAll()
		{
			List<TBFAQ> MySlider = dbcontext.TBFAQs.Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public List<TBFAQ> GetAllActive()
		{
			List<TBFAQ> MySlider = dbcontext.TBFAQs.OrderByDescending(n => n.IdFAQ).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
			return MySlider;
		}
		public TBFAQ GetById(int IdFAQ)
		{
			TBFAQ sslid = dbcontext.TBFAQs.FirstOrDefault(a => a.IdFAQ == IdFAQ);
			return sslid;
		}
		public bool saveData(TBFAQ savee)
		{
			try
			{
				dbcontext.Add<TBFAQ>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBFAQ updatss)
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
		public bool deleteData(int IdFAQ)
		{
			try
			{
				var catr = GetById(IdFAQ);
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
		public List<TBFAQ> GetAllv(int IdFAQ)
		{
			List<TBFAQ> MySlider = dbcontext.TBFAQs.OrderByDescending(n => n.IdFAQ == IdFAQ).Where(a => a.IdFAQ == IdFAQ).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}

        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBFAQ>> GetAllAsync()
        {
            var myDatd = await dbcontext.TBFAQs.OrderByDescending(n => n.IdFAQ).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBFAQ>> GetAllvAsync(int IdFAQ)
        {
            var myDatd = await dbcontext.TBFAQs.OrderByDescending(n => n.IdFAQ).Where(a => a.IdFAQ == IdFAQ).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBFAQ>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.TBFAQs.Where(a => a.DateEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBFAQ>> GetAllActiveAsync()
        {
            List<TBFAQ> MySlider = await dbcontext.TBFAQs.OrderByDescending(n => n.IdFAQ).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBFAQ> GetByIdAsync(int IdFAQ)
        {
            var sslid = await dbcontext.TBFAQs.FirstOrDefaultAsync(a => a.IdFAQ == IdFAQ);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBFAQ savee)
        {
            try
            {
                await dbcontext.AddAsync<TBFAQ>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdFAQ)
        {
            try
            {
                var email = await GetByIdAsync(IdFAQ);
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

        public async Task<bool> UpdateDataAsync(TBFAQ update)
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
