<template>
  <div class="info-block">
    <div class="header">
        <h3>General Info</h3>
        <h3>{{test}}</h3>
    </div>
    <div class="tail">

        <table class="table">
            <tbody>
                <tr v-for="(item, index) in measurement" v-bind:key="index" >
                    <td>{{ item.name }}</td>
                    <td>{{ item.result }}</td>
                </tr>
            </tbody>
        </table>

        <!-- <ul class="list-group list-group-flush">
            <li class="list-group-item">{{"PM 1" + model.pm1}}</li>
            <li class="list-group-item">{{"PM 2.5" + model.pm2}}</li>
            <li class="list-group-item">{{"PM 10" + model.pm10}}</li>
            <li class="list-group-item">{{"PM1" + model.pm1}}</li>
        </ul> -->
    </div>
  </div>
</template>

<script>
import Vue from "vue";

//var map = leaflet.map('mapid').setView([51.505, -0.09], 13);

const Info = Vue.extend({
    props: ["measurement", "test"],
//   data(){
//         return{
            
//         }
//   },
//   created(){
//     //   this.model.measurement = this.$props.measurement; 
//   },
//   methods:{
      
//   }  
})

export default Info;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.info-block{
    position: absolute;
    width:300px;
    /* height: 220px; */
    /* background: red; */
    top:0px;
    right: 0px;
    z-index: 999;
    margin: 15px;
    background: rgba(0,0,0, 0.3);
    box-shadow: 0px 0px 22px 0px rgba(0,0,0,0.3);
    
    color: white;
}
.header h3{
    /* border-bottom: 1px solid white; */
    padding: 10px 0px;
    margin: 0px 15px;
}
table{
        margin: 0px 15px;
        width: calc(100% - 30px);
}
</style>
