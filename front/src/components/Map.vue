<template>
  <div class="map-container">
      
    <div id="mapid" class="map"> </div>
  </div>
</template>

<script>
import Vue from "vue";
import leaflet from "leaflet";
let smallCircleRadius = 40;
let bigCircleRadius = 300;

//var map = leaflet.map('mapid').setView([51.505, -0.09], 13);

const Map = Vue.extend({
    props:["markers", "asAdmin"],
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
      /// Calculates distance between points in map in metres.
        calculateDistanceInMetres(lat1, lon1, lat2, lon2) {  // generally used geo measurement function
            var R = 6378.137; // Radius of earth in KM
            var dLat = lat2 * Math.PI / 180 - lat1 * Math.PI / 180;
            var dLon = lon2 * Math.PI / 180 - lon1 * Math.PI / 180;
            var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
            Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
            Math.sin(dLon/2) * Math.sin(dLon/2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
            var d = R * c;
            return d * 1000; // meters
        },

/// Calculates interpolated results for list of markers.
        interpolateMeasurements(lat, lon, markersList) {

            let measurements = markersList.map(function(item) {
                return item.measurements[0];
            });
            console.log(measurements);
            let distances = [];

            var i;
            var distSum = 0;
            for (i = 0; i < markersList.length; ++i) {
                let distance = this.calculateDistanceInMetres(lat, lon, markersList[i].latitude, markersList[i].longitude);
                distances.push(distance);
                distSum += distance;
            }

            // var pM1, pM25, pM10, temperature, humidity, pressure;
            var measurement = {
                "pM1" : 0,
                "pM25" : 0,
                "pM10" : 0,
                "temperature" : 0,
                "humidity" : 0,
                "pressure" : 0,
            };
            for (i = 0; i < measurements.length; ++i) {
                measurement.pM1 += measurements[i].pM1 * distances[i];
                measurement.pM25 += measurements[i].pM25 * distances[i];
                measurement.pM10 += measurements[i].pM10 * distances[i];
                measurement.temperature += measurements[i].temperature * distances[i];
                measurement.humidity += measurements[i].humidity * distances[i];
                measurement.pressure += measurements[i].pressure * distances[i];
            }
            measurement.pM1 /= distSum;
            measurement.pM25 /= distSum;
            measurement.pM10 /= distSum;
            measurement.temperature /= distSum;
            measurement.humidity /= distSum;
            measurement.pressure /= distSum;
            return measurement;
        },

        generateAndEmitResults(measurement) {
            let result = [{
                name: "PM 1",
                result: (measurement.reduce((a, b) => a + (b['pM1'] || 0), 0)/measurement.length).toFixed(2) + ' µg/m3'
                // result: +Math.floor(Math.random() * 10) + "µg/m3"
            },{
                name: "PM 2.5",
                result: (measurement.reduce((a, b) => a + (b['pM25'] || 0), 0)/measurement.length).toFixed(2) + ' µg/m3'
                // result: +Math.floor(Math.random() * 250) + "µg/m3"
            },{
                name: "PM 10",
                result: (measurement.reduce((a, b) => a + (b['pM10'] || 0), 0)/measurement.length).toFixed(2) + ' µg/m3'
                // result: +Math.floor(Math.random() * 1000) + "µg/m3"
            },{
                name: "Temperatura",
                result: (measurement.reduce((a, b) => a + (b['temperature'] || 0), 0)/measurement.length).toFixed(2) + '°C'
                // result: +Math.floor(Math.random() * 40) + "°C"
            },{
                name: "Wilgotność",
                result: (measurement.reduce((a, b) => a + (b['humidity'] || 0), 0)/measurement.length).toFixed(2) + '%'
                // result: +Math.floor(Math.random() * 10) + "%"
            },{
                name: "Ciśnienie",
                result: (measurement.reduce((a, b) => a + (b['pressure'] || 0), 0)/measurement.length).toFixed(2) + ' hPa'
                // result: +Math.floor(Math.random() * 75) + " hPa"
            }];
            this.$emit('chooseMarker', result);
        },

      // Click on marker 
      //  this.model.mapMarkers <- here You have all little markers with diametr 40
      // Big one with 300
      // e parammetr is event parammetr, try to find coords and try to make it
      onMarkerClick(e){
            let latitude = e.latlng.lat;
            let longitude = e.latlng.lng;
            console.log('Latitude: ' + latitude + '; Longitude: ' + longitude)
            const list = this.markers.filter((marker) => {
                let distance = this.calculateDistanceInMetres(marker.latitude, marker.longitude, e.latlng.lat, e.latlng.lng);
                return distance <= bigCircleRadius;
            });
            let measurement=[];
            console.log(list.length);
            if(list.length==0)
                this.$emit("hideInfoEmit",true);
            for(let elList of list)
            measurement.push(elList.measurements[0]);
            console.log(measurement);            
            this.generateAndEmitResults(measurement);            
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
                if($(".leaflet-popup-close-button").length>0) $(".leaflet-popup-close-button")[0].click();
            }, false)
            but.innerText = "Dodaj nowe urządzenie";
            this.model.map.on('contextmenu',(e) => {
                // close prev popups
                if(this.$props.asAdmin){
                    
                    console.log(e.latlng);
                    if($(".leaflet-popup-close-button").length>0) $(".leaflet-popup-close-button")[0].click() 
                    but.setAttribute("data-latlng", e.latlng.lat + " " + e.latlng.lng )
                    L.popup()
                    .setLatLng(e.latlng)
                    .setContent(but)
                    .addTo(this.model.map)
                    .openOn(this.model.map);
                
                }
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
                let colorKoef = 300;
                if(markerInfo.measurements[0]!=undefined)
                    colorKoef = 1/((markerInfo.measurements[0].pM1 + markerInfo.measurements[0].pM25 + markerInfo.measurements[0].pM10) / 3 /300)/100;
                console.log(colorKoef);
                let R = Math.floor( 255 * (1 -colorKoef)) //.toString(16);
                console.log("R "+R);
                if(R<16)
                    R="0"+R;
                else
                    R= R.toString(16)

                console.log("R "+R);

                let G = Math.floor( 255 * colorKoef) //.toString(16)
                
                console.log("G "+G);
                if(G<16)
                    G="0"+G;
                else
                    G= G.toString(16)
                
                console.log("G "+G);

                let color = '#'+R  +G  +'77';
                console.log(color);
                var circle = L.circle([markerInfo.latitude, markerInfo.longitude], {
                    color: 'red',
                    fillColor: color,
                    stroke:false,
                    fillOpacity: 0.5,
                    radius: 300,
                }).addTo(this.model.map);
            }
            this.model.map.on('click', this.onMarkerClick);
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
