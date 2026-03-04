import { login } from "../services/authService";
import { renderFeedPage } from "./homePage";
import { renderRegisterPage } from "./registerPage";

export function renderLoginPage() {

    const app = document.getElementById("app");
    if (!app) return;

    app.innerHTML = `
        <div class="login-container">
            <div class="login-card">
                <h2>SkillStage</h2>

                <input id="username" placeholder="Username" />
                <input id="password" type="password" placeholder="Password" />

                <button id="loginBtn">Login</button>
                <button id="registerBtn">Register</button>
            </div>
        </div>
    `;

    document.getElementById("loginBtn")!.addEventListener("click", async () => {

        const username = (document.getElementById("username") as HTMLInputElement).value;
        const password = (document.getElementById("password") as HTMLInputElement).value;
        if(!username.trim() || !password.trim())
        {
             alert("Morate popuniti sva polja!");
            return;
        }
        try {
            await login(username, password);
            renderFeedPage();
        } catch {
            alert("Login failed");
        }
    });

    document.getElementById("registerBtn")!.addEventListener("click", () => {
    renderRegisterPage();
});
}