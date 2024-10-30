using System;
using System.Collections.Generic;
using EmberAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.APIContext;

public partial class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CreatedUser> CreatedUsers { get; set; }

    public virtual DbSet<DBRole> DBRoles { get; set; }

    public virtual DbSet<DataCenter> DataCenters { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<Instance> Instances { get; set; }

    public virtual DbSet<PaymentInfo> PaymentInfos { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketDetail> TicketDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientID).HasName("PK__Client__E67E1A042AF465EB");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<CreatedUser>(entity =>
        {
            entity.HasKey(e => e.CreatedUsersID).HasName("PK__CreatedU__6A20F8E223EE0C7F");

            entity.HasOne(d => d.DBRoles).WithMany(p => p.CreatedUsers).HasConstraintName("FK__CreatedUs__DBRol__47DBAE45");

            entity.HasOne(d => d.Instance).WithMany(p => p.CreatedUsers).HasConstraintName("FK__CreatedUs__Insta__46E78A0C");
        });

        modelBuilder.Entity<DBRole>(entity =>
        {
            entity.HasKey(e => e.DBRolesID).HasName("PK__DBRoles__333880DEA8E61F1C");
        });

        modelBuilder.Entity<DataCenter>(entity =>
        {
            entity.HasKey(e => e.DataCenterID).HasName("PK__DataCent__9E487008D19DAFCB");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF1456BA782");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaID).HasName("PK__Factura__5C0248055F73FFA5");

            entity.Property(e => e.FacturaID).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.Facturas).HasConstraintName("FK__Factura__ClientI__6FE99F9F");

            entity.HasOne(d => d.FacturaDetalle).WithMany(p => p.Facturas).HasConstraintName("FK__Factura__Factura__70DDC3D8");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.FacturaDetalleID).HasName("PK__FacturaD__A9674B1AD206DFA0");
        });

        modelBuilder.Entity<Instance>(entity =>
        {
            entity.HasKey(e => e.InstanceID).HasName("PK__Instance__5C51996F5F57385A");

            entity.HasOne(d => d.Client).WithMany(p => p.Instances).HasConstraintName("FK__Instance__Client__403A8C7D");

            entity.HasOne(d => d.DataCenter).WithMany(p => p.Instances).HasConstraintName("FK__Instance__DataCe__412EB0B6");
        });

        modelBuilder.Entity<PaymentInfo>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany().HasConstraintName("FK__PaymentIn__Clien__49C3F6B7");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.ticketID).HasName("PK__Ticket__3333C6702C4B651A");

            entity.Property(e => e.ticketCreationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tickets).HasConstraintName("FK__Ticket__Assigned__66603565");
        });

        modelBuilder.Entity<TicketDetail>(entity =>
        {
            entity.HasKey(e => e.TicketDetailsID).HasName("PK__TicketDe__01F22079E56D9644");

            entity.Property(e => e.MsgDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Client).WithMany(p => p.TicketDetails).HasConstraintName("FK__TicketDet__Clien__7A672E12");

            entity.HasOne(d => d.Employee).WithMany(p => p.TicketDetails).HasConstraintName("FK__TicketDet__Emplo__797309D9");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketDet__Ticke__7B5B524B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
