window.highlight = (type, id, isHighlight) => {
    let pbPage = document.querySelector('pb-page');
    let pbView = pbPage.querySelector('pb-view');
    let elements = pbView.shadowRoot.querySelectorAll(type);
    let color = type == 'persname' ? '#F1E5AC' : '#BCC6CC';
    elements.forEach(element => {
        if (element.getAttribute('ref') === '#' + id) {
            if (isHighlight) element.style.backgroundColor = color;
            else element.style.backgroundColor = '';
        }
    });
}