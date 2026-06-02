using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Campeonato> Campeonatos => Set<Campeonato>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Criadero> Criaderos => Set<Criadero>();
    public DbSet<Equino> Equinos => Set<Equino>();
    public DbSet<EquinoCampeonato> EquinoCampeonatos => Set<EquinoCampeonato>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<Propietario> Propietarios => Set<Propietario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<TipoDePaso> TiposDePaso => Set<TipoDePaso>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureCampeonato(modelBuilder);
        ConfigureCategoria(modelBuilder);
        ConfigureCriadero(modelBuilder);
        ConfigureEstado(modelBuilder);
        ConfigurePropietario(modelBuilder);
        ConfigureRol(modelBuilder);
        ConfigureTipoDePaso(modelBuilder);
        ConfigureUsuario(modelBuilder);
        ConfigureEquino(modelBuilder);
        ConfigureEquinoCampeonato(modelBuilder);
    }

    private static void ConfigureCampeonato(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campeonato>(e =>
        {
            e.ToTable("campeonato");
            e.HasKey(x => x.CampeonatoId);
            e.Property(x => x.CampeonatoId).HasColumnName("campeonato_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.FechaCampeonato).HasColumnName("fecha_campeonato").HasColumnType("date");
            e.Property(x => x.Ubicacion).HasColumnName("ubicacion");
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
            e.Property(x => x.Nivel).HasColumnName("nivel");
        });
    }

    private static void ConfigureCategoria(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("categoria");
            e.HasKey(x => x.CategoriaId);
            e.Property(x => x.CategoriaId).HasColumnName("categoria_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
        });
    }

    private static void ConfigureCriadero(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Criadero>(e =>
        {
            e.ToTable("criadero");
            e.HasKey(x => x.CriaderoId);
            e.Property(x => x.CriaderoId).HasColumnName("criadero_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Nit).HasColumnName("nit");
            e.Property(x => x.Telefono).HasColumnName("telefono");
        });
    }

    private static void ConfigureEstado(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(e =>
        {
            e.ToTable("estado");
            e.HasKey(x => x.EstadoId);
            e.Property(x => x.EstadoId).HasColumnName("estado_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
        });
    }

    private static void ConfigurePropietario(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Propietario>(e =>
        {
            e.ToTable("propietario");
            e.HasKey(x => x.PropietarioId);
            e.Property(x => x.PropietarioId).HasColumnName("propietario_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Alias).HasColumnName("alias");
            e.Property(x => x.Apellido).HasColumnName("apellido");
            e.Property(x => x.Cedula).HasColumnName("cedula");
            e.Property(x => x.Nit).HasColumnName("nit");
            e.Property(x => x.FechaDeNacimiento).HasColumnName("fecha_de_nacimiento").HasColumnType("timestamp without time zone");
            e.Property(x => x.Telefono).HasColumnName("telefono");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.Direccion).HasColumnName("direccion");
            e.Property(x => x.FechaDeIngreso).HasColumnName("fecha_de_ingreso").HasColumnType("timestamp without time zone");
        });
    }

    private static void ConfigureRol(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(e =>
        {
            e.ToTable("rol");
            e.HasKey(x => x.RolId);
            e.Property(x => x.RolId).HasColumnName("rol_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
        });
    }

    private static void ConfigureTipoDePaso(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoDePaso>(e =>
        {
            e.ToTable("tipo_de_paso");
            e.HasKey(x => x.TipoPasoId);
            e.Property(x => x.TipoPasoId).HasColumnName("tipo_paso_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
        });
    }

    private static void ConfigureUsuario(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuario");
            e.HasKey(x => x.UsuarioId);
            e.Property(x => x.UsuarioId).HasColumnName("usuario_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.Apellido).HasColumnName("apellido");
            e.Property(x => x.Username).HasColumnName("username");
            e.Property(x => x.Password).HasColumnName("password");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.RolId).HasColumnName("rol_id");
            e.Property(x => x.FechaDeCreacion).HasColumnName("fecha_de_creacion").HasColumnType("date");
            e.HasIndex(x => x.Email).IsUnique();
            e.HasIndex(x => x.Username).IsUnique();
            e.HasOne(x => x.Rol).WithMany(x => x.Usuarios).HasForeignKey(x => x.RolId);
        });
    }

    private static void ConfigureEquino(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equino>(e =>
        {
            e.ToTable("equino");
            e.HasKey(x => x.EquinoId);
            e.Property(x => x.EquinoId).HasColumnName("equino_id").UseIdentityByDefaultColumn();
            e.Property(x => x.Nombre).HasColumnName("nombre");
            e.Property(x => x.TipoDeSangre).HasColumnName("tipo_de_sangre");
            e.Property(x => x.EstadoId).HasColumnName("estado_id");
            e.Property(x => x.FechaDeNacimiento).HasColumnName("fecha_de_nacimiento").HasColumnType("date");
            e.Property(x => x.FechaDeFallecimiento).HasColumnName("fecha_de_fallecimiento").HasColumnType("date");
            e.Property(x => x.CriaderoId).HasColumnName("criadero_id");
            e.Property(x => x.Descripcion).HasColumnName("descripcion");
            e.Property(x => x.Sexo).HasColumnName("sexo");
            e.Property(x => x.ChipId).HasColumnName("chip_id");
            e.Property(x => x.Capon).HasColumnName("capon");
            e.Property(x => x.Mular).HasColumnName("mular");
            e.Property(x => x.EnCompetencia).HasColumnName("en_competencia");
            e.Property(x => x.FechaDeCreacion).HasColumnName("fecha_de_creacion").HasColumnType("date");
            e.Property(x => x.FechaDeActualizacion).HasColumnName("fecha_de_actualizacion").HasColumnType("date");
            e.Property(x => x.TipoDePasoId).HasColumnName("tipo_de_paso_id");
            e.Property(x => x.PropietarioId).HasColumnName("propietario_id");
            e.Property(x => x.PadreId).HasColumnName("padre_id");
            e.Property(x => x.MadreId).HasColumnName("madre_id");

            e.HasOne(x => x.Estado).WithMany(x => x.Equinos).HasForeignKey(x => x.EstadoId);
            e.HasOne(x => x.Criadero).WithMany(x => x.Equinos).HasForeignKey(x => x.CriaderoId);
            e.HasOne(x => x.TipoDePaso).WithMany(x => x.Equinos).HasForeignKey(x => x.TipoDePasoId);
            e.HasOne(x => x.Propietario).WithMany(x => x.Equinos).HasForeignKey(x => x.PropietarioId);
            e.HasOne(x => x.Padre).WithMany(x => x.HijosComoPadre).HasForeignKey(x => x.PadreId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Madre).WithMany(x => x.HijosComoMadre).HasForeignKey(x => x.MadreId).OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureEquinoCampeonato(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquinoCampeonato>(e =>
        {
            e.ToTable("equino_campeonato");
            e.HasKey(x => x.EquinoCampeonatoId);
            e.Property(x => x.EquinoCampeonatoId).HasColumnName("equino_campeonato_id").UseIdentityByDefaultColumn();
            e.Property(x => x.EquinoId).HasColumnName("equino_id");
            e.Property(x => x.CampeonatoId).HasColumnName("campeonato_id");
            e.Property(x => x.CategoriaId).HasColumnName("categoria_id");
            e.Property(x => x.Resultado).HasColumnName("resultado");
            e.Property(x => x.Puntaje).HasColumnName("puntaje").HasColumnType("numeric");
            e.Property(x => x.Posicion).HasColumnName("posicion");

            e.HasOne(x => x.Equino).WithMany(x => x.EquinoCampeonatos).HasForeignKey(x => x.EquinoId);
            e.HasOne(x => x.Campeonato).WithMany(x => x.EquinoCampeonatos).HasForeignKey(x => x.CampeonatoId);
            e.HasOne(x => x.Categoria).WithMany(x => x.EquinoCampeonatos).HasForeignKey(x => x.CategoriaId);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyEquinoTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyEquinoTimestamps();
        return base.SaveChanges();
    }

    private void ApplyEquinoTimestamps()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        foreach (var entry in ChangeTracker.Entries<Equino>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.FechaDeCreacion = today;
                entry.Entity.FechaDeActualizacion = today;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.FechaDeActualizacion = today;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Usuario>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.FechaDeCreacion = today;
            }
        }
    }
}
