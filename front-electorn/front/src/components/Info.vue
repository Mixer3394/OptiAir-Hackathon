<template>
  <div id="info-block" v-bind:class="[hide ? 'hidden' : '', 'info-block']">
    <div class="header">
        <h3>Informacja: <font-awesome-icon style="float:right" icon="times-circle" @click="hide=true" /></h3>        
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
    </div>
  </div>
</template>

<script>
import Vue from "vue";

const Info = Vue.extend({
    props: ["measurement", "hidethis"],
  data(){
        return{
            hide:false        
        }
  },
  created(){
      this.hide = true; 
  },
  watch: { 
        measurement: function(newVal, oldVal) {
            this.hide=false;
        },
        hidethis(newVal, oldVal){
            if(oldVal)
                this.hide=true;
        }
    }
})

export default Info;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.info-block{
    position: absolute;
    width:300px;
    top:0px;
    right: 0px;
    z-index: 999;
    margin: 15px;
    background: rgba(0,0,0, 0.3);
    box-shadow: 0px 0px 22px 0px rgba(0,0,0,0.3);
    text-align: left;
    opacity: 1;
    color: white;
    transition: opacity 1s ease-in-out; 
}
.header h3{
    padding: 10px 0px;
    margin: 0px 15px;
}
table{
        margin: 0px 15px;
        width: calc(100% - 30px);
}
</style>
