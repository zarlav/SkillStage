const API_URL = "https://localhost:7288/api";

export async function apiFetch(endpoint: string, options: RequestInit = {}) {

    const token = localStorage.getItem("token");

    const headers: any = {
        "Content-Type": "application/json"
    };
    if (token) {
        headers["Authorization"] = "Bearer " + token;
    }

    if (options.headers) {
        Object.assign(headers, options.headers);
    }

    const response = await fetch(API_URL + endpoint, {
        method: options.method || "GET",
        headers: headers,
        body: options.body
    });
    if (!response.ok) {
        console.error("Greska sa serverom:", response.status);
        throw new Error("Greska pri komunikaciji sa serverom");
    }
    return await response.json();
}