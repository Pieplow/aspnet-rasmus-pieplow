document.addEventListener('DOMContentLoaded', function () {
    const menuToggle = document.getElementById('mobile-menu');
    const navContainer = document.querySelector('.nav-container');

    if (menuToggle && navContainer) {
        menuToggle.addEventListener('click', function (e) {
            e.preventDefault(); // Hindrar eventuell oönskad laddning
            navContainer.classList.toggle('active');
            console.log("Meny klickad, active-klass växlad!"); // Kolla konsolen (F12) om det funkar
        });
    } else {
        console.error("Kunde inte hitta mobile-menu eller nav-container i DOM.");
    }
});