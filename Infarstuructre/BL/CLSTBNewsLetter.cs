using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IINewsLetters
    {
        List<TBViewNewsLetter> GetAll();
        TBNewsletter GetById(int IdNewsletterGroup);
        bool saveData(TBNewsletter savee);
        bool UpdateData(TBNewsletter updatss);
        bool DeleteData(int IdNewsletterGroup);
        List<TBViewNewsLetter> GetAllv(int IdNewsletterGroup);

        // /////////////////////////API////////////////////////////////////
        Task<List<TBViewNewsLetter>> GetAllAsync();
        Task<List<TBViewNewsLetter>> GetAllvAsync(int id);
        Task<TBNewsletter> GetByIdAsync(int id);
        Task<bool> AddDataAsync(TBNewsletter savee);
        Task<bool> UpdateDataAsync(TBNewsletter updatee);
        Task<bool> DeleteDataAsync(int id);
    }
    public class CLSTBNewsLetter : IINewsLetters
    {
        MasterDbcontext dbcontext;
        public CLSTBNewsLetter(MasterDbcontext dbcontext1)
        {
            dbcontext = dbcontext1;
        }

        public List<TBViewNewsLetter> GetAll()
        {
            List<TBViewNewsLetter> MySlider = dbcontext.ViewNewsLetter.OrderByDescending(n => n.IdNewsletter).ToList();
            return MySlider;
        }
        public TBNewsletter GetById(int IdNewsletter)
        {
            TBNewsletter sslid = dbcontext.TBNewsletters.FirstOrDefault(a => a.IdNewsletter == IdNewsletter);
            return sslid;
        }
        public bool saveData(TBNewsletter savee)
        {
            try
            {
                dbcontext.Add<TBNewsletter>(savee);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateData(TBNewsletter updatss)
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
        public bool DeleteData(int IdNewsletter)
        {
            try
            {
                var catr = GetById(IdNewsletter);
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
        public List<TBViewNewsLetter> GetAllv(int IdNewsletter)
        {
            List<TBViewNewsLetter> MySlider = dbcontext.ViewNewsLetter.OrderByDescending(n => n.IdNewsletter == IdNewsletter).Where(a => a.IdNewsletterGroup == IdNewsletter).ToList();
            return MySlider;
        }

        // /////////////////////////////////////////////////API//////////////////////////////////////////////////////////
        public async Task<List<TBViewNewsLetter>> GetAllAsync()
        {
            List<TBViewNewsLetter> MySlider = await dbcontext.ViewNewsLetter.OrderByDescending(n => n.IdNewsletter).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<List<TBViewNewsLetter>> GetAllvAsync(int id)
        {
            List<TBViewNewsLetter> MySlider = await dbcontext.ViewNewsLetter.OrderByDescending(n => n.IdNewsletter == id).Where(a => a.IdNewsletter == id).Where(a => a.CurrentState == true).ToListAsync();
            return MySlider;
        }

        public async Task<TBNewsletter> GetByIdAsync(int id)
        {
            TBNewsletter sslid = await dbcontext.TBNewsletters.FirstOrDefaultAsync(a => a.IdNewsletter == id);
            return sslid;
        }

        public async Task<bool> AddDataAsync(TBNewsletter savee)
        {
            try
            {
                await dbcontext.AddAsync<TBNewsletter>(savee);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDataAsync(TBNewsletter updatee)
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
