using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CPDMS.Entities;

public partial class AdminDcpcContext : DbContext
{
    public AdminDcpcContext()
    {
    }

    public AdminDcpcContext(DbContextOptions<AdminDcpcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MBlock> MBlocks { get; set; }

    public virtual DbSet<MChemical> MChemicals { get; set; }

    public virtual DbSet<MChemicalProduct> MChemicalProducts { get; set; }

    public virtual DbSet<MDistrict> MDistricts { get; set; }

    public virtual DbSet<MPchemical> MPchemicals { get; set; }

    public virtual DbSet<MPchemicalProduct> MPchemicalProducts { get; set; }

    public virtual DbSet<MState> MStates { get; set; }

    public virtual DbSet<TRegistration> TRegistrations { get; set; }

    public virtual DbSet<TUnit> TUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Server=tcp:103.83.81.251,1433;Database=admin_dcpc;Integrated Security=False;Encrypt=False;Connection Timeout=30;user Id=admin_dcpc;password=DcPT#@$1234#@");
         //=> optionsBuilder.UseSqlServer("Server=tcp:103.83.81.251,1433;Database=admin_dcpc;Integrated Security=True;Encrypt=False;Connection Timeout=30;user Id=admin_dcpc;password=DcPT#@$1234#@");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("admin_dcpc");

        modelBuilder.Entity<MBlock>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("M_Block", "dbo");

            entity.Property(e => e.BlockCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Block_Code");
            entity.Property(e => e.BlockName)
                .HasMaxLength(100)
                .HasColumnName("Block_Name");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("District_Code");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("State_Code");
        });

        modelBuilder.Entity<MChemical>(entity =>
        {
            entity.HasKey(e => e.ChemicalCode).HasName("PK_M_Chemical_Units");

            entity.ToTable("M_Chemical", "dbo");

            entity.Property(e => e.ChemicalCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Chemical_Code");
            entity.Property(e => e.ChemicalName)
                .HasMaxLength(250)
                .HasColumnName("Chemical_Name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<MChemicalProduct>(entity =>
        {
            entity.HasKey(e => e.ChemicalProductCode).HasName("PK_M_Chemical_Units_Product");

            entity.ToTable("M_Chemical_Product", "dbo");

            entity.Property(e => e.ChemicalProductCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Chemical_Product_Code");
            entity.Property(e => e.ChemicalCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Chemical_Code");
            entity.Property(e => e.ChemicalProductName)
                .HasMaxLength(250)
                .HasColumnName("Chemical_Product_Name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.ChemicalCodeNavigation).WithMany(p => p.MChemicalProducts)
                .HasForeignKey(d => d.ChemicalCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_M_Chemical_Units_Product_M_Chemical_Units");
        });

        modelBuilder.Entity<MDistrict>(entity =>
        {
            entity.HasKey(e => e.DistrictCode);

            entity.ToTable("M_District", "dbo");

            entity.Property(e => e.DistrictCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("District_Code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .HasColumnName("District_Name");
            entity.Property(e => e.DistrictShortName)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("District_Short_Name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("State_Code");

            entity.HasOne(d => d.StateCodeNavigation).WithMany(p => p.MDistricts)
                .HasForeignKey(d => d.StateCode)
                .HasConstraintName("FK_M_District_M_District");
        });

        modelBuilder.Entity<MPchemical>(entity =>
        {
            entity.HasKey(e => e.PchemicalCode).HasName("PK_M_PChemical_Units_SubCategory");

            entity.ToTable("M_PChemical", "dbo");

            entity.Property(e => e.PchemicalCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("PChemical_Code");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PchemicalName)
                .HasMaxLength(250)
                .HasColumnName("PChemical_Name");
        });

        modelBuilder.Entity<MPchemicalProduct>(entity =>
        {
            entity.HasKey(e => e.PchemicalProductCode).HasName("PK_M_PChemical_Units_Product");

            entity.ToTable("M_PChemical_Product", "dbo");

            entity.Property(e => e.PchemicalProductCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("PChemical_Product_Code");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PchemicalCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("PChemical_Code");
            entity.Property(e => e.PchemicalProductName)
                .HasMaxLength(250)
                .HasColumnName("PChemical_Product_Name");

            entity.HasOne(d => d.PchemicalCodeNavigation).WithMany(p => p.MPchemicalProducts)
                .HasForeignKey(d => d.PchemicalCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_M_PChemical_Product_M_PChemical_Product");
        });

        modelBuilder.Entity<MState>(entity =>
        {
            entity.HasKey(e => e.StateCode);

            entity.ToTable("M_State", "dbo");

            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("State_Code");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.StateCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("State_Category");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("State_Name");
        });

        modelBuilder.Entity<TRegistration>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("T_Registration", "dbo");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");
            entity.Property(e => e.CpciicCode)
                .HasMaxLength(50)
                .HasColumnName("CPCIIC_Code");
            entity.Property(e => e.DeactiveDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .HasColumnName("Email_id");
            entity.Property(e => e.EmailOtp).HasColumnName("Email_OTP");
            entity.Property(e => e.EmailOtpConfirm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Email_OTP_Confirm");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Entry_Date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("IP_Address");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Islogin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISLogin");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Mobile_no");
            entity.Property(e => e.MobileOtp).HasColumnName("Mobile_OTP");
            entity.Property(e => e.MobileOtpConfirm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Mobile_OTP_Confirm");
            entity.Property(e => e.NameOfUnit)
                .HasMaxLength(250)
                .HasColumnName("Name_of_Unit");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("T_Units", "dbo");

            entity.Property(e => e.AdAddress)
                .HasMaxLength(500)
                .HasColumnName("AD_Address");
            entity.Property(e => e.AdEmail)
                .HasMaxLength(50)
                .HasColumnName("AD_Email");
            entity.Property(e => e.AdMobileNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AD_Mobile_no");
            entity.Property(e => e.AdName)
                .HasMaxLength(100)
                .HasColumnName("AD_Name");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.BlockCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Block_Code");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("District_Code");
            entity.Property(e => e.EntryBy)
                .HasMaxLength(50)
                .HasColumnName("Entry_By");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("Entry_Date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("IP_Address");
            entity.Property(e => e.NdAddress)
                .HasMaxLength(500)
                .HasColumnName("ND_Address");
            entity.Property(e => e.NdEmail)
                .HasMaxLength(50)
                .HasColumnName("ND_Email");
            entity.Property(e => e.NdMobileNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ND_Mobile_no");
            entity.Property(e => e.NdName)
                .HasMaxLength(100)
                .HasColumnName("ND_Name");
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("State_Code");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("Update_By");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasColumnName("User_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
