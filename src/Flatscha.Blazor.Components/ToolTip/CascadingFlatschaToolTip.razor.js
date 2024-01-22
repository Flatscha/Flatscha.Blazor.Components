
export function positionFlatschaToolTip(wrapper, tooltip) {
    var wrapper_rect = wrapper.getBoundingClientRect();

    var quarterRem = 0.25 * parseFloat(getComputedStyle(document.documentElement).fontSize);

    var tipX = wrapper_rect.x + quarterRem;
    var tipY = wrapper_rect.y + wrapper_rect.height + quarterRem;

    tooltip.style.top = tipY + 'px';
    tooltip.style.left = tipX + 'px';

    var tooltip_rect = tooltip.getBoundingClientRect();

    var posX = window.innerWidth - (tooltip_rect.x + tooltip_rect.width);
    if (posX < 0)
        tipX = tipX + posX - quarterRem;
    else if (tooltip_rect.x < 0)
        tipX = tipX - tooltip_rect.x + quarterRem;

    var posY = window.innerHeight - (tooltip_rect.y + tooltip_rect.height);
    if (posY < 0)
        tipY = wrapper_rect.y - tooltip_rect.height - quarterRem;

    tooltip.style.top = tipY + 'px';
    tooltip.style.left = tipX + 'px';
}