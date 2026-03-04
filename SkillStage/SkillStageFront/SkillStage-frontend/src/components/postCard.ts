export function createPostCard(post: any) {

    const div = document.createElement("div");
    div.className = "post-card";

    div.innerHTML = `
        <h3>${post.title}</h3>
        <p>${post.content}</p>
        <small>Type: ${post.type}</small>
        <hr/>
    `;

    return div;
}