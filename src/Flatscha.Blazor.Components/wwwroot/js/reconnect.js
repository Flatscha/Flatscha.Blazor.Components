initReconnect();

function initReconnect() {
    if (Blazor.defaultReconnectionHandler == undefined) {
        setTimeout(() => initReconnect(), 20);
    }
    else {
        Blazor.defaultReconnectionHandler.onConnectionDown = function (options, error) {

            display();
            setTimeout(() => reconnect(), 5000);
        };
    }
}

function reconnect() {
    var status = 0;

    try {
        var http = new XMLHttpRequest();
        http.open("GET", window.location.origin, false);
        http.send(null);
        status = http.status;
    }
    catch { }

    if (status == 200) {
        document.location.reload(true);
        return;
    }

    setTimeout(() => reconnect(), 5000);
}

function display() {
    var modal = document.createElement('div');

    const modalStyles = [
        'position: fixed',
        'top: 0',
        'right: 0',
        'bottom: 0',
        'left: 0',
        'z-index: 1050',
        'display: block',
        'overflow: hidden',
        'background-color: #fff',
        'opacity: 0.8',
        'text-align: center',
        'padding : 1rem',
    ];

    modal.style.cssText = modalStyles.join(';');
    modal.innerHTML = 'Reconnecting...';

    document.body.appendChild(modal);
}
