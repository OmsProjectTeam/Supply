using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
	public interface IIFAQList 
	{
		List<TBViewFAQList> GetAll();
		TBFAQList GetByIdFAQList(int IdFAQList);
		bool saveData(TBFAQList savee);
		bool UpdateData(TBFAQList updatss);
		bool deleteData(int IdFAQList);
		List<TBViewFAQList> GetAllv(int IdFAQList);
        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBViewFAQList>> GetAllAsync();
        Task<List<TBViewFAQList>> GetAllActiveAsync();
        Task<TBFAQList> GetByIdAsync(int IdFAQList);
        Task<bool> AddDataAsync(TBFAQList savee);
        Task<bool> DeleteDataAsync(int IdFAQList);
        Task<bool> UpdateDataAsync(TBFAQList update);
    }


	public class CLSTBFAQList : IIFAQList
	{
        MasterDbcontext dbcontext;
		public CLSTBFAQList(MasterDbcontext dbcontext1)
		{
			dbcontext = dbcontext1;
		}

		public List<TBViewFAQList> GetAll()
		{
			List<TBViewFAQList> MySlIdFAQLister = dbcontext.ViewFAQList.OrderByDescending(n => n.IdFAQList).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
			return MySlIdFAQLister;
		}
		public TBFAQList GetByIdFAQList(int IdFAQList)
		{
			TBFAQList sslIdFAQList = dbcontext.TBFAQLists.FirstOrDefault(a => a.IdFAQList == IdFAQList);
			return sslIdFAQList;
		}
		public bool saveData(TBFAQList savee)
		{
			try
			{
				dbcontext.Add<TBFAQList>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBFAQList updatss)
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
		public bool deleteData(int IdFAQList)
		{
			try
			{
				var catr = GetByIdFAQList(IdFAQList);
				catr.CurrentState = false;
				//TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdFAQListBrand == IdFAQListBrand).FirstOrDefault();
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
		public List<TBViewFAQList> GetAllv(int IdFAQList)
		{
			List<TBViewFAQList> MySlIdFAQLister = dbcontext.ViewFAQList.OrderByDescending(n => n.IdFAQList == IdFAQList).Where(a => a.IdFAQList == IdFAQList).Where(a => a.CurrentState == true).ToList();
			return MySlIdFAQLister;
		}

        //// ///////////////////APIs////////////////////////////////////////////////////////////////

        public async Task<List<TBViewFAQList>> GetAllAsync()
        {
            var myDatd = await dbcontext.ViewFAQList.OrderByDescending(n => n.IdFAQList).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBViewFAQList>> GetAllActiveAsync()
        {
            var MySlider = await dbcontext.ViewFAQList.OrderByDescending(n => n.IdFAQList).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBFAQList> GetByIdAsync(int IdFAQList)
        {
            var sslid = await dbcontext.TBFAQLists.FirstOrDefaultAsync(a => a.IdFAQList == IdFAQList);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBFAQList savee)
        {
            try
            {
                await dbcontext.AddAsync<TBFAQList>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdFAQList)
        {
            try
            {
                var fakDescription = await GetByIdAsync(IdFAQList);
                fakDescription.CurrentState = false;
                dbcontext.Entry(fakDescription).State = EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDataAsync(TBFAQList update)
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
