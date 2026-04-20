using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PaginaRefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Pagos")]
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        public int FacturaID { get; set; }
        public int TipoPagoID { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;

        [ForeignKey("FacturaID")]
        public virtual Facturas? Factura { get; set; }
        [ForeignKey("TipoPagoID")]
        public virtual TipoPago? TipoPago { get; set; }
    }
}