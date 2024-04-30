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
    });


    //const marker = new maptilersdk.Marker()
    //    .setLngLat(center)
    //    .addTo(map);

}