
export function addFlatschaKeyboardEvents(keyboard, className, onlyTouch, alwaysActive) {
    if (onlyTouch && navigator.maxTouchPoints <= 0) { return; }

    keyboard.tabIndex = 0;
    var keyboardParts = keyboard.getElementsByTagName("*")
    Array.prototype.forEach.call(keyboardParts, function (el) {
        el.tabIndex = 0;
    });

    var inputs = document.getElementsByClassName(className);

    Array.prototype.forEach.call(inputs, function (inputElement) {
        addFlatschaKeyboardEventsToInputElement(keyboard, inputElement, alwaysActive);
    });
}

function addFlatschaKeyboardEventsToInputElement(keyboard, inputElement, alwaysActive) {
    if (!isTextInput(inputElement) && !isTextArea(inputElement)) { return; }

    inputElement.addEventListener('focusout', (event) => {
        if (keyboard.contains(event.relatedTarget)) {
            inputElement.focus();
            return;
        }

        if (alwaysActive) { return; }

        keyboard.classList.remove('active');
    });

    if (alwaysActive) { return; }

    inputElement.addEventListener('focus', (event) => {
        setTimeout(function () {
            if (keyboard.classList.contains('active')) { return; }

            keyboard.classList.add('active')
        }, 10);
    });
}

function isTextInput(el) {
    let tagName = el.tagName;
    if (tagName === "INPUT") {
        let validType = ['text', 'password', 'number', 'email', 'tel', 'url', 'search', 'date', 'datetime', 'datetime-local', 'time', 'month', 'week'];
        let elType = el.type;
        return validType.includes(elType);
    }
    return false;
}

function isTextArea(el) {
    return el.tagName === "TEXTAREA";
}

export function keyPressed(key) {
    var input = document.activeElement;

    if (!isTextInput(input) && !isTextArea(input)) { return; }

    var original = input.value;
    var position = input.selectionStart;
    var newValue = original.slice(0, position) + key + original.slice(position);

    setValueOfElementWithEvent(input, newValue);

    input.setSelectionRange(position + key.length, position + key.length);
}

export function returnPressed() {
    var input = document.activeElement;

    if (!isTextInput(input) && !isTextArea(input)) { return; }

    var position = input.selectionStart;
    if (position <= 0) { return; }

    var original = input.value;

    var newValue = original.slice(0, position - 1) + original.slice(position);
    setValueOfElementWithEvent(input, newValue);

    input.setSelectionRange(position - 1, position - 1);
}

export function enterPressed(keyboard) {
    var input = document.activeElement;

    if (isTextArea(input)) {
        keyPressed('\n');
        return;
    }

    if (!isTextInput(input)) { return; }

    input.dispatchEvent(new Event('change', { bubbles: true }));
    keyboard.classList.remove('active');
}

export function clearPressed() {
    var input = document.activeElement;

    if (!isTextInput(input) && !isTextArea(input)) { return; }

    setValueOfElementWithEvent(input, '');
}

function setValueOfElementWithEvent(el, value) {
    el.value = value;
    el.dispatchEvent(new Event('input', { bubbles: true }));
}