

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
            List<TBViewOrder> MySlider = dbcontext.ViewOrder.Where(a => a.CurrentState == true).ToList();
            return MySlider;
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

    }
}
