var ngayend = document.getElementById("ngayend").value;     //12/25/2021 00:00:00
const date = new Date(ngayend);
const flag = 1;
function updatecountdown() {
    const datenow = new Date();
    const diff = date - datenow;
    const d = Math.floor(diff / 1000 / 60 / 60 / 24);
    const h = Math.floor(diff / 1000 / 60 / 60) % 24;
    const m = Math.floor(diff / 1000 / 60) % 60;
    const s = Math.floor(diff / 1000) % 60;

    if (s == 0 && m ==0 && h ==0 && d == 0) {
        setTimeout(function () {
            $('#click_endtime_reset').trigger('click');
        }, 0);
    }   
    if (diff >= 0) {          
        document.getElementById("daycd").innerHTML = d;
        document.getElementById("hourcd").innerHTML = h;
        document.getElementById("minutecd").innerHTML = m;
        document.getElementById("secondcd").innerHTML = s;
    }    
    else {        
        document.getElementById("daycd").innerHTML = "END";
        document.getElementById("hourcd").innerHTML = "END";
        document.getElementById("minutecd").innerHTML = "END";
        document.getElementById("secondcd").innerHTML = "END";
        //@@@@@ CSS @@@@@
        //FONTSIZE
        document.getElementById("daycd").style.fontSize =  "30px";
        document.getElementById("hourcd").style.fontSize =  "30px";
        document.getElementById("minutecd").style.fontSize = "30px";
        document.getElementById("secondcd").style.fontSize = "30px";
        //PADDING-TOP
        document.getElementById("daycd").style.paddingTop = "17px";
        document.getElementById("hourcd").style.paddingTop = "17px";
        document.getElementById("minutecd").style.paddingTop = "17px";
        document.getElementById("secondcd").style.paddingTop = "17px";
        //COLOR
        document.getElementById("daycd").style.color = "red";
        document.getElementById("hourcd").style.color = "red";
        document.getElementById("minutecd").style.color = "red";
        document.getElementById("secondcd").style.color = "red";

        //vo hieu hoa cac nut
        document.getElementById("btn-paynow").disabled = true;
        document.getElementById("btn_auction").disabled = true;
    }
}
setInterval(updatecountdown, 1000);
