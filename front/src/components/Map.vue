<template>
  <div class="map-container">
      
    <div id="mapid" class="map"> </div>
  </div>
</template>

<script>
import Vue from "vue";
import leaflet from "leaflet";

//var map = leaflet.map('mapid').setView([51.505, -0.09], 13);

const Map = Vue.extend({
    props:["markers"],
  data(){
        return{
            model:{
                map: undefined,
                markers: [{
                    x: 54.52632533704946,  
                    y: 18.507049403926203
                }],
                mapMarkers:[]
                
            }
            
        }
  },
  mounted(){        
        this.mapCreation();
        this.mapInition();
  },
  methods:{
      // Click on marker 
      //  this.model.mapMarkers <- here You have all little markers with diametr 40
      // Big one with 300
      // e parammetr is event parammetr, try to find coords and try to make it
      onMarkerClick(e){
            const list = this.markers.filter((marker) => {
                return marker.latitude== e.latlng.lat && marker.longitude== e.latlng.lng;
            });

            let result = [{
                name: "PM 1",
                result: +Math.floor(Math.random() * 10) + "µg/m3"
            },{
                name: "PM 2.5",
                result: +Math.floor(Math.random() * 250) + "µg/m3"
            },{
                name: "PM 10",
                result: +Math.floor(Math.random() * 1000) + "µg/m3"
            },{
                name: "Temperatura",
                result: +Math.floor(Math.random() * 40) + "°C"
            },{
                name: "Wilgotność",
                result: +Math.floor(Math.random() * 10) + "%"
            },{
                name: "Ciśnienie",
                result: +Math.floor(Math.random() * 75) + " hPa"
            }];
            this.$emit('chooseMarker', result);
          console.log(list);
      },
      mapCreation(){
            let locationCookie = getCookie("location");
            let location = [52.192753,  21.285887];
            if(locationCookie!=undefined && locationCookie!="")
                location = [locationCookie.split(" ")[0],  locationCookie.split(" ")[1]]
            
            this.model.map  = leaflet.map('mapid').setView(location, 13);            
            
            leaflet.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1Ijoib3B0aWFpciIsImEiOiJjang3Zm8zZmowOTZhM3pwN2s4NXAxbHRmIn0.agJ-Rbbb3lqwyniGkGSiWA', {
                attribution: 'Powered by © <a href="https://www.optinav.pl/">OptiNav</a> brains',
                maxZoom: 18,
                id: 'mapbox.streets',
                accessToken: 'your.mapbox.access.token'
            }).addTo(this.model.map);


            /*
              Context Menu to add new device
             */

            var popup = L.popup().setContent('<p>Hello world!<br />This is a nice popup.</p>'); 
            let but = document.createElement("button");
            but.setAttribute("class", "btn ");
            let _th = this;
            but.addEventListener("click", function(e){
                let atr = this.getAttribute("data-latlng").split(" ");
                _th.$emit("AddDevice", {lat: atr[0], lng: atr[1]});
            }, false)
            but.innerText = "Dodaj nowe urządzenie";
            this.model.map.on('contextmenu',(e) => {
                // close prev popups
                console.log(e.latlng);
                if($(".leaflet-popup-close-button").length>0) $(".leaflet-popup-close-button")[0].click() 
                but.setAttribute("data-latlng", e.latlng.lat + " " + e.latlng.lng )
                L.popup()
                .setLatLng(e.latlng)
                .setContent(but)
                .addTo(this.model.map)
                .openOn(this.model.map);
            });
      },
      mapInition(){
          navigator.geolocation.getCurrentPosition((position) =>{
              if(this.model.map != undefined){
                    setCookie("location", position.coords.latitude+" "+position.coords.longitude, 14);
                    this.model.map.flyTo({lat:position.coords.latitude, lng:position.coords.longitude});
                  
              }
          })
      }
  },
  watch: { 
        markers: function(newVal, oldVal) {
                for(let markerElement of this.model.markers){
                this.model.map.removeLayer(markerElement);
            }
            this.model.mapMarkers= [];


            
            for(let markerInfo of newVal){
                let color = '#'+Math.floor(Math.random() * 256).toString(16)  +Math.floor(Math.random() * 256).toString(16)  +'77';
                console.log(color);
                var circle = L.circle([markerInfo.latitude, markerInfo.longitude], {
                    color: 'red',
                    fillColor: color,
                    stroke:false,
                    fillOpacity: 0.5,
                    radius: 300,
                }).addTo(this.model.map).on('click', this.onMarkerClick);

            }
            for(let markerInfo of newVal){
                
                let mcircle = L.circle([markerInfo.latitude, markerInfo.longitude], {
                    color: 'gray',
                    fillColor: '#116',
                    stroke:false,
                    fillOpacity: 0.5,
                    radius: 40
                }).addTo(this.model.map) //.on('click', this.onMarkerClick);
                this.model.mapMarkers.push(mcircle);
            }
                    
        }
    }  
})

export default Map;

//  {
//   name: 'HelloWorld',
//   props: {
//     msg: String
//   },
//   created(){
//       console.log("jestem");
//   }
// }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.map, .map-container{
    height: 100%;
}

</style>
