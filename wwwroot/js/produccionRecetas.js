// wwwroot/js/produccionRecetas.js
// Lógica de UI para el módulo de Producción y Recetas

document.addEventListener("DOMContentLoaded", function () {
  // Elementos del DOM
  const btnNuevaReceta = document.getElementById("btnNuevaReceta");
  const modal = document.getElementById("modalNuevaReceta");
  const modalClose = document.querySelector(".modal-close");
  const btnAgregarIngrediente = document.getElementById(
    "btnAgregarIngrediente",
  );
  const listaIngredientes = document.getElementById("listaIngredientes");
  const btnGuardarReceta = document.getElementById("btnGuardarReceta");
  const btnCancelar = document.getElementById("btnCancelar");
  const btnNuevaOrden = document.getElementById("btnNuevaOrden");

  // ==================== MODAL NUEVA RECETA ====================
  if (btnNuevaReceta) {
    btnNuevaReceta.addEventListener("click", () => {
      modal.style.display = "flex";
    });
  }

  if (modalClose) {
    modalClose.addEventListener("click", () => {
      modal.style.display = "none";
    });
  }

  window.addEventListener("click", (e) => {
    if (e.target === modal) {
      modal.style.display = "none";
    }
  });

  // Formulario nueva receta
  const formNuevaReceta = document.getElementById("formNuevaReceta");
  if (formNuevaReceta) {
    formNuevaReceta.addEventListener("submit", (e) => {
      e.preventDefault();
      alert("Receta creada exitosamente (Demo)");
      modal.style.display = "none";
      // Aquí se integraría la lógica de guardado con el backend
    });
  }

  // ==================== AGREGAR INGREDIENTE DINÁMICO ====================
  if (btnAgregarIngrediente && listaIngredientes) {
    btnAgregarIngrediente.addEventListener("click", () => {
      const newRow = document.createElement("div");
      newRow.className = "grid-row ingrediente-item";
      newRow.innerHTML = `
                <select class="select-ingrediente" data-field="IngredienteID">
                    <option value="1">Azúcar Refinada</option>
                    <option value="2">Jarabe de Maíz</option>
                    <option value="3">Agua Purificada</option>
                    <option value="4">Gas Carbónico</option>
                    <option value="5">Saborizante Natural</option>
                </select>
                <input type="number" class="input-cantidad" data-field="Cantidad" value="0" step="0.1" placeholder="Cantidad">
                <select class="select-unidad" data-field="UnidadMedidaID">
                    <option value="1">kg</option>
                    <option value="2">L</option>
                    <option value="3">g</option>
                    <option value="4">ml</option>
                </select>
                <input type="number" class="input-orden" data-field="OrdenProceso" value="${getNextOrdenProceso()}" placeholder="Orden">
                <button class="btn-eliminar-fila">🗑️</button>
            `;
      listaIngredientes.appendChild(newRow);
      attachDeleteEvent(newRow.querySelector(".btn-eliminar-fila"));
    });
  }

  // Función para obtener el siguiente número de orden de proceso
  function getNextOrdenProceso() {
    const ordenes = document.querySelectorAll(".input-orden");
    let maxOrden = 0;
    ordenes.forEach((input) => {
      const val = parseInt(input.value);
      if (!isNaN(val) && val > maxOrden) maxOrden = val;
    });
    return maxOrden + 1;
  }

  // Función para adjuntar evento de eliminar a los botones existentes
  function attachDeleteEvent(btn) {
    if (btn) {
      btn.addEventListener("click", function () {
        this.closest(".ingrediente-item").remove();
        reordenarProcesos();
      });
    }
  }

  // Reordenar números de proceso después de eliminar
  function reordenarProcesos() {
    const ordenes = document.querySelectorAll(".input-orden");
    ordenes.forEach((input, index) => {
      input.value = index + 1;
    });
  }

  // Adjuntar eventos a botones eliminar existentes
  document
    .querySelectorAll(".btn-eliminar-fila")
    .forEach((btn) => attachDeleteEvent(btn));

  // ==================== GUARDAR RECETA ====================
  if (btnGuardarReceta) {
    btnGuardarReceta.addEventListener("click", () => {
      const ingredientes = [];
      document.querySelectorAll(".ingrediente-item").forEach((item) => {
        const ingrediente = {
          IngredienteID: item.querySelector('[data-field="IngredienteID"]')
            ?.value,
          Cantidad: item.querySelector('[data-field="Cantidad"]')?.value,
          UnidadMedidaID: item.querySelector('[data-field="UnidadMedidaID"]')
            ?.value,
          OrdenProceso: item.querySelector('[data-field="OrdenProceso"]')
            ?.value,
        };
        ingredientes.push(ingrediente);
      });
      console.log("Datos a guardar:", ingredientes);
      alert("Receta guardada correctamente (Demo - Conectar con API)");
      // Aquí se enviaría la data al servidor mediante fetch
    });
  }

  // ==================== CANCELAR FORMULARIO ====================
  if (btnCancelar) {
    btnCancelar.addEventListener("click", () => {
      if (confirm("¿Deseas cancelar los cambios?")) {
        // Resetear formulario o simplemente recargar datos demo
        location.reload();
      }
    });
  }

  // ==================== NUEVA ORDEN PRODUCCIÓN ====================
  if (btnNuevaOrden) {
    btnNuevaOrden.addEventListener("click", () => {
      alert(
        "Abrir formulario para nueva orden de producción (Integrar con backend)",
      );
      // Aquí se puede abrir otro modal o redirigir a una vista de creación
    });
  }

  // ==================== EDICIÓN DE RECETAS ====================
  document.querySelectorAll(".btn-editar").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const recetaId = btn.getAttribute("data-id");
      alert(`Editar receta ID: ${recetaId} - Cargar datos en formulario`);
      // Cargar datos de la receta seleccionada en el formulario de detalle
      document.getElementById("recetaSeleccionada").innerText =
        `Receta #${recetaId}`;
      // Aquí se haría fetch para obtener los detalles de la receta
    });
  });

  // ==================== VER DETALLE DE RECETA ====================
  document.querySelectorAll(".btn-ver").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const recetaId = btn.getAttribute("data-id");
      alert(
        `Ver detalle de receta ID: ${recetaId} - Mostrar ingredientes completos`,
      );
      // Mostrar modal o expandir sección con los detalles de la receta
    });
  });

  // ==================== ACTUALIZAR EFICIENCIA (DEMO) ====================
  // Simular actualización periódica de la eficiencia
  function updateEfficiency() {
    const eficienciaSpan = document.getElementById("eficienciaProduccion");
    if (eficienciaSpan) {
      const newEfficiency = (Math.random() * 15 + 80).toFixed(1);
      eficienciaSpan.innerText = newEfficiency;
      const progressFill = document
        .querySelector("#eficienciaProduccion")
        ?.closest(".card")
        ?.querySelector(".progress-fill");
      if (progressFill) progressFill.style.width = `${newEfficiency}%`;
    }
  }

  // Actualizar eficiencia cada 30 segundos (solo demo)
  setInterval(updateEfficiency, 30000);

  // ==================== CAMBIOS DE ESTADO VISUALES ====================
  // Cambiar estado de orden (demo con clicks en las tarjetas)
  document.querySelectorAll(".orden-card").forEach((card) => {
    card.addEventListener("click", (e) => {
      if (!e.target.closest(".btn-icon")) {
        const estadoSpan = card.querySelector(".pill-verde, .pill-rojo");
        if (estadoSpan) {
          const currentState = estadoSpan.innerText;
          if (currentState === "En Proceso") {
            estadoSpan.innerText = "Completada";
            estadoSpan.className = "pill-verde";
          } else if (currentState === "Planificada") {
            estadoSpan.innerText = "En Proceso";
            estadoSpan.className = "pill-verde";
          } else if (currentState === "Pausada") {
            estadoSpan.innerText = "En Proceso";
            estadoSpan.className = "pill-verde";
          }
        }
      }
    });
  });

  // ==================== INICIALIZACIÓN ====================
  console.log("Módulo de Producción y Recetas cargado correctamente");
});
