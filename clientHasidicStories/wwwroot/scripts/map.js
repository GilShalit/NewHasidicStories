window.showMap = (center, divname, data) => {
    maptilersdk.config.apiKey = 'yHCSCgx2fJ24IoOXLGYO';
    const map = new maptilersdk.Map({
        container: divname, // container's id or the HTML element to render the map
        style: maptilersdk.MapStyle.TOPO.TOPOGRAPHIQUE,
        center: center, // starting position [lng, lat]
        zoom: 3, // starting zoom
        pitch: 90,
        //bearing: -100.86,
        //maxPitch: 85,
        maxZoom: 14
    });

    map.on('load', async function () {
        // Add an image to use as a custom marker
        map.addSource('points', data);

        map.addLayer({
            'id': 'points',
            'type': 'circle',
            'source': 'points',
            'paint': {
                'circle-radius': 8,
                'circle-color': '#B42222',
                'circle-opacity': 0.5
            },
        });
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

        // Change it back to the default when it leaves.
        map.on('mouseleave', 'points', function () {
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
        }); map.on('click', 'points', function (evt) {
            if (evt.features.length > 0) {
                const feature = evt.features[0];
                var id = feature.properties.xmlid;
                DotNet.invokeMethodAsync("clientHasidicStories", "PointClicked", id)
                    .then(result => { });
            }
        });
    });



    //const marker = new maptilersdk.Marker()
    //    .setLngLat(center)
    //    .addTo(map);

}