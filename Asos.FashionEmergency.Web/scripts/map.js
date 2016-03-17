var map, center;

function initMap() {
    google.maps.event.addDomListener(window, 'load', function() {
        var boutiques = [
            { position: { lat: 51.4926642, lng: -0.12804000000005544 }, title: 'Aluna Mae' },
            { position: { lat: 51.5140259, lng: -0.14757220000001325 }, title: 'Liquor n Poker' },
            { position: { lat: 51.5234921, lng: -0.09517759999994269 }, title: 'Messina Hembry Clothing' },
            { position: { lat: 51.51498280000001, lng: -0.14780719999998837 }, title: 'Never Fully Dressed' },
            { position: { lat: 51.4943604498741, lng: -0.130635678182769 }, title: 'THIRTY8BIRDS' }
        ];
        var image = {
            url : '../images/asos-locator-small.png',
            size: new google.maps.Size(60, 50),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(0, 15)
        };
        boutiques.forEach(function(boutique) {
            var markerInfo = $.extend(boutique, { map: map, animation: google.maps.Animation.DROP, icon: image });
            new google.maps.Marker(markerInfo);
        });
    });

    center = new google.maps.LatLng(51.5233512, -0.08101320000002943);
    map = new google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 11,
        disableDefaultUI: true
    });

    //new google.maps.Circle({
    //    strokeColor: '#FF3300',
    //    strokeOpacity: 0.8,
    //    strokeWeight: 2,
    //    fillColor: '#FF3300',
    //    fillOpacity: 0.35,
    //    map: map,
    //    center: center,
    //    radius: 16093.4
    //});

    //new google.maps.Circle({
    //    strokeColor: '#FFCC00',
    //    strokeOpacity: 0.8,
    //    strokeWeight: 2,
    //    fillColor: '#FFCC00',
    //    fillOpacity: 0.35,
    //    map: map,
    //    center: center,
    //    radius: 8046.72
    //});
}
