var clientGuid;

$(document).ready(function () {
    var req = window.location.href;
    if (req.indexOf("?") < 0) {
        connect();
    }
});

$(window).unload(function () {
    var req = window.location.href;
    if (req.indexOf("?") < 0)
        disconnect();
});

function sendRequest() {
    var url = './im?cid=' + clientGuid;
    $.ajax({
        type: "POST",
        url: url,
        success: processResponse,
        error: sendRequest
    });
}

function connect() {
    var url = './im?cpsp=CONNECT';
    $.ajax({
        type: "POST",
        url: url,
        success: onConnected,
        error: connectionRefused
    });
}

function disconnect() {
    var url = './im?cpsp=DISCONNECT';
    $.ajax({
        type: "POST",
        url: url
    });
}

function processResponse(transport) {
    sendRequest();
}

function onConnected(transport) {
    clientGuid = transport;
    sendRequest();
}

function connectionRefused() {
    setTimeout(сonnect(), 5000);
}
