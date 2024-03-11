using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Model;

public class DataContext : DbContext
{

    private readonly string connectionString;
    public DataContext(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder
               .HasAnnotation("ProductVersion", "8.0.2")
               .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("Domain.Entity.Usuario", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<DateTime>("FechaCreacion")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            b.Property<DateTime>("FechaModificacion")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            b.Property<DateTime>("FechaNacimiento")
                .HasColumnType("date");

            b.Property<string>("PrimerApellido")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            b.Property<string>("PrimerNombre")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            b.Property<string>("SegundoApellido")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            b.Property<string>("SegundoNombre")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            b.Property<decimal>("Sueldo")
                .HasColumnType("decimal(18, 2)");

            b.HasKey("Id");

            b.ToTable("Usuarios");
        });
    }

}
