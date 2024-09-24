using Domin.Entity;
using Domin.Entity.SignalR;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Infarstuructre.Data
{
	public class MasterDbcontext : IdentityDbContext<ApplicationUser>
	{
		public MasterDbcontext(DbContextOptions<MasterDbcontext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            //***********************************************************


			builder.Entity<VwUser>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("VwUsers");
			});

            //***********************************************************
            builder.Entity<TBViewFAQList>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewFAQList");
            });
            //************************************************************
            builder.Entity<TBViewFAQDescription>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewFAQDescription");
            });
            //************************************************************

            //************************************************************
            builder.Entity<TBViewCustomerMessages>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewCustomerMessages");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewWareHouseType>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewWareHouseType");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewProductCategory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewProductCategory");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewWareHouse>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewWareHouse");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewWareHouseBranch>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewWareHouseBranch");
            });
            //************************************************************
             //************************************************************
            builder.Entity<TBViewProductInformation>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewProductInformation");
            });
            //************************************************************  
            //************************************************************
            builder.Entity<TBViewMerchants>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewMerchants");
            });
            //************************************************************
              //************************************************************
            builder.Entity<TBViewOrder>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewOrder");
            });
            //************************************************************
            //************************************************************
            //************************************************************
            builder.Entity<TBViewChatMessage>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewChat");
            }); 
            //************************************************************
            builder.Entity<TBViewSupportTicket>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewSupportTicket");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewNewsLetter>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewNewsLetter");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewSendLog>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewSendLog");
            });
            //************************************************************
            //************************************************************
            builder.Entity<TBViewNewsLetterGroup>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewNewsLetterGroup");
            });
            //************************************************************
            //---------------------------------
            builder.Entity<TBFAQ>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBFAQ>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBFAQ>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBFAQDescreption>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBFAQDescreption>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBFAQList>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBFAQList>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBCustomerMessages>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCustomerMessages>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBTypesOfMessage>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTypesOfMessage>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBTypesOfMessage>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------	
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBEmailAlartSetting>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------	
            builder.Entity<TBWareHouseType>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBWareHouseType>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBWareHouseType>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------	
            builder.Entity<TBProductCategory>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBProductCategory>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBProductCategory>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------	
            builder.Entity<TBWareHouse>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBWareHouse>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBWareHouse>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------
            //---------------------------------
            builder.Entity<TBMerchants>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBMerchants>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
            //---------------------------------
            //---------------------------------	
            builder.Entity<TBWareHouseBranch>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBWareHouseBranch>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBWareHouseBranch>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");

            //---------------------------------
            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBTypesProduct>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTypesProduct>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //---------------------------------   
            //---------------------------------
            builder.Entity<TBProductInformation>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBProductInformation>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBProductInformation>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------  
            //---------------------------------
            builder.Entity<TBBondType>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBBondType>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBBondType>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");
            //---------------------------------     
            //---------------------------------
            //--------------------------------- 

            builder.Entity<TBMessageChat>()
           .Property(m => m.MessageeTime)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBMessageChat>()
           .Property(m => m.CurrentState)
           .HasDefaultValueSql("((1))");
            builder.Entity<TBMessageChat>()
           .Property(m => m.IsRead)
           .HasDefaultValueSql("((0))");

            //---------------------------------
            //--------------------------------- 

            builder.Entity<TBConnectAndDisConnect>()
           .Property(m => m.TimeConnection)
           .HasDefaultValueSql("getdate()");
            //---------------------------------
             //---------------------------------
            builder.Entity<TBOrder>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBOrder>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
      
            //---------------------------------     
              //---------------------------------
            builder.Entity<TBSupportTicketType>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBSupportTicketType>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
      
            //-   
            //---------------------------------
            builder.Entity<TBSupportTicketStatus>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBSupportTicketStatus>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");
      
            //---------------------------------     
             //---------------------------------
            builder.Entity<TBSupportTicket>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBSupportTicket>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //---------------------------------
            
            //---------------------------------
            builder.Entity<TBNewsletterGroup>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBNewsletterGroup>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBNewsletter>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBNewsletter>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBTemplate>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBTemplate>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBSendLog>()
           .Property(b => b.SentDate)
           .HasDefaultValueSql("getdate()");

            //--------------------------------- 
            //---------------------------------
            builder.Entity<TBCompanyInformation>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBCompanyInformation>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))");

            //---------------------------------    
            //---------------------------------
            builder.Entity<TBBrandName>()
           .Property(b => b.DateTimeEntry)
           .HasDefaultValueSql("getdate()");
            builder.Entity<TBBrandName>()
           .Property(b => b.CurrentState)
           .HasDefaultValueSql("((1))"); 
            builder.Entity<TBBrandName>()
           .Property(b => b.Active)
           .HasDefaultValueSql("((1))");

            //--------------------------------- 
        }
        //***********************************
        public DbSet<VwUser> VwUsers { get; set; }
        public DbSet<TBFAQ> TBFAQs { get; set; }
        public DbSet<TBFAQDescreption> TBFAQDescreptions { get; set; }
        public DbSet<TBFAQList> TBFAQLists { get; set; }
        public DbSet<TBViewFAQDescription> ViewFAQDescription { get; set; }
        public DbSet<TBViewFAQList> ViewFAQList { get; set; }
        public DbSet<TBCustomerMessages> TBCustomerMessagess { get; set; }
        public DbSet<TBViewCustomerMessages> ViewCustomerMessages { get; set; }
        public DbSet<TBTypesOfMessage> TBTypesOfMessages { get; set; }
        public DbSet<TBEmailAlartSetting> TBEmailAlartSettings { get; set; }
        public DbSet<TBWareHouseType> TBWareHouseTypes { get; set; }
        public DbSet<TBProductCategory> TBProductCategorys { get; set; }
        public DbSet<TBWareHouse> TBWareHouses { get; set; }
        public DbSet<TBViewWareHouse> ViewWareHouse { get; set; }
        public DbSet<TBViewProductCategory> ViewProductCategory { get; set; }
        public DbSet<TBViewWareHouseType> ViewWareHouseType { get; set; }
        public DbSet<TBMerchants> TBMerchantss { get; set; }
        public DbSet<TBWareHouseBranch> TBWareHouseBranchs { get; set; }
        public DbSet<TBViewWareHouseBranch> ViewWareHouseBranch { get; set; }
        public DbSet<TBTypesProduct> TBTypesProducts { get; set; }
        public DbSet<TBProductInformation> TBProductInformations { get; set; }
        public DbSet<TBViewProductInformation> ViewProductInformation { get; set; }
        public DbSet<TBViewMerchants> ViewMerchants { get; set; }
        public DbSet<TBBondType> TBBondTypes { get; set; }
        public DbSet<TBOrder> TBOrders { get; set; }
        public DbSet<TBViewOrder> ViewOrder { get; set; }
        public virtual DbSet<TBMessageChat> TBMessageChats { get; set; }
        public virtual DbSet<TBViewChatMessage> ViewChatMessage { get; set; }
        public DbSet<TBConnectAndDisConnect> TBConnectAndDisConnects { get; set; }
        public DbSet<TBSupportTicketType> TBSupportTicketTypes { get; set; }
        public DbSet<TBSupportTicketStatus> TBSupportTicketStatuss { get; set; }
        public DbSet<TBSupportTicket> TBSupportTickets { get; set; }
        public DbSet<TBViewSupportTicket> ViewSupportTicket { get; set; }
        public DbSet<TBTemplate> TBTemplates { get; set; }
        public DbSet<TBNewsletter> TBNewsletters { get; set; }
        public DbSet<TBSendLog> TBSendLogs { get; set; }
        public DbSet<TBNewsletterGroup> TBNewsletterGroups { get; set; }
        public DbSet<TBViewNewsLetter> ViewNewsLetter { get; set; }
        public DbSet<TBViewNewsLetterGroup> ViewNewsLetterGroup { get; set; }
        public DbSet<TBViewSendLog> ViewSendLog { get; set; } 
        public DbSet<TBCompanyInformation> TBCompanyInformations { get; set; }
        public DbSet<TBBrandName> TBBrandNames { get; set; }
     

    }
}
