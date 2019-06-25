<template>
  <div id="app" class="app">
    <Map  @closeAddDevice="isAddDevice=false" :asAdmin="asAdmin" :markers="markers" @chooseMarker='measurement = $event' @AddDevice="addDeviceLocation=$event;isAddDevice=true"/>
    <Info @hide="hideInfo = true"   :measurement="measurement"  />
    <AddDevice :token="token" v-if="isAddDevice" :location="addDeviceLocation" @close="isAddDevice=false" />
    <AdminPanel v-if="!asAdmin" @login="login($event)" />
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'
import Map from "./components/Map";
import Info from "./components/Info";
import AddDevice from "./components/AddDevice";
import AdminPanel from "./components/AdminPanel"

import axios from "axios";

export default {
  name: 'app',
  components: {
    Map,
    Info,
    AddDevice,
    AdminPanel
  },
  data(){
    return{
        isAddDevice:false,
        addDeviceLocation:{},
        asAdmin:false,
        token:"",
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
  methods:{
    login(e){
      console.log("login inner");
        this.asAdmin = true;
        this.token = e;
    }
  },
  created(){
    let login = getCookie("login");
    if(login!=undefined){
      this.asAdmin = true;
      this.token = login; 
    }
    axios.get("https://optiair.azurewebsites.net/api/devices",
    {
      headers: {"Access-Control-Allow-Origin": "*"},
      crossdomain: true,
    })
    .then( response =>{
      console.table(response.data);
      this.markers = response.data;
    }) 
  },
  mounted(){
    console.log(this.measurement);
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
.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}
.leaflet-popup-close-button{
  opacity: 0;
}
</style>
