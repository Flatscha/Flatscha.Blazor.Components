
export function flipIconPrompt(wrapper) {
    var animationClass = 'flip-icon-prompt';
    try {
        wrapper.addEventListener('animationend', function (event) {
            if (!event.animationName.startsWith('fly-away'))
                return;
            
            this.classList.remove(animationClass);
        });
        wrapper.classList.add(animationClass);
    }
    catch (err) {
        console.log(err.message);
    }
}
