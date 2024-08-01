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
			List<TBViewFAQDescription> MySlIdFAQDescreptioner = dbcontext.ViewFAQDescription.OrderByDescending(n => n.IdFAQ == IdFAQDescreption).Where(a => a.IdFAQ == IdFAQDescreption).Where(a => a.CurrentState == true).ToList();
			return MySlIdFAQDescreptioner;
		}


	}
}
