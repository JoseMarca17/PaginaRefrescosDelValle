using System;

namespace RefrescosDelValle.Models.Produccion
{
    public class ProduccionDetalle
    {
        public int ProduccionDetalleID { get; set; }

        public int OrdenProduccionID { get; set; }

        public DateTime Fecha { get; set; }

        public decimal CantidadProducida { get; set; }

        public string Observaciones { get; set; }

        // Relación
        // public OrdenProduccion OrdenProduccion { get; set; }
    }
}