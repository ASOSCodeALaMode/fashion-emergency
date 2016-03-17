var map, center;

function debounce(func, wait, immediate) {
    var timeout;
    return function() {
        var context = this, args = arguments;
        var later = function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};

function initMap() {
    google.maps.event.addDomListener(window, 'load', function() {

        var anchor = new google.maps.Size(-1.52, -50);
        var marker = new RichMarker({
            position: center,
            map: map,
            draggable: true,
            content: '<img src="../images/asos-locator.png" class="locator">',
            anchor: anchor
        });
        marker.setShadow('');

        var geocoder = new google.maps.Geocoder;

        google.maps.event.addListener(marker, 'position_changed', debounce(function() {
            var position = marker.getPosition();

            geocoder.geocode({ 'location': { lat: position.lat(), lng: position.lng() } }, function(results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    results.every(function(result) {
                        var address_component = result.address_components.find(function(address_component) {
                            return address_component.types[0] === 'postal_code';
                        });
                        if (address_component) {
                            $('#postcode')[0].value = address_component.long_name;
                            return false;
                        }
                        return true;
                    });
                }
            });
        }, 250));
    });

    center = new google.maps.LatLng(51.5233512, -0.08101320000002943);
    map = new google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 12,
        disableDefaultUI: true
    });
}
