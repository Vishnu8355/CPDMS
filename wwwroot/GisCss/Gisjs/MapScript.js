var map;
var Url = "http://gisserver.bih.nic.in/arcgis/rest/services/";
var DistrictGraphicsLayer;
require(["esri/map", "esri/layers/ArcGISDynamicMapServiceLayer", "esri/dijit/BasemapGallery",  "esri/layers/FeatureLayer", "esri/tasks/QueryTask", "dojo/query", "dojo/domReady!"], function (Map, ArcGISDynamicMapServiceLayer, BasemapGallery, FeatureLayer, QueryTask, query)
{
      map = new Map("map", {
          basemap: "streets",  
          center: [82.99, 25], 
        zoom: 8
      });

    var basemaps = [];
    var blankBase = new esri.dijit.BasemapLayer({
        url: "http://services.arcgisonline.com/arcgis/rest/services/World_Street_Map/MapServer", opacity: 0

    });

    var blankBasemap = new esri.dijit.Basemap({
        layers: [blankBase],
        title: "Blank Map",
        thumbnailUrl: "image/Blank.jpg"

    });
    basemaps.push(blankBasemap);



    var basemapGallery = new BasemapGallery({
        showArcGISBasemaps: true,
        basemaps: basemaps,
        map: map
    }, "basemapGallery");

    basemapGallery.startup();

    basemapGallery.on("error", function (msg) {
        console.log("basemap gallery error:  ", msg);
    });



     var StateLayer = new ArcGISDynamicMapServiceLayer(Url + "AdministrativrBoundaries/StateBoundary/MapServer",
        {

            visible: true

        });
     var DistrictLayer = new ArcGISDynamicMapServiceLayer(Url + "AdministrativrBoundaries/District/MapServer",
         {
             visible: false
         });

       var DistrictFeatureLayer = new FeatureLayer(Url + "AdministrativrBoundaries/District/MapServer/0",
           {
               visible: false
        });




    map.addLayer(StateLayer);
    map.addLayer(DistrictLayer);    
    map.addLayer(DistrictFeatureLayer);

    const DistrictLayercheckbox = document.getElementById('chkDistrict');

    DistrictLayercheckbox.addEventListener('change', (event) => {
        if (event.currentTarget.checked)
        {
            DistrictLayer.setVisibility(true);
        }
        else {
            DistrictLayer.setVisibility(false);
        }
    });

    const ddlDistrict = document.getElementById('ddlDistrict');

    ddlDistrict.addEventListener('change', (event) =>
    {
        var DistrictCode = ddlDistrict.value;
        var stateQueryTask = new esri.tasks.QueryTask(Url + "AdministrativrBoundaries/District/MapServer/0");
        var stateQuery = new esri.tasks.Query();
        stateQuery.returnGeometry = true;
        stateQuery.where = "DIST_CODE ='" + DistrictCode +"'";
        stateQueryTask.execute(stateQuery, ZoomToGeometry);

    });
    function ZoomToGeometry(featureSet) {


        if (featureSet.features.length > 0)
        {

            if (featureSet.geometryType == "esriGeometryPolygon")
            {
                var graphic = featureSet.features[0];
                var color = new dojo.Color([200, 5, 5, 1]);
                color = new dojo.Color([200, 5, 5, 1]);
                graphic.id = 'District'; 
                var selSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_NULL, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, color, 4.0));
                graphic.setSymbol(selSymbol);

                map.graphics.remove(DistrictGraphicsLayer);


                DistrictGraphicsLayer = graphic;
                map.graphics.add(DistrictGraphicsLayer);

                var timeEvent = setTimeout(map.setExtent(featureSet.features[0].geometry.getExtent().expand(2)), 1);
            }
          
        }
     

    }
});





