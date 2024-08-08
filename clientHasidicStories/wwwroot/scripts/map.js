let map;
let circleRadius = 6;
let originalOpacity = 0.5;

window.storyMap = (center, divname, data) => {
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

    map.on('load', () => {
        map.addSource('points', data);

        map.addLayer({
            'id': 'points',
            'type': 'circle',
            'source': 'points',
            'paint': {
                'circle-radius': circleRadius,
                // Start with an opacity of 0
                'circle-color': '#B42222',
                'circle-opacity': 0
            },
        });

        // Add a symbol layer for the point names with a border
        map.addLayer({
            'id': 'point-names',
            'type': 'symbol',
            'source': 'points',
            'layout': {
                'text-field': ['get', 'name'],
                'text-font': ['Open Sans Bold', 'Arial Unicode MS Bold'],
                'text-size': 12,
                'text-offset': [0, 1.5], // Adjust the offset to position the text above the point
                'text-anchor': 'bottom',
                'text-padding': 5, // Add padding around the text
                'text-justify': 'center'
            },
            'paint': {
                'text-color': '#000000',
                'text-halo-color': '#B42222', // Border color
                'text-halo-width': 0.5, // Width of the border
                'text-halo-blur': 1 // No blur for the border
            }
        });

        // Calculate the bounding box for all points
        const coordinates = data.data.features.map(feature => feature.geometry.coordinates);
        const bounds = coordinates.reduce((bounds, coord) => {
            return bounds.extend(coord);
        }, new maptilersdk.LngLatBounds(coordinates[0], coordinates[0]));

        // Fit the map to the bounds
        if (coordinates.length === 1) {
            // If there is only one point, set a default zoom level
            map.setCenter(coordinates[0]);
            map.setZoom(8); // Adjust the zoom level as needed
        } else {
            map.fitBounds(bounds, {
                padding: 40 // Optional padding around the bounds
            });
        }

        // Animate the opacity
        let opacity = 0;
        const maxOpacity = 1; // Target opacity
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

        map.on('mouseenter', 'points', function (evt) {
            // Change the cursor to a pointer when it enters a feature in the 'points' layer.
            map.getCanvas().style.cursor = 'pointer';
        });
        map.on('mouseleave', 'points', function () {
            // Change it back to the default when it leaves.
            map.getCanvas().style.cursor = '';
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
    });
}

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
        //map.on('click', function (evt) {
        //    resetPulsatingPoint();
        //});
        map.on('click', 'points', function (evt) {
            if (evt.features.length > 0) {
                const feature = evt.features[0];
                var id = feature.properties.xmlid;
                dotNetRef.invokeMethodAsync("PointClicked", id)
                    .then(result => { });

                window.lastClickedPointId = id;
                // Remove existing pulsing effect if any
                if (window.pulseInterval) clearInterval(window.pulseInterval);

                let pulseRadius = circleRadius; // Starting radius
                let pulseOpacity = 1; // Starting opacity for the clicked point
                const maxRadius = 15;
                const minRadius = 3;
                const minOpacity = 0.1;
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
                        ["==", ["get", "xmlid"], window.lastClickedPointId], pulseRadius,
                        circleRadius // Default radius for other points
                    ]);
                    map.setPaintProperty('points', 'circle-opacity', [
                        "case",
                        ["==", ["get", "xmlid"], window.lastClickedPointId], pulseOpacity,
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
            'circle-radius': circleRadius,
            // Start with an opacity of 0
            'circle-color': '#B42222',
            'circle-opacity': 0
        },
    });

    // Calculate the bounding box for all points
    const coordinates = data.data.features.map(feature => feature.geometry.coordinates);
    const bounds = coordinates.reduce((bounds, coord) => {
        return bounds.extend(coord);
    }, new maptilersdk.LngLatBounds(coordinates[0], coordinates[0]));

    // Fit the map to the bounds
    map.fitBounds(bounds, {
        padding: 20 // Optional padding around the bounds
    });

    // Animate the opacity
    let opacity = 0;
    const maxOpacity = originalOpacity; // Target opacity
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

window.resetPulsatingPoint = () => {
    // Remove existing pulsing effect if any
    if (window.pulseInterval) {
        clearInterval(window.pulseInterval);
        window.pulseInterval = null; // Clear the interval ID to prevent interference

        // Reset the size and opacity of the pulsating point to original values
        // Assuming the original size is circleRadius and the original opacity is OriginalOpacity
        // You might need to adjust these values based on your initial settings

        // Check if there was a previously clicked point to avoid unnecessary updates
        if (window.lastClickedPointId) {
            map.setPaintProperty('points', 'circle-radius', [
                "case",
                ["==", ["get", "xmlid"], window.lastClickedPointId], circleRadius,
                circleRadius // Default size for other points
            ]);
            map.setPaintProperty('points', 'circle-opacity', [
                "case",
                ["==", ["get", "xmlid"], window.lastClickedPointId], originalOpacity,
                originalOpacity // Original opacity for the rest of the points
            ]);

            // Clear the last clicked point ID
            window.lastClickedPointId = null;
        }
    }
};

