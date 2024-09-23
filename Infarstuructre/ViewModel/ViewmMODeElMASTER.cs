using Domin.Entity;
using Domin.Entity.SignalR;
using Domin.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
	public class ViewmMODeElMASTER
	{
		public returnUrl returnUrl { get; set; }
        public IEnumerable<IdentityRole> ListIdentityRole { get; set; }
        public IEnumerable<prodactlist> Listprodatt { get; set; }
        public IdentityRole? sIdentityRole { get; set; } 
        public IEnumerable<VwUser> ListVwUser { get; set; }
        public IEnumerable<ApplicationUser> ListlicationUser { get; set; }
        public VwUser sVwUser { get; set; }
        public ApplicationUser sUser { get; set; }
        public RegisterViewModel ruser { get; set; }
        public NewRegister SNewRegister { get; set; }
		public IEnumerable<RegisterViewModel> ListRegisterViewModel { get; set; }
		public IEnumerable<NewRegister> ListNewRegister { get; set; }
		public ChangePasswordViewModel SChangePassword { get; set; }

        public bool Rememberme { get; set; }
        public List<SelectListItem> Roles1 { get; set; }
        public string SelectedRoleId { get; set; }
        public string ProductName { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string UPC { get; set; }
        public int Quantity { get; set; }
        public string RetailPrice { get; set; }
        public string TotalRetailPrice { get; set; }
        public string UserName { get; set; }
		public string UserId { get; set; }
		public string UserImage { get; set; }
		public string Name { get; set; }
		public string UserRole { get; set; }
		public NewRegister NewRegister { get; set; }
		public string Id { get; set; }
		public string RoleName { get; set; }
		public string Email { get; set; }
		public string? ImageUser { get; set; }
		public bool ActiveUser { get; set; }
		public string Password { get; set; }
		public string ComparePassword { get; set; }
		public string username1 { get; set; }
		public string PhoneNumber { get; set; }	
		
		//public string userName { get; set; }
		
		public List<IdentityRole> Roles { get; set; }
		public List<VwUser> Users { get; set; }
        public IEnumerable<TBViewFAQDescription> ListFAQDescription { get; set; }
        public TBFAQDescreption FAQDescreption { get; set; }
        public IEnumerable<TBViewFAQList> ListFAQList { get; set; }
        public TBFAQList FAQList { get; set; }
        public IEnumerable<TBFAQ> ListFAQ { get; set; }
        public TBFAQ FAQ { get; set; }
        public IEnumerable<TBTypesOfMessage> ListTypesOfMessage { get; set; }
        public TBTypesOfMessage TypesOfMessage { get; set; }
        public IEnumerable<TBViewCustomerMessages> ListViewCustomerMessages { get; set; }
        public TBCustomerMessages CustomerMessages { get; set; }
        public IEnumerable<TBEmailAlartSetting> ListEmailAlartSetting { get; set; }
        public TBEmailAlartSetting EmailAlartSetting { get; set; }
        public TBWareHouse WareHouse { get; set; }
        public IEnumerable<TBViewWareHouse> ViewWareHouse { get; set; }
        public IEnumerable<TBWareHouseType> ViewWareHouseType { get; set; }
        public TBWareHouseType WareHouseType { get; set; }
        public TBProductCategory ProductCategory { get; set; }
        public IEnumerable<TBViewProductCategory> ViewProductCategory { get; set; }
        public IEnumerable<TBViewMerchants> listViewMerchants { get; set; }
        public TBMerchants Merchants { get; set; }   
        public IEnumerable<TBTypesProduct> ListTypesProduct { get; set; }
        public TBTypesProduct TypesProduct { get; set; }
        public TBWareHouseBranch WareHouseBranch { get; set; }
        public IEnumerable<TBViewWareHouseBranch> ViewWareHouseBranch { get; set; }
        public IEnumerable<TBViewProductInformation> ListViewProductInformation { get; set; }
        public TBProductInformation ProductInformation { get; set; }  
        public IEnumerable<TBBondType> ListBondType { get; set; }
        public TBBondType BondType { get; set; }


        public IEnumerable<TBViewOrder> ListViewOrder { get; set; }
        public TBOrder Order { get; set; }
  
        public IEnumerable<TBConnectAndDisConnect> ConnectAndDisConnect { get; set; }
        public TBMessageChat TBMessageChat { get; set; }
        public IEnumerable<TBViewChatMessage> ViewChatMessage { get; set; }
        public IEnumerable<TBSupportTicketType> ListSupportTicketType { get; set; }
        public TBSupportTicketType SupportTicketType { get; set; }
        public IEnumerable<TBSupportTicketStatus> ListSupportTicketStatus { get; set; }
        public TBSupportTicketStatus SupportTicketStatus { get; set; }
        public IEnumerable<TBViewSupportTicket> ListViewSupportTicket { get; set; }
        public TBSupportTicket SupportTicket { get; set; }


        public IEnumerable<TBViewNewsLetterGroup> ListNewsLetterGroup { get; set; }
        public TBNewsletterGroup NewsletterGroup { get; set; }
        public IEnumerable<TBViewNewsLetter> ListNewsLetter { get; set; }
        public TBNewsletter Newsletter { get; set; }
        public IEnumerable<TBTemplate> ListTemplate { get; set; }
        public TBTemplate Template { get; set; } 
        public IEnumerable<TBCompanyInformation> ListCompanyInformatione { get; set; }
        public TBCompanyInformation CompanyInformation { get; set; }  
        public IEnumerable<TBBrandName> ListBrandName { get; set; }
        public TBBrandName BrandName { get; set; }


    }
}

