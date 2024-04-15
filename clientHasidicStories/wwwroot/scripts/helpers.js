window.blazorCulture = {
    get: () => {
        value = window.localStorage['BlazorCulture'];
        // Set the direction based on the culture
        document.body.dir = value === 'he-IL' ? 'rtl' : 'ltr';
        return value;
    },
    set: (value) => window.localStorage['BlazorCulture'] = value
};
window.loadCssFile = (href) => {
    var existingLinks = Array.from(document.head.getElementsByTagName('link'));
    if (!existingLinks.some(link => link.href === href)) {
        var newLink = document.createElement('link');
        newLink.rel = 'stylesheet';
        newLink.href = href;
        document.head.appendChild(newLink);
    }
}
//function loadEditionLibraries() {
//    // Remove existing script elements
//    var oldScripts = document.querySelectorAll('script[src="https://unpkg.com/@webcomponents/webcomponentsjs@2.4.3/webcomponents-loader.js"], script[src="https://unpkg.com/@teipublisher/pb-components@latest/dist/pb-components-bundle.js"]');
//    oldScripts.forEach(function (script) {
//        script.parentNode.removeChild(script);
//    });

//    // Create new script elements
//    var libraries = [
//        { src: 'https://unpkg.com/@webcomponents/webcomponentsjs@2.4.3/webcomponents-loader.js', type: 'text/javascript' },
//        { src: 'https://unpkg.com/@teipublisher/pb-components@latest/dist/pb-components-bundle.js', type: 'module' }
//    ];
//    libraries.forEach(function (lib) {
//        var script = document.createElement('script');
//        script.src = lib.src;
//        script.type = lib.type;
//        document.body.appendChild(script);
//    });
//}

//function unloadEditionLibraries() {
//    // Remove existing script elements
//    var oldScripts = document.querySelectorAll('script[src="https://unpkg.com/@webcomponents/webcomponentsjs@2.4.3/webcomponents-loader.js"], script[src="https://unpkg.com/@teipublisher/pb-components@latest/dist/pb-components-bundle.js"]');
//    oldScripts.forEach(function (script) {
//        script.parentNode.removeChild(script);
//    });
//}
