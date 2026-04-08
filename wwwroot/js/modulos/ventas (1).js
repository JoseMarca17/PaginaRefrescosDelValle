/**
 * ventas.js  –  Módulo Ventas & Clientes · RefrescosDelValle
 */

// ── Buscador en tabla ────────────────────────────────────────────────────────
function iniciarBuscador(inputId, tablaId) {
    const input = document.getElementById(inputId);
    if (!input) return;
    input.addEventListener('input', function () {
        const filtro = this.value.toLowerCase();
        document.querySelectorAll(`#${tablaId} tbody tr`).forEach(fila => {
            fila.style.display = fila.textContent.toLowerCase().includes(filtro) ? '' : 'none';
        });
    });
}

// ── Detalle dinámico de pedido ───────────────────────────────────────────────
let filaIndex = 0;

function agregarFilaDetalle() {
    const tabla = document.getElementById('tablaDetalles');
    if (!tabla) return;

    const fila = document.createElement('tr');
    fila.innerHTML = `
        <td>
            <select name="Detalles[${filaIndex}].ProductoID" class="form-select ventas-input select-producto" required>
                <option value="">— Producto —</option>
                ${productosDisponibles.map(p => `<option value="${p.id}" data-precio="${p.precio}">${p.nombre}</option>`).join('')}
            </select>
        </td>
        <td><input type="number" name="Detalles[${filaIndex}].Cantidad"       class="form-control ventas-input input-cantidad" min="1" value="1" required /></td>
        <td><input type="number" name="Detalles[${filaIndex}].PrecioUnitario" class="form-control ventas-input input-precio"   min="0.01" step="0.01" required /></td>
        <td class="subtotal-celda fw-semibold">Bs 0.00</td>
        <td><button type="button" class="btn-icono danger" onclick="this.closest('tr').remove(); calcularTotal()"><i class="bi bi-trash"></i></button></td>
    `;
    tabla.querySelector('tbody').appendChild(fila);

    // Autocompletar precio al seleccionar producto
    fila.querySelector('.select-producto').addEventListener('change', function () {
        const opt = this.selectedOptions[0];
        const precio = opt?.dataset?.precio ?? '';
        fila.querySelector('.input-precio').value = precio;
        calcularSubtotal(fila);
    });

    fila.querySelector('.input-cantidad').addEventListener('input', () => calcularSubtotal(fila));
    fila.querySelector('.input-precio').addEventListener('input',   () => calcularSubtotal(fila));

    filaIndex++;
}

function calcularSubtotal(fila) {
    const cant   = parseFloat(fila.querySelector('.input-cantidad')?.value) || 0;
    const precio = parseFloat(fila.querySelector('.input-precio')?.value)   || 0;
    fila.querySelector('.subtotal-celda').textContent = `Bs ${(cant * precio).toFixed(2)}`;
    calcularTotal();
}

function calcularTotal() {
    let total = 0;
    document.querySelectorAll('.subtotal-celda').forEach(celda => {
        total += parseFloat(celda.textContent.replace('Bs ', '')) || 0;
    });
    const totEl = document.getElementById('totalPedido');
    if (totEl) totEl.textContent = `Bs ${total.toFixed(2)}`;
    const hidEl = document.getElementById('hiddenTotal');
    if (hidEl) hidEl.value = total.toFixed(2);
}

// ── Confirmar cambio de estado ────────────────────────────────────────────────
function confirmarCambioEstado(form, estado) {
    if (confirm(`¿Cambiar el estado a "${estado}"?`)) form.submit();
}

// ── Mostrar/ocultar campo banco según tipo de pago ────────────────────────────
function toggleBanco() {
    const tipoPagoSelect = document.getElementById('TipoPagoID');
    const campoBanco     = document.getElementById('campoBanco');
    if (!tipoPagoSelect || !campoBanco) return;

    tipoPagoSelect.addEventListener('change', function () {
        const requiere = this.selectedOptions[0]?.dataset?.requiereBanco === 'true';
        campoBanco.style.display = requiere ? '' : 'none';
    });
}

// ── Init ─────────────────────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
    iniciarBuscador('buscador', 'tablaClientes');
    iniciarBuscador('buscadorPedidos', 'tablaPedidos');
    iniciarBuscador('buscadorFacturas', 'tablaFacturas');
    toggleBanco();

    // Si hay filas iniciales en el formulario de pedido
    document.querySelectorAll('#tablaDetalles tbody tr').forEach(fila => {
        fila.querySelector('.input-cantidad')?.addEventListener('input', () => calcularSubtotal(fila));
        fila.querySelector('.input-precio')?.addEventListener('input',   () => calcularSubtotal(fila));
    });

    calcularTotal();
});

// Variable global con productos (se llena desde la vista con @Json.Serialize)
var productosDisponibles = window.productosDisponibles || [];
