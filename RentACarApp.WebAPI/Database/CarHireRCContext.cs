using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RentACarApp.WebAPI.Database
{
    public partial class RentACarAppContext : DbContext
    {
        public RentACarAppContext()
        {
        }

        public RentACarAppContext(DbContextOptions<RentACarAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Automobil> Automobil { get; set; }
        public virtual DbSet<Drzava> Drzava { get; set; }
        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<KategorijaVozila> KategorijaVozila { get; set; }
        public virtual DbSet<Klijent> Klijent { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<KorisniciUloge> KorisniciUloge { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Ocjena> Ocjena { get; set; }
        public virtual DbSet<Poruka> Poruka { get; set; }
        public virtual DbSet<Proizvodjac> Proizvodjac { get; set; }
        public virtual DbSet<Racun> Racun { get; set; }
        public virtual DbSet<RegistracijaVozila> RegistracijaVozila { get; set; }
        public virtual DbSet<RezervacijaRentanja> RezervacijaRentanja { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=CarHireRC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Automobil>(entity =>
            {
                entity.Property(e => e.AutomobilId).HasColumnName("AutomobilID");

                entity.Property(e => e.Boja)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BrojSjedista)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BrojVrata)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.EmisioniStandard)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Gorivo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KategorijaId).HasColumnName("KategorijaID");

                entity.Property(e => e.Kubikaza)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Potrosnja).HasMaxLength(50);

                entity.Property(e => e.SnagaMotora)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Transmisija)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Kategorija)
                    .WithMany(p => p.Automobil)
                    .HasForeignKey(d => d.KategorijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Automobil_KategorijaID");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Automobil)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Automobil_ModelID");
            });

            modelBuilder.Entity<Drzava>(entity =>
            {
                entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostanskiBroj).HasMaxLength(50);

                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Grad)
                    .HasForeignKey(d => d.DrzavaId);
            });

            modelBuilder.Entity<KategorijaVozila>(entity =>
            {
                entity.HasKey(e => e.KategorijaId);

                entity.Property(e => e.KategorijaId).HasColumnName("KategorijaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Klijent>(entity =>
            {
                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DatumRegistracije).HasColumnType("datetime");

                entity.Property(e => e.DatumRodjenja).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Klijent)
                    .HasForeignKey(d => d.GradId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradID");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.KorisnikId);

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Adresa).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon).HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Korisnici)
                    .HasForeignKey(d => d.GradId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Grad_GradID");
                 
                
            });

            modelBuilder.Entity<KorisniciUloge>(entity =>
            {
                entity.HasKey(e => e.KorisniciUlogeId);

                entity.Property(e => e.KorisniciUlogeId).HasColumnName("KorisniciUlogeID");

                entity.Property(e => e.DatumIzmjene).HasColumnType("datetime");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.UlogaId).HasColumnName("UlogaID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK_UserRole_User_UserID");

                entity.HasOne(d => d.Uloga)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.UlogaId)
                    .HasConstraintName("FK_UserRole_Role_RoleID");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProizvodjacId).HasColumnName("ProizvodjacID");

                entity.HasOne(d => d.Proizvodjac)
                    .WithMany(p => p.Model)
                    .HasForeignKey(d => d.ProizvodjacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Model_ProizvodjacID");
            });

            modelBuilder.Entity<Ocjena>(entity =>
            {
                entity.Property(e => e.OcjenaId).HasColumnName("OcjenaID");

                entity.Property(e => e.DatumEvidentiranja).HasColumnType("datetime");

                entity.Property(e => e.Ocjena1).HasColumnName("Ocjena");

                entity.Property(e => e.RezervacijaRentanjaId).HasColumnName("RezervacijaRentanjaID");

                entity.HasOne(d => d.RezervacijaRentanja)
                    .WithMany(p => p.Ocjena)
                    .HasForeignKey(d => d.RezervacijaRentanjaId);
            });

            modelBuilder.Entity<Poruka>(entity =>
            {
                entity.Property(e => e.PorukaId).HasColumnName("PorukaID");

                entity.Property(e => e.DatumVrijeme).HasColumnType("datetime");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.Naslov)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Posiljaoc)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RezervacijaRentanjaId).HasColumnName("RezervacijaRentanjaID");

                entity.Property(e => e.Sadrzaj).IsRequired();

                entity.Property(e => e.UposlenikId).HasColumnName("UposlenikID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Poruka)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RezervacijaRentanja)
                    .WithMany(p => p.Poruka)
                    .HasForeignKey(d => d.RezervacijaRentanjaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poruka_RezervacijaRentanjaID");

                entity.HasOne(d => d.Uposlenik)
                    .WithMany(p => p.Poruka)
                    .HasForeignKey(d => d.UposlenikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poruka_UposlenikID");
            });

            modelBuilder.Entity<Proizvodjac>(entity =>
            {
                entity.Property(e => e.ProizvodjacId).HasColumnName("ProizvodjacID");

                entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Proizvodjac)
                    .HasForeignKey(d => d.DrzavaId);
            });

            modelBuilder.Entity<Racun>(entity =>
            {
                entity.Property(e => e.RacunId).HasColumnName("RacunID");

                entity.Property(e => e.DatumIzdavanja).HasColumnType("datetime");
            });

            modelBuilder.Entity<RegistracijaVozila>(entity =>
            {
                entity.Property(e => e.RegistracijaVozilaId).HasColumnName("RegistracijaVozilaID");

                entity.Property(e => e.AutomobilId).HasColumnName("AutomobilID");

                entity.Property(e => e.DatumRegistracije).HasColumnType("datetime");

                entity.Property(e => e.RegistarskeOznake)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Trajanje)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UposlenikId).HasColumnName("UposlenikID");

                entity.Property(e => e.VaziDo).HasColumnType("datetime");

                entity.HasOne(d => d.Automobil)
                    .WithMany(p => p.RegistracijaVozila)
                    .HasForeignKey(d => d.AutomobilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registracija_AutomobilID");

                entity.HasOne(d => d.Uposlenik)
                    .WithMany(p => p.RegistracijaVozila)
                    .HasForeignKey(d => d.UposlenikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistracijaVozila_UposlenikID");
            });

            modelBuilder.Entity<RezervacijaRentanja>(entity =>
            {
                entity.Property(e => e.RezervacijaRentanjaId).HasColumnName("RezervacijaRentanjaID");

                entity.Property(e => e.AutomobilId).HasColumnName("AutomobilID");

                entity.Property(e => e.DatumKreiranja).HasColumnType("datetime");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.LokacijaPreuzimanja).HasMaxLength(100);

                entity.Property(e => e.RacunId).HasColumnName("RacunID");

                entity.Property(e => e.RezervacijaDo).HasColumnType("datetime");

                entity.Property(e => e.RezervacijaOd).HasColumnType("datetime");

                entity.Property(e => e.VracanjeUposlovnicu).HasColumnName("VracanjeUPoslovnicu");

                entity.HasOne(d => d.Automobil)
                    .WithMany(p => p.RezervacijaRentanja)
                    .HasForeignKey(d => d.AutomobilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RezervacijaRentanja_AutomobilID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.RezervacijaRentanja)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RezervacijaRentanja_KlijentID");

                entity.HasOne(d => d.Racun)
                    .WithMany(p => p.RezervacijaRentanja)
                    .HasForeignKey(d => d.RacunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RezervacijaRentanja_RacunID");
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.HasKey(e => e.UlogaId);

                entity.Property(e => e.UlogaId).HasColumnName("UlogaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
