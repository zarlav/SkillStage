import { createPost } from "../services/postService";

export function renderCreatePostForm(container: HTMLElement) {

    const form = document.createElement("div");

    form.innerHTML = `
        <h3>Kreiraj post</h3>

        <input id="postTitle" placeholder="Title" />
        <br/><br/>

        <textarea id="postContent" placeholder="Content"></textarea>
        <br/><br/>

        <select id="postType">
            <option value="0">Crtez</option>
            <option value="1">Muzika</option>
            <option value="2">Tekst</option>
        </select>

        <div id="musicField" style="display:none;">
            <input id="musicUrl" placeholder="Link ka muzici" />
        </div>

        <br/>
        <button id="createBtn">Kreiraj</button>
        <hr/>
    `;

    container.appendChild(form);
    const button = form.querySelector("#createBtn") as HTMLButtonElement;

    button.addEventListener("click", async () => {

        const title = (form.querySelector("#postTitle") as HTMLInputElement).value;
        const content = (form.querySelector("#postContent") as HTMLTextAreaElement).value;
        const type = (form.querySelector("#postType") as HTMLSelectElement).value;

        await createPost({
            title,
            content,
            type: Number(type),
            imageUrl: ""
        });

        alert("Post kreiran!");
        location.reload();
    });
}