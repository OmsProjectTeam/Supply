using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{

	public interface IIFAQDescreption
	{
		List<TBViewFAQDescription> GetAll();
		TBFAQDescreption GetByIdFAQDescreption(int IdFAQDescreption);
		bool saveData(TBFAQDescreption savee);
		bool deleteData(int IdFAQDescreption);
		List<TBViewFAQDescription> GetAllv(int IdFAQDescreption);
		bool UpdateData(TBFAQDescreption updatss);
        // /////////////APIs////////////////////////////////////////////////
        Task<List<TBViewFAQDescription>> GetAllAsync();
        Task<List<TBViewFAQDescription>> GetAllvAsync(int IdFAQDescreption);
        Task<List<TBViewFAQDescription>> GetAllDataentryAsync(string dataEntry);
        Task<List<TBViewFAQDescription>> GetAllActiveAsync();
        Task<TBFAQDescreption> GetByIdAsync(int IdFAQDescreption);
        Task<bool> AddDataAsync(TBFAQDescreption savee);
        Task<bool> DeleteDataAsync(int IdFAQDescreption);
        Task<bool> UpdateDataAsync(TBFAQDescreption update);

    }

	public class CLSTBFAQDescreption : IIFAQDescreption
	{
		MasterDbcontext dbcontext;
		public CLSTBFAQDescreption(MasterDbcontext dbcontext1)
        {
			dbcontext = dbcontext1;

		}

		public List<TBViewFAQDescription> GetAll()
		{
			List<TBViewFAQDescription> MySlIdFAQDescreptioner = dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQDescreption).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToList();
			return MySlIdFAQDescreptioner;
		}
		public TBFAQDescreption GetByIdFAQDescreption(int IdFAQDescreption)
		{
			TBFAQDescreption sslIdFAQDescreption = dbcontext.TBFAQDescreptions.FirstOrDefault(a => a.IdFAQDescreption == IdFAQDescreption);
			return sslIdFAQDescreption;
		}
		public bool saveData(TBFAQDescreption savee)
		{
			try
			{
				dbcontext.Add<TBFAQDescreption>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBFAQDescreption updatss)
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
		public bool deleteData(int IdFAQDescreption)
		{
			try
			{
				var catr = GetByIdFAQDescreption(IdFAQDescreption);
				catr.CurrentState = false;
				//TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdFAQDescreptionBrand == IdFAQDescreptionBrand).FirstOrDefault();
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
		public List<TBViewFAQDescription> GetAllv(int IdFAQDescreption)
		{
			List<TBViewFAQDescription> MySlIdFAQDescreptioner = dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQDescreption == IdFAQDescreption).Where(a => a.IdFAQDescreption == IdFAQDescreption).Where(a => a.CurrentState == true).ToList();
			return MySlIdFAQDescreptioner;
		}

        // //////////////////////////APIs/////////////////////////////////////////////////////////////////

        public async Task<List<TBViewFAQDescription>> GetAllAsync()
        {
            var myDatd = await dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQDescreption).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBViewFAQDescription>> GetAllvAsync(int IdFAQDescreption)
        {
            var myDatd = await dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQDescreption).Where(a => a.IdFAQDescreption == IdFAQDescreption).Where(a => a.CurrentState == true).ToListAsync();
            return myDatd;
        }

        public async Task<List<TBViewFAQDescription>> GetAllDataentryAsync(string dataEntry)
        {
            var MySlider = await dbcontext.ViewFAQDescription.Where(a => a.DateEntry == dataEntry && a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBViewFAQDescription>> GetAllActiveAsync()
        {
            var MySlider = await dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQDescreption).Where(a => a.CurrentState == true).Where(a => a.Active == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBFAQDescreption> GetByIdAsync(int IdFAQDescreption)
        {
            var sslid = await dbcontext.TBFAQDescreptions.FirstOrDefaultAsync(a => a.IdFAQDescreption == IdFAQDescreption);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBFAQDescreption savee)
        {
            try
            {
                await dbcontext.AddAsync<TBFAQDescreption>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteDataAsync(int IdFAQDescreption)
        {
            try
            {
                var fakDescription = await GetByIdAsync(IdFAQDescreption);
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

        public async Task<bool> UpdateDataAsync(TBFAQDescreption update)
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
