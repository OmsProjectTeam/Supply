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


	}
}
