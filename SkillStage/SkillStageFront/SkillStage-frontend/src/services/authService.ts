import { apiFetch } from "../utils/api";

export async function login(userName: string, password: string) {
    const data = await apiFetch("/Auth/login", {
        method: "POST",
        body: JSON.stringify({ userName, password })
    });

    localStorage.setItem("token", data.token);
    return data;
}

export async function register(data: any) {
    return await apiFetch("/Auth/register", {
        method: "POST",
        body: JSON.stringify(data)
    });
}