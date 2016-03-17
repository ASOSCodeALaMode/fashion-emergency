function initMap() {
    showMap();
    google.maps.event.addDomListener(window, 'load', function() {
        var boutiques = [
            { position: { lat: 51.4926642, lng: -0.12804000000005544 }, title: 'Aluna Mae' },
            { position: { lat: 51.5140259, lng: -0.14757220000001325 }, title: 'Liquor n Poker' },
            { position: { lat: 51.5234921, lng: -0.09517759999994269 }, title: 'Messina Hembry Clothing' },
            { position: { lat: 51.51498280000001, lng: -0.14780719999998837 }, title: 'Never Fully Dressed' },
            { position: { lat: 51.4943604498741, lng: -0.130635678182769 }, title: 'THIRTY8BIRDS' }
        ];
        boutiques.forEach(function(boutique) {
            var markerInfo = $.extend(boutique, { map: map, animation: google.maps.Animation.DROP, icon: map.marketplaceStoreImage });
            new google.maps.Marker(markerInfo);
        });
    });
}
