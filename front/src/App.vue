<template>
  <div id="app" class="app">
    <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
    <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
    <Map  :markers="markers" @chooseMarker='measurement = $event'/>
    <Info class="d-none d-sm-block"  :measurement="measurement"  />
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'
import Map from "./components/Map";
import Info from "./components/Info";
import axios from "axios";

export default {
  name: 'app',
  components: {
    Map,
    Info
  },
  data(){
    return{
        measurement: [
           {
                name: "Pm1",
                result: "10"
            },{
                name: "Pm2.5",
                result: "25"
            },{
                name: "Pm10",
                result: "14"
            },{
                name: "Temperatura",
                result: "21"
            },{
                name: "Wilgotność",
                result: "5/10"
            },{
                name: "Ciśnienie",
                result: "60 Psi"
            }
        ],
      markers:[]
    }
  },
  computed:{
    // measurement(){
    //   return this.model.measurement;
    // }
  },
  created(){
    setTimeout(()=>{
      this.measurement = [
           {
                name: "Pm2",
                result: "10"
            },{
                name: "Pm2.5",
                result: "25"
            },{
                name: "Pm10",
                result: "14"
            },{
                name: "Temperatura",
                result: "21"
            },{
                name: "Wilgotność",
                result: "5/10"
            },{
                name: "Ciśnienie",
                result: "60 Psi"
            }
        ];
    }, 5000);

    axios.get("https://optiair.azurewebsites.net/api/devices",
    {
      headers: {"Access-Control-Allow-Origin": "*"},
      crossdomain: true,
    })
    .then( response =>{
      console.table(response.data);
      
      //console.table(response.data);
      // let dd = [];
      // let d1 = response.data;
      // for(let dataEl of d1){
      //   dd.push(dataEl);
      //   let el = dd[dd.length-1];
      //   let lt = el.latitude;
      //   //el.latitude = el.longitude;
      // //   dd[dd.length-1].longitude = lt;
      // }
      // console.log("------------");
      // console.log(dd);
      /*
       */
      //this.markers = response.data;
      this.markers = response.data;
    }) 
  },
  mounted(){
    console.log(this.measurement);

    this.measurement = [
           {
                name: "Pm1",
                result: "10"
            },{
                name: "Pm2.5",
                result: "25"
            },{
                name: "Pm10",
                result: "14"
            },{
                name: "Temperatura",
                result: "21"
            },{
                name: "Wilgotność",
                result: "5/10"
            },{
                name: "Ciśnienie",
                result: "60 Psi"
            }
        ];
  }
}
</script>

<style>
.app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /* margin-top: 60px; */  
  height: 100%;
  position: relative;
}
body,html{
  height: 100%;
}
body{
  padding: 0px;
  margin: 0px;
}
path.leaflet-interactive,
.leaflet-marker-icon,
.leaflet-marker-shadow {
  -webkit-animation: fadein 3s; /* Safari, Chrome and Opera > 12.1 */
  -moz-animation: fadein 3s; /* Firefox < 16 */
  -ms-animation: fadein 3s; /* Internet Explorer */
  -o-animation: fadein 3s; /* Opera < 12.1 */
  animation: fadein 3s;
}

@keyframes fadein {
    from { opacity: 0; }
    to   { opacity: 1; }
}

/* Firefox < 16 */
@-moz-keyframes fadein {
    from { opacity: 0; }
    to   { opacity: 1; }
}

/* Safari, Chrome and Opera > 12.1 */
@-webkit-keyframes fadein {
    from { opacity: 0; }
    to   { opacity: 1; }
}

/* Internet Explorer */
@-ms-keyframes fadein {
    from { opacity: 0; }
    to   { opacity: 1; }
}

/* Opera < 12.1 */
@-o-keyframes fadein {
    from { opacity: 0; }
}

</style>
