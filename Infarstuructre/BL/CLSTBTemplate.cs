using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IITemplate
    {
        List<TBTemplate> GetAll();
        TBTemplate GetById(int IdTemplate);
        bool saveData(TBTemplate savee);
        bool UpdateData(TBTemplate updatss);
        bool DeleteData(int IdTemplate);
        List<TBTemplate> GetAllv(int IdTemplate);
        // /////////////////////////API////////////////////////////////////
        Task<List<TBTemplate>> GetAllAsync();
        Task<List<TBTemplate>> GetAllvAsync(int id);
        Task<TBTemplate> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBTemplate savee);
        Task<bool> UpdateDataAsync(TBTemplate updatee);
        Task<bool> DeleteDataAsync(int id);
    }
    public class CLSTBTemplate : IITemplate
    {
        MasterDbcontext dbcontext;
        public CLSTBTemplate(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBTemplate> GetAll()
        {
            List<TBTemplate> MySlider = dbcontext.TBTemplates.OrderByDescending(n => n.IdTemplate).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBTemplate GetById(int IdTemplate)
        {
            TBTemplate sslid = dbcontext.TBTemplates.FirstOrDefault(a => a.IdTemplate == IdTemplate);
            return sslid;
        }
        public bool saveData(TBTemplate savee)
        {
            try
            {
                dbcontext.Add<TBTemplate>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBTemplate updatss)
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
        public bool DeleteData(int IdTemplate)
        {
            try
            {
                var catr = GetById(IdTemplate);
                catr.CurrentState = false;
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<TBTemplate> GetAllv(int IdTemplate)
        {
            List<TBTemplate> MySlider = dbcontext.TBTemplates.OrderByDescending(n => n.IdTemplate == IdTemplate).Where(a => a.IdTemplate == IdTemplate).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        // /////////////////////////////////////////////////API//////////////////////////////////////////////////////////
        public async Task<List<TBTemplate>> GetAllAsync()
        {
            List<TBTemplate> MySlider = await dbcontext.TBTemplates.OrderByDescending(n => n.IdTemplate).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBTemplate>> GetAllvAsync(int id)
        {
            List<TBTemplate> MySlider = await dbcontext.TBTemplates.OrderByDescending(n => n.IdTemplate == id).Where(a => a.IdTemplate == id).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBTemplate> GetByIdAsync(int id)
        {
            TBTemplate sslid = await dbcontext.TBTemplates.FirstOrDefaultAsync(a => a.IdTemplate == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBTemplate savee)
        {
            try
            {
                await dbcontext.AddAsync<TBTemplate>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBTemplate updatee)
        {
            try
            {
                dbcontext.Entry(updatee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDataAsync(int id)
        {
            try
            {
                var catr = await GetByIdAsync(id);
                catr.CurrentState = false;
                //TbSubCateegoory dele = dbcontex.TbSubCateegoorys.Where(a => a.IdBrand == IdBrand).FirstOrDefault();
                //dbcontex.TbSubCateegoorys.Remove(dele);
                dbcontext.Entry(catr).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
