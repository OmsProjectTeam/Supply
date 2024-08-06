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
			List<TBViewFAQList> MySlIdFAQLister = dbcontext.ViewFAQList.OrderByDescending(n => n.IdFAQ == IdFAQList).Where(a => a.IdFAQ == IdFAQList).Where(a => a.CurrentState == true).ToList();
			return MySlIdFAQLister;
		}
	}
}
