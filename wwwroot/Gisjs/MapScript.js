var Url = "http://gisserver.bih.nic.in/arcgis/rest/services/";
require([
    "esri/config",
    "esri/Map",
    "esri/views/MapView",
    "esri/widgets/Expand",
 
    "esri/widgets/BasemapGallery"


], function (esriConfig, Map, MapView, Expand, BasemapGallery) {


    var viewOptions = {
        container: "viewDiv",
        map: {
            basemap: "streets-vector"
        },
        center: [-98.35, 39.5],
        zoom: 2
    };

    //esriConfig.apiKey = "YOUR_API_KEY";

    const map = new Map({
        //  basemap: "arcgis-imagery"
        basemap: "streets"
    });

    var view = new MapView(viewOptions);

    //const basemapToggle = new BasemapToggle({
    //    view: view,
    //    nextBasemap: "arcgis-topographic"
    //});

   // view.ui.add(basemapToggle, "bottom-right");

    const basemapGallery = new BasemapGallery({
        view: view,
        source: {
            query: {
                title: '"World Basemaps for Developers" AND owner:esri'
            }
        }
    });

    var expand = new Expand({
        view: view,
        content: basemapGallery,
        expandTooltip: 'Change Basemap'
    });
    view.ui.add(expand, "top-right");
    basemapGallery.watch('activeBasemap', function () {
        console.log('changed!');
        expand.expanded = !expand.expanded;
    })
});
        //var StateLayer = new ArcGISDynamicMapServiceLayer(Url + "AdministrativrBoundaries/StateBoundary/MapServer",
        //    {

        //        visible: true

        //    });

