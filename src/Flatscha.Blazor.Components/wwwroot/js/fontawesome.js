
var fontAwesomeFiles = {
    0: '_content/Flatscha.Blazor.Components/css/fontawesome/fontawesome.svg',
    1: '_content/Flatscha.Blazor.Components/css/fontawesome/customIcons.svg',
};

var xmlFiles = {};

loadXMLDocuments();

function loadXMLDocuments() {
    for (var style in fontAwesomeFiles) {
        try {
            loadXMLDocument(style);
        }
        catch (err) {
            console.error('Failed to load style ' + style + ': ' + err.message);
        }
    }
}

function loadXMLDocument(style) {
    var file = fontAwesomeFiles[style];

    if (typeof file == "undefined") { throw new Error('Icon Typ ' + style + ' nicht definiert'); }

    const xhr = new XMLHttpRequest();

    xhr.open('GET', file, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            xmlFiles[style] = xhr.responseXML;
        }
    };

    xhr.send();
}

function loadSpecificIcon(iconName, node, style) {
    var xmlDocument = xmlFiles[style];

    if (!xmlDocument) {
        setTimeout(function () { loadSpecificIcon(iconName, node, style); }, 100);
        return;
    }

    var content = xmlDocument.getElementById(iconName);
    if (typeof content === 'undefined' || content === null) {
        content = xmlFiles[0].getElementById(iconName);
        node.setAttribute('fontawesome-style-not-found', '');
    }

    if (content === null && iconName == 'question') { return; }
    if (content === null) {
        loadSpecificIcon('question', node, style);
        return;
    }

    var path = content.innerHTML;

    var viewBox = content.getAttribute('viewBox');

    node.setAttribute('viewBox', viewBox);
    node.setAttribute('fontawesome-icon', iconName);
    node.setAttribute('fontawesome-style', style);
    node.firstElementChild.outerHTML = path;
}

var targetNode = document.body;
var config = { childList: true, subtree: true };

var callback = function (mutationsList, observer) {
    for (var mutation of mutationsList) {
        if (mutation.type !== 'childList') { return; }

        mutation.addedNodes.forEach(function (addedNode) {
            if (addedNode.nodeName === 'svg' && addedNode.firstElementChild.nodeName === 'use') {

                var icon = addedNode.firstElementChild.getAttribute('href');
                if (!icon.startsWith('#')) { return; }

                var style = addedNode.firstElementChild.getAttribute('fontawesome-style');

                loadSpecificIcon(icon.replace('#', ''), addedNode, style);
            }
        });
    }
};

var observer = new MutationObserver(callback);
observer.observe(targetNode, config);