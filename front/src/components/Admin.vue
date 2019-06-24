<template>
  <div id="" >
    
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
    }
})

export default Info;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
