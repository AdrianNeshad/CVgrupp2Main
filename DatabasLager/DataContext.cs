using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.DatabasLager
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Erfarenheter> Erfarenheter { get; set; }
        public DbSet<Kompetenser> Kompetenser { get; set; }
        public DbSet<Kontaktuppgifter> Kontaktuppgifter { get; set; }
        public DbSet<Meddelande> Meddelande { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonErfarenheter> PersonErfarenheter { get; set; }
        public DbSet<PersonKompetenser> PersonKompetenser { get; set; }
        public DbSet<PersonProjekt> PersonProjekt { get; set; }
        public DbSet<PersonMottagitMeddelande> PersonMottagitMeddelande { get; set; }
        public DbSet<PersonSkickatMeddelande> PersonSkickatMeddelande { get; set; }
        public DbSet<PersonUtbildningar> PersonUtbildningar { get; set; }
        public DbSet<Projekt> Projekt { get; set; }
        public DbSet<Utbildningar> Utbildningar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utbildningar>().HasData(

                new Utbildningar
                {
                    UtbildningID = 1,
                    Namn = "Örebro University",
                    Beskrivning = "Systemkunskap"
                }
            );

            modelBuilder.Entity<Kompetenser>().HasData(

                new Kompetenser
                {
                    ID = 1,
                    Titel = "Mecka med bilar",
                    Beskrivning = "Lagar motorer"
                }

            );

            modelBuilder.Entity<Erfarenheter>().HasData(

                new Erfarenheter
                {
                    ID = 1,
                    Titel = "Frisör",
                    Beskrivning = "Systemutvecklare"
                }

            );

            modelBuilder.Entity<Meddelande>().HasData(

                new Meddelande
                {
                    ID = 1,
                    Innehåll = "A"
                }

            );

            modelBuilder.Entity<Person>().HasData(

                new Person
                {
                    Användarnamn = "A",
                    Förnamn = "Pasta",
                    Efternamn = "Bolognese",
                    Lösenord = "Abc123",
                    Privat = false,
                    KontaktID = 1,

                }
            );

            modelBuilder.Entity<Kontaktuppgifter>().HasData(

                new Kontaktuppgifter
                {
                    KontaktID = 1,
                    Adress = "Brickebacken 123",
                    Telefonnummer = "+46 123",
                    Email = "ABC@edunet.oru.se"
                }
            );

            modelBuilder.Entity<Projekt>().HasData(

                new Projekt
                {
                    ProjektID = 1,
                    Namn = ".NET",
                    Beskrivning = "Programmera .NET",
                    Skapare = "A"
                }

            );

            modelBuilder.Entity<PersonUtbildningar>().
                HasKey(pu => new { pu.PersonID, pu.UtbildningID });

            modelBuilder.Entity<PersonUtbildningar>().HasData(
                new PersonUtbildningar
                {
                    PersonID = "A",
                    UtbildningID = 1

                });

            modelBuilder.Entity<PersonSkickatMeddelande>().
                HasKey(ps => new { ps.Användarnamn, ps.MeddelandeID });

            modelBuilder.Entity<PersonSkickatMeddelande>().HasData(
                new PersonSkickatMeddelande
                {
                    Användarnamn = "A",
                    MeddelandeID = 1

                });

            modelBuilder.Entity<PersonMottagitMeddelande>().
                HasKey(pm => new { pm.Användarnamn, pm.MeddelandeID });

            modelBuilder.Entity<PersonMottagitMeddelande>().HasData(
                new PersonMottagitMeddelande
                {
                    Användarnamn = "A",
                    MeddelandeID = 1

                });

            modelBuilder.Entity<PersonProjekt>().
                HasKey(pm => new { pm.ProjektID, pm.Medverkande });

            modelBuilder.Entity<PersonProjekt>().HasData(
                new PersonProjekt
                {
                    ProjektID = 1,
                    Medverkande = "A"

                });

            modelBuilder.Entity<PersonProjekt>()
                .HasOne(p => p.Projekt)
                .WithMany(p => p.HarMedverkat)
                .HasForeignKey(pm => pm.ProjektID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonProjekt>()
                .HasOne(p => p.Person)
                .WithMany(p => p.HarMedverkat)
                .HasForeignKey(pm => pm.Medverkande)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PersonKompetenser>().
                HasKey(pk => new { pk.ID, pk.PersonID });

            modelBuilder.Entity<PersonKompetenser>().HasData(
                new PersonKompetenser
                {
                    ID = 1,
                    PersonID = "A"

                });

            modelBuilder.Entity<PersonErfarenheter>().
                HasKey(pe => new { pe.ID, pe.PersonID });

            modelBuilder.Entity<PersonErfarenheter>().HasData(
                new PersonErfarenheter
                {
                    ID = 1,
                    PersonID = "A"

                });

        }

    }
}
