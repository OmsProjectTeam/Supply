using Domin.Entity;
using Domin.Entity.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infarstuructre.BL
{
    public interface IINewsLettersGroup
    {
        List<TBViewNewsLetterGroup> GetAll();
        TBNewsletterGroup GetById(int IdNewsletterGroup);
        bool saveData(TBNewsletterGroup savee);
        bool UpdateData(TBNewsletterGroup updatss);
        bool DeleteData(int IdNewsletterGroup);
        List<TBViewNewsLetterGroup> GetAllv(int IdNewsletterGroup);

        // ////////////////////////API////////////////////////////////////

        Task<List<TBViewNewsLetterGroup>> GetAllAsync();
        Task<List<TBViewNewsLetterGroup>> GetAllvAsync(int id);
        Task<TBNewsletterGroup> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBNewsletterGroup savee);
        Task<bool> UpdateDataAsync(TBNewsletterGroup updatee);
        Task<bool> DeleteDataAsync(int id);

    }
    public class CLSTBNewsLettersGroup : IINewsLettersGroup
    {
        MasterDbcontext dbcontext;
        public CLSTBNewsLettersGroup(MasterDbcontext dbcontext1) 
        {
            dbcontext = dbcontext1;
        }

        public List<TBViewNewsLetterGroup> GetAll()
        {
            List<TBViewNewsLetterGroup> MySlider = dbcontext.ViewNewsLetterGroup.OrderByDescending(n => n.IdNewsletterGroup).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }
        public TBNewsletterGroup GetById(int IdNewsletterGroup)
        {
            TBNewsletterGroup sslid = dbcontext.TBNewsletterGroups.FirstOrDefault(a => a.IdNewsletterGroup == IdNewsletterGroup);
            return sslid;
        }
        public bool saveData(TBNewsletterGroup savee)
        {
            try
            {
                dbcontext.Add<TBNewsletterGroup>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBNewsletterGroup updatss)
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
        public bool DeleteData(int IdNewsletterGroup)
        {
            try
            {
                var catr = GetById(IdNewsletterGroup);
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
        public List<TBViewNewsLetterGroup> GetAllv(int IdNewsletterGroup)
        {
            List<TBViewNewsLetterGroup> MySlider = dbcontext.ViewNewsLetterGroup.OrderByDescending(n => n.IdNewsletterGroup == IdNewsletterGroup).Where(a => a.IdNewsletterGroup == IdNewsletterGroup).Where(a => a.CurrentState == true).ToList();
            return MySlider;
        }

        // /////////////////////////////////////////////////API//////////////////////////////////////////////////////////
        public async Task<List<TBViewNewsLetterGroup>> GetAllAsync()
        {
            List<TBViewNewsLetterGroup> MySlider = await dbcontext.ViewNewsLetterGroup.OrderByDescending(n => n.IdNewsletterGroup).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBViewNewsLetterGroup>> GetAllvAsync(int id)
        {
            List<TBViewNewsLetterGroup> MySlider = await dbcontext.ViewNewsLetterGroup.OrderByDescending(n => n.IdNewsletterGroup == id).Where(a => a.IdNewsletterGroup == id).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBNewsletterGroup> GetByIdAsync(int id)
        {
            TBNewsletterGroup sslid = await dbcontext.TBNewsletterGroups.FirstOrDefaultAsync(a => a.IdNewsletterGroup == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBNewsletterGroup savee)
        {
            try
            {
                await dbcontext.AddAsync<TBNewsletterGroup>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBNewsletterGroup updatee)
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
