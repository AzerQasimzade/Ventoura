document.addEventListener("DOMContentLoaded", function () {
    const car = document.querySelector(".car");
    const miniRoad = document.querySelector(".mini-road");

    // Arac? hareket ettiren fonksiyon
    function moveCar(distance) {
        car.style.bottom = distance + "px";
    }

    // Her bir gün için arac?n hareket etmesini sa?la
    // Örne?in, 3 gün varsa ve her gün için 100px ileri gitmesini sa?la
    const dayDistances = [0, 100, 200]; // ?htiyaca göre ayarla

    // Arac? günler aras?nda hareket ettir
    dayDistances.forEach((distance, index) => {
        setTimeout(() => {
            moveCar(distance);
        }, index * 3000); // Her bir gün için 3 saniye süre ekleyebilirsin
    });
});
