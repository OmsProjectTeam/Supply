

namespace Infarstuructre.BL
{
	public interface IIEmailAlartSetting
	{
		List<TBEmailAlartSetting> GetAll();
		TBEmailAlartSetting GetById(int IdEmailAlartSetting);
		bool saveData(TBEmailAlartSetting savee);
		bool UpdateData(TBEmailAlartSetting updatss);
		bool deleteData(int IdEmailAlartSetting);
		List<TBEmailAlartSetting> GetAllv(int IdEmailAlartSetting);
	}
	public class CLSTBEmailAlartSetting: IIEmailAlartSetting
	{
		MasterDbcontext dbcontext;
		public CLSTBEmailAlartSetting(MasterDbcontext dbcontext1)
        {
			dbcontext=dbcontext1;

		}
		public List<TBEmailAlartSetting> GetAll()
		{
			List<TBEmailAlartSetting> MySlider = dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
		public TBEmailAlartSetting GetById(int IdEmailAlartSetting)
		{
			TBEmailAlartSetting sslid = dbcontext.TBEmailAlartSettings.FirstOrDefault(a => a.IdEmailAlartSetting == IdEmailAlartSetting);
			return sslid;
		}
		public bool saveData(TBEmailAlartSetting savee)
		{
			try
			{
				dbcontext.Add<TBEmailAlartSetting>(savee);
				dbcontext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool UpdateData(TBEmailAlartSetting updatss)
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
		public bool deleteData(int IdEmailAlartSetting)
		{
			try
			{
				var catr = GetById(IdEmailAlartSetting);
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
		public List<TBEmailAlartSetting> GetAllv(int IdEmailAlartSetting)
		{
			List<TBEmailAlartSetting> MySlider = dbcontext.TBEmailAlartSettings.OrderByDescending(n => n.IdEmailAlartSetting == IdEmailAlartSetting).Where(a => a.IdEmailAlartSetting == IdEmailAlartSetting).Where(a => a.CurrentState == true).ToList();
			return MySlider;
		}
	}
}
