window.addEventListener('scroll', () => {
    document.body.style.setProperty('--scroll', window.pageYOffset / (document.body.offsetHeight - window.innerHeight));
}, false);


// JavaScript för att hantera fältet för avsändare baserat på anonymitet
document.getElementById("anonymCheckbox").addEventListener("change", function () {
    const avsandareField = document.getElementById("avsandareField");
    const avsandareInput = document.querySelector("input[name='Avsändare']"); // Hämta inputfältet för avsändare

    if (this.checked) {
        avsandareInput.value = "A"; // Fyll i med "A"
        avsandareField.style.display = "none"; // Dölj fältet för avsändare
    } else {
        avsandareField.style.display = "block"; // Visa fältet för avsändare
        avsandareInput.value = ""; // Rensa värdet om det visas igen
    }
});