using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure.Persistence
{
    public class EventContext : DbContext
    {
        // CONSTRUCTOR QUE RECIBE LAS OPCIONES DE CONFIGURACION (CADENA DE CONEXION, ETC.)
        public EventContext(DbContextOptions<EventContext> options) : base(options) { }

        // CONSTRUCTOR VACIO PARA COMPATIBILIDAD CON HERRAMIENTAS DE DISEÑO
        public EventContext() : base() { }

        // DEFINICION DE LAS TABLAS DE LA BASE DE DATOS BASADAS EN LAS ENTIDADES
        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- CONFIGURACION DE RELACIONES (FLUENT API) --- 

            // UN EVENTO TIENE MUCHOS SECTORES 
            modelBuilder.Entity<Sector>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Sectors)
                .HasForeignKey(s => s.EventId);

            // UN SECTOR TIENE MUCHAS BUTACAS
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Sector)
                .WithMany(sec => sec.Seats)
                .HasForeignKey(s => s.SectorId);

            // UN USUARIO REALIZA MUCHAS RESERVAS 
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            // UNA BUTACA SE ASIGNA A UNA RESERVA
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SeatId);

            // UN USUARIO GENERA MUCHOS LOGS DE AUDITORIA 
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .IsRequired(false); // PUEDE SER NULO SI ES UN PROCESO DE SISTEMA 

            // --- CONFIGURACION DE PROPIEDADES Y TABLAS ---

            // CONFIGURACION DE ASIENTO: CONCURRENCIA OPTIMISTA 
            modelBuilder.Entity<Seat>()
                .Property(s => s.Version)
                .IsConcurrencyToken(); // MECANISMO PARA EVITAR DOBLE ASIGNACION 

            // CONFIGURACION DE PRECISION PARA PRECIOS
            modelBuilder.Entity<Sector>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            // --- SEED DATA (DATOS INICIALES PARA ENTREGA 1) --- 

            // 1. EVENTO DE PRUEBA
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Name = "Concierto de Rock", EventDate = DateTime.Now.AddMonths(1), Venue = "Estadio Unaj", Status = "Active" }
            );

            // 2. DOS SECTORES
            modelBuilder.Entity<Sector>().HasData(
                new Sector { Id = 1, EventId = 1, Name = "Platea Alta", Price = 5000, Capacity = 50 },
                new Sector { Id = 2, EventId = 1, Name = "Campo", Price = 3000, Capacity = 50 }
            );

            // 3. GENERACION DE 100 BUTACAS (50 POR SECTOR) 
            var seats = new List<Seat>();
            // GENERAR PARA SECTOR 1
            for (int i = 1; i <= 50; i++)
            {
                seats.Add(new Seat { Id = Guid.NewGuid(), SectorId = 1, RowIdentifier = "A", SeatNumber = i, Status = "Available", Version = 1 });
            }
            // GENERAR PARA SECTOR 2
            for (int i = 1; i <= 50; i++)
            {
                seats.Add(new Seat { Id = Guid.NewGuid(), SectorId = 2, RowIdentifier = "B", SeatNumber = i, Status = "Available", Version = 1 });
            }

            modelBuilder.Entity<Seat>().HasData(seats);

            // 4. USUARIO DE PRUEBA
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Usuario Test", Email = "test@correo.com", PasswordHash = "123456" }
            );
        }
    }
}