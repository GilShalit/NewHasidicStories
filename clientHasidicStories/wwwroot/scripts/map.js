window.showMap = (center, divname, data) => {
    maptilersdk.config.apiKey = 'yHCSCgx2fJ24IoOXLGYO';
    const map = new maptilersdk.Map({
        container: divname, // container's id or the HTML element to render the map
        style: maptilersdk.MapStyle.TOPO.TOPOGRAPHIQUE,
        center: center, // starting position [lng, lat]
        zoom: 8, // starting zoom
        pitch: 70,
        //bearing: -100.86,
        maxPitch: 85,
        maxZoom: 14
    });


    //const marker = new maptilersdk.Marker()
    //    .setLngLat(center)
    //    .addTo(map);

}