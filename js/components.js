const isInSubfolder = window.location.pathname.includes("/modulos/");
const BASE = isInSubfolder ? "../" : "/";

async function loadComponent(selector, path) {
  try {
    const res = await fetch(BASE + path);
    if (!res.ok) throw new Error(`No se pudo cargar ${path}`);
    const html = await res.text();
    document.querySelector(selector).innerHTML = html;
  } catch (err) {
    console.warn("[components.js]", err.message);
  }
}

function getSession() {
  const raw = sessionStorage.getItem("rdv_session");
  return raw ? JSON.parse(raw) : null;
}
function setSession(data) {
  sessionStorage.setItem("rdv_session", JSON.stringify(data));
}
function clearSession() {
  sessionStorage.removeItem("rdv_session");
}

function applySession() {
  const session = getSession();
  const btnLogin = document.getElementById("btnAbrirLogin");
  const userMenu = document.getElementById("userMenu");
  if (!btnLogin || !userMenu) return; // header aún no cargado

  if (session) {
    btnLogin.style.display = "none";
    userMenu.style.display = "block";

    const initials = session.nombre
      .split(" ")
      .slice(0, 2)
      .map((w) => w[0].toUpperCase())
      .join("");

    document.getElementById("userInitials").textContent = initials;
    document.getElementById("userName").textContent =
      session.nombre.split(" ")[0];
    document.getElementById("userRoleLabel").textContent = session.rol;
    document.getElementById("ddUserFull").textContent = session.nombre;
    document.getElementById("ddUserEmail").textContent = session.email;
  } else {
    btnLogin.style.display = "";
    userMenu.style.display = "none";
  }
}

function bindHeaderEvents() {
  /* Scroll effect navbar */
  const navbar = document.getElementById("navbar");
  if (navbar) {
    window.addEventListener(
      "scroll",
      () => {
        navbar.classList.toggle("scrolled", window.scrollY > 60);
      },
      { passive: true },
    );
  }

  const navLinks = document.querySelectorAll('.nav-link[href*="#"]');
  const sections = document.querySelectorAll("section[id]");
  window.addEventListener(
    "scroll",
    () => {
      let current = "";
      sections.forEach((s) => {
        if (window.scrollY >= s.offsetTop - 100) current = s.id;
      });
      navLinks.forEach((l) => {
        l.classList.toggle(
          "active",
          l.getAttribute("href").endsWith("#" + current),
        );
      });
    },
    { passive: true },
  );

  const btnAbrir = document.getElementById("btnAbrirLogin");
  const modal = document.getElementById("loginModal");
  if (btnAbrir && modal) {
    btnAbrir.addEventListener("click", () => {
      modal.classList.add("open");
      document.getElementById("inputUsuario")?.focus();
    });
  }

  /* Cerrar modal (botón X) */
  const btnCerrar = document.getElementById("btnCerrarModal");
  if (btnCerrar && modal) {
    btnCerrar.addEventListener("click", () => modal.classList.remove("open"));
  }

  if (modal) {
    modal.addEventListener("click", (e) => {
      if (e.target === modal) modal.classList.remove("open");
    });
  }

  document.addEventListener("keydown", (e) => {
    if (e.key === "Escape") modal?.classList.remove("open");
  });

  const loginForm = document.getElementById("loginForm");
  const loginError = document.getElementById("loginError");
  if (loginForm) {
    loginForm.addEventListener("submit", (e) => {
      e.preventDefault();
      const usuario = document.getElementById("inputUsuario").value.trim();
      const password = document.getElementById("inputPassword").value;

      if (usuario === "admin@rdv.bo" && password === "admin123") {
        setSession({
          nombre: "Administrador Sistema",
          email: "admin@rdv.bo",
          rol: "Super Admin",
        });
        loginError.classList.remove("show");
        modal?.classList.remove("open");
        applySession();
        /* Redirigir al módulo de seguridad (ejemplo) */
        window.location.href = BASE + "modulos/seguridad.html";
      } else {
        loginError.classList.add("show");
        document.getElementById("inputPassword").value = "";
      }
    });
  }

  /* Dropdown usuario */
  const trigger = document.getElementById("userTrigger");
  const menu = document.getElementById("userMenu");
  const dropdown = document.getElementById("userDropdown");
  if (trigger && menu && dropdown) {
    trigger.addEventListener("click", () => {
      const isOpen = menu.classList.toggle("open");
      trigger.setAttribute("aria-expanded", isOpen);
    });
    /* Cerrar al click fuera */
    document.addEventListener("click", (e) => {
      if (!menu.contains(e.target)) {
        menu.classList.remove("open");
        trigger.setAttribute("aria-expanded", false);
      }
    });
  }
  const btnLogout = document.getElementById("btnLogout");
  if (btnLogout) {
    btnLogout.addEventListener("click", () => {
      clearSession();
      applySession();
      window.location.href = BASE + "index.html";
    });
  }
}

document.addEventListener("DOMContentLoaded", async () => {
  await loadComponent("#header-placeholder", "components/header.html");
  await loadComponent("#footer-placeholder", "components/footer.html");

  applySession();
  bindHeaderEvents();
});
