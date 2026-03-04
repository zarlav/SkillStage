import { register } from "../services/authService";
import { renderLoginPage } from "./loginPage";

export function renderRegisterPage() {

    const app = document.getElementById("app");
    if (!app) return;

    app.innerHTML = `
        <div class="login-container">
            <div class="login-card">
                <h2>Register</h2>
                <input id="regName" placeholder="name" />
                <input id="regLastName" placeholder="lastName" />
                <input id="regUsername" placeholder="userName" />
                <input id="regEmail" placeholder="email" />
                <input id="regPassword" type="password" placeholder="password" />

                <button id="regBtn">Register</button>
                <button id="backBtn">Back to Login</button>
            </div>
        </div>
    `;

    document.getElementById("regBtn")!.addEventListener("click", async () => {
        const name = (document.getElementById("regName") as HTMLInputElement).value;
        const lastname = (document.getElementById("regLastName") as HTMLInputElement).value;
        const username = (document.getElementById("regUsername") as HTMLInputElement).value;
        const email = (document.getElementById("regEmail") as HTMLInputElement).value;
        const password = (document.getElementById("regPassword") as HTMLInputElement).value;
        if (!name.trim() ||!lastname.trim() ||!username.trim() ||!email.trim() ||!password.trim())
        {
            alert("Morate popuniti sva polja!");
            return;
        }

        await register({
            name: name,
            lastName: lastname,
            userName: username,
            email,
            password
        });

        alert("Registration successful!");
        renderLoginPage();
    });

    document.getElementById("backBtn")!.addEventListener("click", () => {
        renderLoginPage();
    });
}