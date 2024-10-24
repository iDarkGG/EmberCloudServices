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
            entity.HasKey(e => e.ClientID).HasName("PK__Client__E67E1A041E7F2ADF");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<CreatedUser>(entity =>
        {
            entity.HasKey(e => e.CreatedUsersID).HasName("PK__CreatedU__6A20F8E288A8D556");

            entity.HasOne(d => d.DBRoles).WithMany(p => p.CreatedUsers).HasConstraintName("FK__CreatedUs__DBRol__45F365D3");
        });

        modelBuilder.Entity<DBRole>(entity =>
        {
            entity.HasKey(e => e.DBRolesID).HasName("PK__DBRoles__333880DE5A668433");
        });

        modelBuilder.Entity<DataCenter>(entity =>
        {
            entity.HasKey(e => e.DataCenterID).HasName("PK__DataCent__9E4870084BC03D8D");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF16F09C503");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaID).HasName("PK__Factura__5C024805222315D5");

            entity.Property(e => e.FacturaID).ValueGeneratedNever();

            entity.HasOne(d => d.FacturaDetalle).WithMany(p => p.Facturas).HasConstraintName("FK__Factura__Factura__534D60F1");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.FacturaDetalleID).HasName("PK__FacturaD__A9674B1AA87E2214");
        });

        modelBuilder.Entity<Instance>(entity =>
        {
            entity.HasKey(e => e.InstanceID).HasName("PK__Instance__5C51996F3DE92C7D");

            entity.HasOne(d => d.Client).WithMany(p => p.Instances).HasConstraintName("FK__Instance__Client__70DDC3D8");

            entity.HasOne(d => d.DataCenter).WithMany(p => p.Instances).HasConstraintName("FK__Instance__DataCe__71D1E811");
        });

        modelBuilder.Entity<PaymentInfo>(entity =>
        {
            entity.HasOne(d => d.Client).WithMany().HasConstraintName("FK__PaymentIn__Clien__47DBAE45");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.ticketID).HasName("PK__Ticket__3333C6703F230ABC");

            entity.Property(e => e.ticketCreationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tickets).HasConstraintName("FK__Ticket__Assigned__5EBF139D");

            entity.HasOne(d => d.TicketDetails).WithMany(p => p.Tickets).HasConstraintName("FK__Ticket__TicketDe__5DCAEF64");
        });

        modelBuilder.Entity<TicketDetail>(entity =>
        {
            entity.HasKey(e => e.TicketDetailsID).HasName("PK__TicketDe__01F22079FE8F96C6");

            entity.Property(e => e.MsgDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.sentByNavigation).WithMany(p => p.TicketDetails).HasConstraintName("FK__TicketDet__sentB__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
