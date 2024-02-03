$(document).ready(function () {
    fetch("/api/SuperCoolFunctionality", {
        method: "GET",
    }).then(response => response.json())
        .then(data => {
            const tableBody = document.getElementById('adviceTable').getElementsByTagName('tbody')[0];
            data.forEach(item => {
                let row = tableBody.insertRow();
                let cell = row.insertCell(0);
                cell.innerHTML = item.advice;
            });
        }).catch(error => {
            console.log(error);
        });
});