var map, center;
function initMap() {
    google.maps.event.addDomListener(window, 'load', function() {
        var marker = new RichMarker({
            position: center,
            map: map,
            draggable: true,
            content: '<img src="../images/asos-locator.png" class="locator">'
        });
        marker.setShadow('');
    });

    center = new google.maps.LatLng(51.5233512, -0.08101320000002943);
    map = new google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 12
    });
}
