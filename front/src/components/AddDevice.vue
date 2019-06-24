<template>
  <div id="AddDevice" class="add-device" >
    <div class="form-group">
        <label for="exampleInputPassword1">Mac Adress</label>
        <input type="text" class="form-control" id="exampleInputPassword1" placeholder="MA:CA:DR:ES:s">
    </div>
  </div>
</template>

<script>
import Vue from "vue";

const Info = Vue.extend({
    props: ["measurement", "test"],
  data(){
        return{
            hide:false        
        }
  },
  created(){
      this.hide = true; 
  },
//   methods:{
//     hideThis(){
        
//     }   
//   }  
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
