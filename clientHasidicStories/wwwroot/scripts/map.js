let map;
window.initializeMap = (center, divname, dotNetRef) => {
    maptilersdk.config.apiKey = 'yHCSCgx2fJ24IoOXLGYO';
    map = new maptilersdk.Map({
        container: divname, // container's id or the HTML element to render the map
        style: maptilersdk.MapStyle.TOPO.TOPOGRAPHIQUE,
        center: center, // starting position [lng, lat]
        zoom: 3, // starting zoom
        pitch: 90,
        //bearing: -100.86,
        //maxPitch: 85,
        maxZoom: 14
    });

    map.on('load', function () {
        var popup = new maptilersdk.Popup({
            closeButton: false,
            closeOnClick: false
        });
        map.on('mouseenter', 'points', function (evt) {
            // Change the cursor to a pointer when it enters a feature in the 'points' layer.
            map.getCanvas().style.cursor = 'pointer';

            if (evt.features.length > 0) {
                const feature = evt.features[0];
                var coordinates = feature.geometry.coordinates.slice();
                var name = feature.properties.name;
                var link = feature.properties.link;

                popup.setLngLat(coordinates) // sets the popup's location
                    .setHTML(`<strong>${name}</strong><p style="padding=0;margin:0">click to filter</p><p style="padding=0;margin:0">double click to go there</p>`) // sets the popup's content
                    .addTo(map); // adds the popup to the map
            }
        });

        map.on('mouseleave', 'points', function () {
            // Change it back to the default when it leaves.
            map.getCanvas().style.cursor = '';
            popup.remove();
        });
        map.on('dblclick', 'points', function (evt) {
            if (evt.features.length > 0) {
                const feature = evt.features[0];
                var link = feature.properties.link; // Get the link from the feature's properties

                // Open the link in a new tab
                if (link) { // Check if the link is not undefined or empty
                    window.open(link, '_blank');
                }
            }
        });
        map.on('click', 'points', function (evt) {
            if (evt.features.length > 0) {
                const feature = evt.features[0];
                var id = feature.properties.xmlid;
                dotNetRef.invokeMethodAsync("PointClicked", id)
                    .then(result => { });

                const clickedPointId = id; // Or any unique property from the feature

                // Remove existing pulsing effect if any
                if (window.pulseInterval) clearInterval(window.pulseInterval);

                let pulseRadius = 8; // Starting radius
                let pulseOpacity = 1; // Starting opacity for the clicked point
                const maxRadius = 20;
                const minRadius = 4;
                const minOpacity = 0.1;
                const originalOpacity = 0.5; // Original opacity for the rest of the points
                let expanding = true; // Direction of the pulse effect

                window.pulseInterval = setInterval(() => {
                    if (expanding) {
                        pulseRadius += 0.4; // Increase radius
                        pulseOpacity = Math.max(minOpacity, 1 - ((pulseRadius - minRadius) / (maxRadius - minRadius)));
                        if (pulseRadius >= maxRadius) {
                            expanding = false; // Reverse direction
                            // Immediately reset radius and opacity to original values for the clicked point
                            pulseRadius = minRadius;
                            pulseOpacity = 1;
                        }
                    }

                    // Update the circle-radius for all points, and circle-opacity only for the clicked point
                    map.setPaintProperty('points', 'circle-radius', [
                        "case",
                        ["==", ["get", "xmlid"], clickedPointId], pulseRadius,
                        8 // Default radius for other points
                    ]);
                    map.setPaintProperty('points', 'circle-opacity', [
                        "case",
                        ["==", ["get", "xmlid"], clickedPointId], pulseOpacity,
                        originalOpacity // Original opacity for the rest of the points
                    ]);

                    // If the circle has contracted, restart the expanding phase
                    if (!expanding) {
                        expanding = true;
                    }

                }, 30); // Adjust the interval as needed for smoother animation

            }
        });
    });
}
// Function to update the points layer with new data
window.updatePoints = (data) => {
    // Remove existing pulsing effect if any
    if (window.pulseInterval) clearInterval(window.pulseInterval);

    if (map.getSource('points')) {
        map.removeLayer('points');
        map.removeSource('points');
    }

    map.addSource('points', data);

    map.addLayer({
        'id': 'points',
        'type': 'circle',
        'source': 'points',
        'paint': {
            'circle-radius': 8,
            // Start with an opacity of 0
            'circle-color': '#B42222',
            'circle-opacity': 0
        },
    });

    // Animate the opacity
    let opacity = 0;
    const maxOpacity = 0.5; // Target opacity
    const animationStep = 0.05; // Incremental step
    const interval = 10; // Time in milliseconds between each step

    const intervalId = setInterval(() => {
        opacity += animationStep;
        if (opacity >= maxOpacity) {
            opacity = maxOpacity;
            clearInterval(intervalId); // Stop the animation when the target opacity is reached
        }
        map.setPaintProperty('points', 'circle-opacity', opacity);
    }, interval);
};
