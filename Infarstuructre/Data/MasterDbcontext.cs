﻿using Domin.Entity;

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

    }
}
