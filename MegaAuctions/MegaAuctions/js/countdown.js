var ngayend = document.getElementById("ngayend").value;     //12/25/2021 00:00:00
const date = new Date(ngayend);
function updatecountdown() {
    const datenow = new Date();
    const diff = date - datenow;
    const d = Math.floor(diff / 1000 / 60 / 60 / 24);
    const h = Math.floor(diff / 1000 / 60 / 60) % 24;
    const m = Math.floor(diff / 1000 / 60) % 60;
    const s = Math.floor(diff / 1000) % 60;

    document.getElementById("daycd").innerHTML = d;
    document.getElementById("hourcd").innerHTML = h;
    document.getElementById("minutecd").innerHTML = m;
    document.getElementById("secondcd").innerHTML = s;

}
setInterval(updatecountdown, 1000);