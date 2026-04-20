using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        // IDENTIFICADOR UNICO DEL USUARIO
        public int Id { get; set; }
        // NOMBRE COMPLETO
        public string Name { get; set; }
        // CORREO ELECTRONICO
        public string Email { get; set; }
        // HASH DE LA CONTRASEÑA PARA SEGURIDAD
        public string PasswordHash { get; set; }

        // RELACION: UN USUARIO REALIZA MUCHAS RESERVAS
        public ICollection<Reservation> Reservations { get; set; }
        // RELACION: UN USUARIO GENERA MUCHOS REGISTROS DE AUDITORIA
        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}