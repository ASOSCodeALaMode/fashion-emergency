var geocoder;

function plotPostcode(postcode, icon) {

    var deferred = $.Deferred();

    geocoder.geocode({ 'address': postcode }, function(results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            var position = { lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() };
            new google.maps.Marker({
                position: position,
                title: storeName,
                map: map,
                animation: google.maps.Animation.DROP,
                icon: icon
            });
            deferred.resolve(position);
        }
    });

    return deferred.promise();
};

function initMap() {
    showMap();
    google.maps.event.addDomListener(window, 'load', function() {

        geocoder = new google.maps.Geocoder;

        $.when(plotPostcode(storePostcode, map.marketplaceStoreImage), plotPostcode(destinationPostcode))
            .done(function(storePosition, destinationPosition) {
                var directionsDisplay = new google.maps.DirectionsRenderer();
                directionsDisplay.setOptions({ suppressMarkers: true });
                var directionsService = new google.maps.DirectionsService();

                directionsService.route({
                    origin: storePosition,
                    destination: destinationPosition,
                    travelMode: google.maps.TravelMode.DRIVING
                }, function(result, status) {
                    if (status === google.maps.DirectionsStatus.OK) {
                        directionsDisplay.setDirections(result);
                        directionsDisplay.setMap(map);
                    }
                });
            });
    });
}
