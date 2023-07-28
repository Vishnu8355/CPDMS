require([
  "esri/views/SceneView",
  "esri/views/MapView",
  "esri/widgets/Expand",
  "esri/widgets/BasemapGallery"
],
function(SceneView, MapView, Expand, BasemapGallery) {
  
  var viewOptions = {
    container: "viewDiv",
    map: {
      basemap: "streets-vector"
    },
      center: [78.6569, 22.9734],
    zoom: 3
  };

  // 2D:
  var view = new MapView(viewOptions);

  // 3D:
  // var view = new SceneView(viewOptions);
  
  //var basemapGallery = new BasemapGallery({
  //  view: view
  //});

    basemapGallery = new BasemapGallery({
        view: view
    }, "basemapGalleryDiv");
    basemapGallery.startup();
  var expand = new Expand({
    view: view,
    content: basemapGallery,
    expandTooltip: 'Change Basemap'
  });

    //var layerDropdown = document.querySelector(".layerddl");
    //layerDropdown.appendChild(expand.container);
  //view.ui.add(expand, "top-right");
  
  // Using the "watch" concept
  // https://developers.arcgis.com/javascript/latest/guide/index.html#watching-property-changes
  // to watch the "activeBasemap" property of the BasemapGallery
  // When it changes, close the Expand widget.
  basemapGallery.watch('activeBasemap', function() {
    console.log('changed!');
      expand.expanded = !expand.expanded;

  })
});