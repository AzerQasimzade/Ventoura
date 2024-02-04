document.addEventListener("DOMContentLoaded", function () {
    const car = document.querySelector(".car");
    const miniRoad = document.querySelector(".mini-road");

    // Arac? hareket ettiren fonksiyon
    function moveCar(distance) {
        car.style.bottom = distance + "px";
    }

    // Her bir g�n i�in arac?n hareket etmesini sa?la
    // �rne?in, 3 g�n varsa ve her g�n i�in 100px ileri gitmesini sa?la
    const dayDistances = [0, 100, 200]; // ?htiyaca g�re ayarla

    // Arac? g�nler aras?nda hareket ettir
    dayDistances.forEach((distance, index) => {
        setTimeout(() => {
            moveCar(distance);
        }, index * 3000); // Her bir g�n i�in 3 saniye s�re ekleyebilirsin
    });
});
