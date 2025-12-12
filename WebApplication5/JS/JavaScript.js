function showAlert() {
    alert("Patch saved successfully (JS Triggered)");
}
function toggleSidebar() {
    document.querySelector(".sidebar").classList.toggle("collapsed");
    document.querySelector(".main-content")?.classList.toggle("collapsed");
    document.querySelector(".content")?.classList.toggle("collapsed");
}