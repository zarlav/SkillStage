import "./style.css";
import { renderLoginPage } from "./pages/loginPage";

document.addEventListener("DOMContentLoaded", () => {
    const app = document.querySelector<HTMLDivElement>("#app");

    if (!app) return;
    app.innerHTML = `<div id="root"></div>`;

    renderLoginPage();
});