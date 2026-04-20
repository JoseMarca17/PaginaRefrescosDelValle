using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    [Table("Facturas")] // Esto le dice a SQL que la tabla se llama Facturas
    public partial class Facturas
    {
        [Key]
        public int FacturaID { get; set; }

        [Required]
        public string NumeroFactura { get; set; } = null!;

        public DateTime FechaEmision { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        // --- RELACIONES ---

        // Relación con Cliente
        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public virtual Cliente Cliente { get; set; } = null!;

        // Relación inversa: Una factura tiene muchos detalles
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
    }
}