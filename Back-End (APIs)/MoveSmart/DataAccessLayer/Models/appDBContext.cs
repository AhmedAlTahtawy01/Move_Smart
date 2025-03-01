﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public partial class appDBContext : DbContext
{
    public appDBContext(DbContextOptions<appDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<Consumablespurchaseorder> Consumablespurchaseorders { get; set; }

    public virtual DbSet<Consumablesreplacement> Consumablesreplacements { get; set; }

    public virtual DbSet<Consumableswithdrawapplication> Consumableswithdrawapplications { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Sparepart> Spareparts { get; set; }

    public virtual DbSet<Sparepartspurchaseorder> Sparepartspurchaseorders { get; set; }

    public virtual DbSet<Sparepartsreplacement> Sparepartsreplacements { get; set; }

    public virtual DbSet<Sparepartswithdrawapplication> Sparepartswithdrawapplications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Vehicleconsumable> Vehicleconsumables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("applications");

            entity.HasIndex(e => e.ApplicationType, "FK_Applications_ApplicationTypes");

            entity.HasIndex(e => e.CreatedByUser, "FK_Applications_Users");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicationDescription)
                .IsRequired()
                .HasMaxLength(2000)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("enum('Confirmed','Rejected','Pending','Canceled')");

            entity.HasOne(d => d.CreatedByUserNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.CreatedByUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Users");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PRIMARY");

            entity.ToTable("buses");

            entity.HasIndex(e => e.VehicleId, "FK_Buses_Vehicles");

            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Buses)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buses_Vehicles");
        });

        modelBuilder.Entity<Consumablespurchaseorder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("consumablespurchaseorders");

            entity.HasIndex(e => e.ApplicationId, "FK_ConsumablesPurchaseOrders_Applications");

            entity.HasIndex(e => e.RequiredItem, "FK_ConsumablesPurchaseOrders_VehicleConsumables");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.HasOne(d => d.Application).WithMany(p => p.Consumablespurchaseorders)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesPurchaseOrders_Applications");

            entity.HasOne(d => d.RequiredItemNavigation).WithMany(p => p.Consumablespurchaseorders)
                .HasForeignKey(d => d.RequiredItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesPurchaseOrders_VehicleConsumables");
        });

        modelBuilder.Entity<Consumablesreplacement>(entity =>
        {
            entity.HasKey(e => e.ReplacementId).HasName("PRIMARY");

            entity.ToTable("consumablesreplacements");

            entity.HasIndex(e => e.MaintenanceId, "FK_ConsumablesReplacements_Maintenance");

            entity.HasIndex(e => e.ConsumableId, "FK_ConsumablesReplacements_VehicleConsumables");

            entity.Property(e => e.ReplacementId).HasColumnName("ReplacementID");
            entity.Property(e => e.ConsumableId).HasColumnName("ConsumableID");
            entity.Property(e => e.MaintenanceId).HasColumnName("MaintenanceID");

            entity.HasOne(d => d.Consumable).WithMany(p => p.Consumablesreplacements)
                .HasForeignKey(d => d.ConsumableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesReplacements_VehicleConsumables");
        });

        modelBuilder.Entity<Consumableswithdrawapplication>(entity =>
        {
            entity.HasKey(e => e.WithdrawApplicationId).HasName("PRIMARY");

            entity.ToTable("consumableswithdrawapplications");

            entity.HasIndex(e => e.ApplicationId, "FK_ConsumablesWithdrawApplications_Applications");

            entity.HasIndex(e => e.ConsumableId, "FK_ConsumablesWithdrawApplications_VehicleConsumables");

            entity.HasIndex(e => e.VehicleId, "FK_ConsumablesWithdrawApplications_Vehicles");

            entity.Property(e => e.WithdrawApplicationId).HasColumnName("WithdrawApplicationID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ConsumableId).HasColumnName("ConsumableID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Application).WithMany(p => p.Consumableswithdrawapplications)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesWithdrawApplications_Applications");

            entity.HasOne(d => d.Consumable).WithMany(p => p.Consumableswithdrawapplications)
                .HasForeignKey(d => d.ConsumableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesWithdrawApplications_VehicleConsumables");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Consumableswithdrawapplications)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumablesWithdrawApplications_Vehicles");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PRIMARY");

            entity.ToTable("drivers");

            entity.HasIndex(e => e.VehicleId, "FK_Drivers_Vehicle");

            entity.Property(e => e.DriverId)
                .ValueGeneratedNever()
                .HasColumnName("DriverID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.NationalNo)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_Vehicle");
        });

        modelBuilder.Entity<Sparepart>(entity =>
        {
            entity.HasKey(e => e.SparePartId).HasName("PRIMARY");

            entity.ToTable("spareparts");

            entity.Property(e => e.SparePartId).HasColumnName("SparePartID");
            entity.Property(e => e.PartName)
                .IsRequired()
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Sparepartspurchaseorder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("sparepartspurchaseorders");

            entity.HasIndex(e => e.ApplicationId, "FK_SparePartsPurchaseOrders_Applications");

            entity.HasIndex(e => e.RequiredItem, "FK_SparePartsPurchaseOrders_SpareParts");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.HasOne(d => d.Application).WithMany(p => p.Sparepartspurchaseorders)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsPurchaseOrders_Applications");

            entity.HasOne(d => d.RequiredItemNavigation).WithMany(p => p.Sparepartspurchaseorders)
                .HasForeignKey(d => d.RequiredItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsPurchaseOrders_SpareParts");
        });

        modelBuilder.Entity<Sparepartsreplacement>(entity =>
        {
            entity.HasKey(e => e.ReplacementId).HasName("PRIMARY");

            entity.ToTable("sparepartsreplacements");

            entity.HasIndex(e => e.MaintenanceId, "FK_SparePartsReplacements_Maintenance");

            entity.HasIndex(e => e.SparePartId, "FK_SparePartsReplacements_SpareParts");

            entity.Property(e => e.ReplacementId).HasColumnName("ReplacementID");
            entity.Property(e => e.MaintenanceId).HasColumnName("MaintenanceID");
            entity.Property(e => e.SparePartId).HasColumnName("SparePartID");

            entity.HasOne(d => d.SparePart).WithMany(p => p.Sparepartsreplacements)
                .HasForeignKey(d => d.SparePartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsReplacements_SpareParts");
        });

        modelBuilder.Entity<Sparepartswithdrawapplication>(entity =>
        {
            entity.HasKey(e => e.WithdrawApplicationId).HasName("PRIMARY");

            entity.ToTable("sparepartswithdrawapplications");

            entity.HasIndex(e => e.ApplicationId, "FK_SparePartsWithdrawApplications_Applications");

            entity.HasIndex(e => e.SparePartId, "FK_SparePartsWithdrawApplications_SpareParts");

            entity.HasIndex(e => e.VehicleId, "FK_SparePartsWithdrawApplications_Vehicles");

            entity.Property(e => e.WithdrawApplicationId).HasColumnName("WithdrawApplicationID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.SparePartId).HasColumnName("SparePartID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Application).WithMany(p => p.Sparepartswithdrawapplications)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsWithdrawApplications_Applications");

            entity.HasOne(d => d.SparePart).WithMany(p => p.Sparepartswithdrawapplications)
                .HasForeignKey(d => d.SparePartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsWithdrawApplications_SpareParts");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Sparepartswithdrawapplications)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SparePartsWithdrawApplications_Vehicles");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.NationalNo)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasColumnType("enum('Hospital Manager','General Manager','General Supervisor','Patrols Supervisor','Workshop Supervisor','Administrative Supervisor')");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PRIMARY");

            entity.ToTable("vehicles");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.AssociatedHospital)
                .IsRequired()
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AssociatedTask)
                .IsRequired()
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BrandName)
                .IsRequired()
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ModelName)
                .IsRequired()
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PlateNumbers)
                .IsRequired()
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("enum('Available','Working','BrokenDown')");
        });

        modelBuilder.Entity<Vehicleconsumable>(entity =>
        {
            entity.HasKey(e => e.ConsumableId).HasName("PRIMARY");

            entity.ToTable("vehicleconsumables");

            entity.Property(e => e.ConsumableId).HasColumnName("ConsumableID");
            entity.Property(e => e.ConsumableName)
                .IsRequired()
                .HasMaxLength(100)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ValidityLength).HasColumnName("validityLength");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}