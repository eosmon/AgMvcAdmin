function openModal() {
    //document.getElementById('modal').style.display = 'block';
    document.getElementById('fade').style.display = 'block';
}

function closeModal() {
    //document.getElementById('modal').style.display = 'none';
    document.getElementById('fade').style.display = 'none';
}

function openFeed() {
    document.getElementById('modal').style.display = 'block';
}

function closeFeed() {
    document.getElementById('modal').style.display = 'none';
}

function loadAjax() {
    //document.getElementById('results').innerHTML = '';
    openModal();
    var xhr = false;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    if (xhr) {
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                closeModal();
                //document.getElementById("results").innerHTML = xhr.responseText;
            }
        }
        //xhr.open("GET", "load-ajax-data.php", true);
        //xhr.send(null);
    }
}