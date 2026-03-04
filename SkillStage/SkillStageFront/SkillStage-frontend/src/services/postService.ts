import { apiFetch } from "../utils/api";

export async function getAllPosts(type?: number) {

    const query = type !== undefined ? `?type=${type}` : "";

    const response = await fetch(
        `https://localhost:7288/api/Post${query}`
    );

    return await response.json();
}

export async function createPost(post: any) {
    return await apiFetch("/Post", {
        method: "POST",
        body: JSON.stringify(post)
    });
}