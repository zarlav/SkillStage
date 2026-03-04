import { getAllPosts, createPost } from "../services/postService";
export{}

export async function renderFeedPage() {

    const app = document.getElementById("app");
    if (!app) return;

    app.innerHTML = `
        <div class="feed-container">
            <div class="filter-section">
                <select id="filterType">
                    <option value="">Svi</option>
                    <option value="0">Crtez</option>
                    <option value="1">Muzika</option>
                    <option value="2">Tekst</option>
                </select>

                <button id="filterBtn">Filtriraj</button>
        </div>
        <div class="create-post-card">
                <h2>Kreiraj post</h2>

                <input id="title" placeholder="Title" />
                <textarea id="content" placeholder="Content"></textarea>

                <select id="postType">
                    <option value="0">Crtez</option>
                    <option value="1">Muzika</option>
                    <option value="2">Tekst</option>
                </select>

                <button id="createBtn">Postavi</button>
            </div>
            <div class="posts-container" id="posts"></div>
        </div>
        
    `;

    await loadPosts();

    attachCreateEvent();
    attachFilterEvent();
}

async function loadPosts(type?: string) {

    const posts = await getAllPosts(type ? Number(type) : undefined);

    const postsDiv = document.getElementById("posts");
    if (!postsDiv) return;

    postsDiv.innerHTML = "";

    posts.forEach((p: any) => {
        const postCard = createPostCard(p);
        postsDiv.appendChild(postCard);
    });
}

function createPostCard(p: any) {

    const postCard = document.createElement("div");
    postCard.className = "post-card";

    const typeText =
        p.type === 0 ? "Crtez" :
        p.type === 1 ? "Muzika" :
        "Tekst";

    postCard.innerHTML = `
        <h3>${p.title}</h3>
        <span class="post-type">${typeText}</span>
        <p>${p.content}</p>

        ${p.imageUrl ? `<img src="${p.imageUrl}" class="post-image" />` : ""}

        <small>Korisnik: ${p.userId}</small>
        <div class="average-rating" id="avg-${p.id}">
        </div>

        <hr/>
        <div>
            <input type="text" id="comment-${p.id}" placeholder="Komentar..." />
            <button class="comment-btn" data-postid="${p.id}">
                Dodaj komentar
            </button>
        </div>

        <div>
            <select id="rating-${p.id}">
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
            </select>

            <button class="rate-btn" data-postid="${p.id}">
                Oceni
            </button>

            <button class="show-comments-btn" data-postid="${p.id}">
                Prikazi komentare
            </button>

            <div class="comments-container" id="comments-${p.id}"></div>
        </div>
    `;

    attachPostEvents(postCard);
    loadAverageRating(p.id);
    return postCard;
}

async function loadAverageRating(postId: string) {

    const res = await fetch(
        `https://localhost:7288/api/Post/${postId}/average-rating`
    );
    const data = await res.json();
    const container = document.getElementById(`avg-${postId}`);
    if (!container) return;
    const avg = data.averageRating;
    container.innerHTML = ` Prosecna ocena: <strong>${avg.toFixed(1)}</strong> / 5 `;
}

function attachPostEvents(postCard: HTMLElement) {

    postCard.querySelector(".comment-btn")?.addEventListener("click", async (e) => {

        const btn = e.target as HTMLButtonElement;
        const postId = btn.dataset.postid!;

        const input = document.getElementById(`comment-${postId}`) as HTMLInputElement;
        const content = input.value;

        if (!content.trim()) {
            alert("Komentar prazan");
            return;
        }

        await fetch("https://localhost:7288/api/Post/comment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            body: JSON.stringify({ postId, content })
        });

        alert("Komentar dodat");
    });


    postCard.querySelector(".rate-btn")?.addEventListener("click", async (e) => {

        const btn = e.target as HTMLButtonElement;
        const postId = btn.dataset.postid!;

        const select = document.getElementById(`rating-${postId}`) as HTMLSelectElement;

        await fetch("https://localhost:7288/api/Post/rate", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            body: JSON.stringify({
                postId,
                value: Number(select.value)
            })
        });

        alert("Ocena poslata");
    });


    postCard.querySelector(".show-comments-btn")?.addEventListener("click", async (e) => {

        const btn = e.target as HTMLButtonElement;
        const postId = btn.dataset.postid!;
        const container = document.getElementById(`comments-${postId}`)!;

        if (container.innerHTML !== "") {
            container.innerHTML = "";
            return;
        }

        const res = await fetch(
            `https://localhost:7288/api/Post/${postId}/comments`
        );

        const comments = await res.json();

        container.innerHTML = "";

        comments.forEach((c: any) => {
            container.innerHTML += `
                <div class="comment-item">
                    <p>${c.content}</p>
                    <small>Korisnik: ${c.userId}</small>
                </div>
            `;
        });
    });
}
function attachFilterEvent() {

    document.getElementById("filterBtn")?.addEventListener("click", async () => {

        const typeValue = (document.getElementById("filterType") as HTMLSelectElement).value;

        await loadPosts(typeValue);
    });
}
function attachCreateEvent() {

    document.getElementById("createBtn")?.addEventListener("click", async () => {

        const title = (document.getElementById("title") as HTMLInputElement).value;
        const content = (document.getElementById("content") as HTMLTextAreaElement).value;
        const type = (document.getElementById("postType") as HTMLSelectElement).value;

        if (!title.trim() || !content.trim()) {
            alert("Popuni sva polja");
            return;
        }

        await createPost({
            title,
            content,
            type: Number(type),
            imageUrl: ""
        });

        await loadPosts();
    });
}