using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SaafiSystems.Data;

namespace SaafiSystems.Migrations
{
    [DbContext(typeof(SaafiDbContext))]
    [Migration("20170420024721_AddOwnerExpense")]
    partial class AddOwnerExpense
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SaafiSystems.Models.Driver", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("DeadMiles");

                    b.Property<string>("Description");

                    b.Property<int>("DriverNameID");

                    b.Property<int>("LoadMiles");

                    b.Property<int>("Rate");

                    b.Property<string>("Reference");

                    b.HasKey("ID");

                    b.HasIndex("DriverNameID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("SaafiSystems.Models.DriverName", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("DriverNames");
                });

            modelBuilder.Entity("SaafiSystems.Models.Expense", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("ExpenseCategoryID");

                    b.Property<string>("Owner");

                    b.Property<string>("Reference");

                    b.HasKey("ID");

                    b.HasIndex("ExpenseCategoryID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("SaafiSystems.Models.ExpenseCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ExpenseCategories");
                });

            modelBuilder.Entity("SaafiSystems.Models.Load", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("LoadCategoryID");

                    b.Property<string>("Owner");

                    b.Property<string>("Reference");

                    b.Property<int?>("RevenueCategoryID");

                    b.HasKey("ID");

                    b.HasIndex("LoadCategoryID");

                    b.HasIndex("RevenueCategoryID");

                    b.ToTable("Loads");
                });

            modelBuilder.Entity("SaafiSystems.Models.LoadCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("LoadCategories");
                });

            modelBuilder.Entity("SaafiSystems.Models.Owner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("SaafiSystems.Models.OwnerExpense", b =>
                {
                    b.Property<int>("ExpenseID");

                    b.Property<int>("OwnerID");

                    b.HasKey("ExpenseID", "OwnerID");

                    b.HasIndex("OwnerID");

                    b.ToTable("OwnerExpenses");
                });

            modelBuilder.Entity("SaafiSystems.Models.OwnerLoad", b =>
                {
                    b.Property<int>("LoadID");

                    b.Property<int>("OwnerID");

                    b.HasKey("LoadID", "OwnerID");

                    b.HasIndex("OwnerID");

                    b.ToTable("OwnerLoads");
                });

            modelBuilder.Entity("SaafiSystems.Models.Revenue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Owner");

                    b.Property<string>("Reference");

                    b.Property<int>("RevenueCategoryID");

                    b.HasKey("ID");

                    b.HasIndex("RevenueCategoryID");

                    b.ToTable("Revenues");
                });

            modelBuilder.Entity("SaafiSystems.Models.RevenueCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("RevenueCategories");
                });

            modelBuilder.Entity("SaafiSystems.Models.Driver", b =>
                {
                    b.HasOne("SaafiSystems.Models.DriverName", "DriverName")
                        .WithMany("Drivers")
                        .HasForeignKey("DriverNameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaafiSystems.Models.Expense", b =>
                {
                    b.HasOne("SaafiSystems.Models.ExpenseCategory", "ExpenseCategory")
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaafiSystems.Models.Load", b =>
                {
                    b.HasOne("SaafiSystems.Models.LoadCategory", "LoadCategory")
                        .WithMany("Loads")
                        .HasForeignKey("LoadCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaafiSystems.Models.RevenueCategory")
                        .WithMany("Loads")
                        .HasForeignKey("RevenueCategoryID");
                });

            modelBuilder.Entity("SaafiSystems.Models.OwnerExpense", b =>
                {
                    b.HasOne("SaafiSystems.Models.Expense", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaafiSystems.Models.Owner", "Owner")
                        .WithMany("OwnerExpenses")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaafiSystems.Models.OwnerLoad", b =>
                {
                    b.HasOne("SaafiSystems.Models.Load", "Load")
                        .WithMany("OwnerLoads")
                        .HasForeignKey("LoadID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SaafiSystems.Models.Owner", "Owner")
                        .WithMany("OwnerLoads")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SaafiSystems.Models.Revenue", b =>
                {
                    b.HasOne("SaafiSystems.Models.RevenueCategory", "RevenueCategory")
                        .WithMany("Revenues")
                        .HasForeignKey("RevenueCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
