<template>
  <div id="AddDevice" class="add-device" >
    <div class="form-group">
        <font-awesome-icon style="float:right" icon="times-circle" @click="$emit('close','')" />
        <div v-if="isAlready" class="alert alert-danger" role="alert">
            To urządzenie już jest zarejestrowane!
        </div>
        <div v-if="isDone" class="alert alert-success" role="alert">
            Udalo się!
        </div>
        <label for="exampleInputPassword1">Nazwa</label>
        <input type="text" class="form-control" v-model="name" placeholder="Stocznia">
        <label for="exampleInputPassword1">Adres MAC</label>
        <input type="text" class="form-control" v-model="mcadress" placeholder="MA:CA:DD:RE:SS">
        <button class="btn" @click="sendMac" >Dodaj</button>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";

const Info = Vue.extend({
    props: ["location"],
  data(){
        return{
            isAlready: false,
            isDone: false,
            hide:false,
            mcadress:"",
            name: ""     
        }
  },
  created(){
      this.hide = true; 
  },
  methods:{
    sendMac(){
        // axios.post("https://optiair.azurewebsites.net/api/devices/", {
        //     "mac": "00:0A:E6:3E:FD:E3",
        //     "name": "DUPA SLONIA",
        //     "latitude": 54.517562,
        //     "longitude": 18.534997,
        //     "isVerified": true
        //     });


        console.log(this.$props.location);

        axios({
            method: 'post',
            url: "https://optiair.azurewebsites.net/api/devices/",
            data: {
                    // "mac": "01:0A:E6:3E:FD:E3",
                    "mac": this.mcadress.toUpperCase(),
                    "name": this.name,
                    "latitude": this.$props.location.lat,
                    "longitude": this.$props.location.lng,
                    "isVerified": true
                    },
            headers: {
                'Content-Type': 'application/json'
            },
        }).then((response) => {
            console.log(response);
            
                this.isDone = true;
            setTimeout(()=>{
                this.$emit("close", "");
            }, 2000)
        }).catch((error) => {
            if(error.message.indexOf("400")>=0){
                this.isAlready = true;
                setTimeout(()=>{
                    this.isAlready = false;
                },3000);
            }
        });
    }   
  }  ,
  watch: { 
        measurement: function(newVal, oldVal) {
            this.hide=false;
        }
    },
    beforeDestroy(){
        console.log('DESTROYYYY!!!')
    }
})

export default Info;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.add-device{
    position: absolute;
    top:200px;
    left: calc(50% - 163px);
    z-index: 999;
    color: white;
    width:300px;
    padding: 15px;
    background: rgba(0,0,0, 0.3);
    box-shadow: 0px 0px 22px 0px rgba(0,0,0,0.3);
    text-align: left;
    
}
</style>
