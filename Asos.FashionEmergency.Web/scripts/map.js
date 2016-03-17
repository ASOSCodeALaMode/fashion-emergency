var map, center;

function showMap() {
    center = new google.maps.LatLng(51.5233512, -0.08101320000002943);
    map = new google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 11,
        disableDefaultUI: true
    });
    map.marketplaceStoreImage = {
        url: '/images/asos-locator-small.png',
        size: new google.maps.Size(60, 50),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(15, 50)
    };
}