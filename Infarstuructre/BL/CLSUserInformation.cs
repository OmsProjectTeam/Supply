using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.BL
{
    public interface IIUserInformation
    {
        List<VwUser> GetAll();
        ApplicationUser GetById(string? Id);
        List<ApplicationUser> GetAllByName(string name);
        List<VwUser> GetAllbyId(string userId);
        List<ApplicationUser> GetAllByNameall();
        ApplicationUser GetByName(string name);
        List<VwUser> GetAllbyRole();
        List<VwUser> GetActiveSupport();
	public interface IIUserInformation
	{
		List<VwUser> GetAll();
		ApplicationUser GetById(string? Id);
		List<ApplicationUser> GetAllByName(string name);
		List<VwUser> GetAllbyId(string userId);
		List<ApplicationUser> GetAllByNameall();
        List<ApplicationUser> GetAllByRole(string role);



    }
    public class CLSUserInformation : IIUserInformation
    {
        UserManager<ApplicationUser> _userManager;
        MasterDbcontext dbcontext;

        public CLSUserInformation(UserManager<ApplicationUser> userManager, MasterDbcontext dbcontext1)
        {
            _userManager = userManager;
            dbcontext = dbcontext1;

        }
        public List<VwUser> GetAll()

        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            //List<VwUser> MySlider = dbcontext.VwUsers.OrderBy(x => x.Role).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
            List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(n => n.ActiveUser == true).ToList();
            return MySlider;
        }


        public List<VwUser> GetAllbyId(string userId)

        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            List<VwUser> MySlider = dbcontext.VwUsers.Where(x => x.Id == userId).Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                            //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }

        public List<VwUser> GetAllbyRole()

        {
            List<VwUser> MySlider = dbcontext.VwUsers.Where(x => x.Role == "Admin").ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                              //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }

        public List<VwUser> GetActiveSupport()

        {
            List<VwUser> MySlider = dbcontext.VwUsers.Where(x => x.Role == "Support").Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                 //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }

        public List<ApplicationUser> GetAllByName(string name)

        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            List<ApplicationUser> MySlider = _userManager.Users.Where(x => x.Email == name).Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                       //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }
        public List<ApplicationUser> GetAllByNameall()

        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            List<ApplicationUser> MySlider = _userManager.Users.OrderByDescending(x => x.Id).Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                        //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }

        public ApplicationUser GetById(string? Id)
        {
            ApplicationUser sslid = _userManager.Users.FirstOrDefault(a => a.Id == Id);
            return sslid;
        }

        public ApplicationUser GetByName(string name)
        {
            ApplicationUser MySlider = _userManager.Users.Where(x => x.UserName == name).Where(n => n.ActiveUser == true).FirstOrDefault(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                            //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }
    }
}
