using System;

namespace Domain.Entities
{
    public class AuditLog
    {
        // IDENTIFICADOR UNICO DEL LOG
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK: USUARIO QUE HIZO LA ACCION (PUEDE SER NULO SI ES UN PROCESO DE SISTEMA)
        public int? UserId { get; set; }
        public User User { get; set; }

        // ACCION REALIZADA (EJ. RESERVE_ATTEMPT, RESERVE_SUCCESS)
        public string Action { get; set; }
        // TIPO DE ENTIDAD AFECTADA (EJ. RESERVATION, SEAT)
        public string EntityType { get; set; }
        // ID DE LA ENTIDAD AFECTADA PARA TRAZABILIDAD
        public string EntityId { get; set; }
        // METADATOS ADICIONALES EN FORMATO JSON
        public string Details { get; set; }
        // MOMENTO EXACTO DEL REGISTRO (MILISEGUNDOS)
        public DateTime CreatedAt { get; set; }
    }
}