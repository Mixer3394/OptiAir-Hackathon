<template>
  <div id="AdminPanel" class="admin-panel" >
    <font-awesome-icon class="icon-btn" icon="bars" @click="hide=!hide" />
    <div v-if="!hide" class="form-group login-form">
        <font-awesome-icon style="float:right; cursor:pointer;" icon="times-circle" @click="hide = true" />
        <div v-if="alert" class="alert alert-danger" role="alert">
            Logowanie nie udało się!
        </div>
        <!-- 
        <div  class="alert alert-success" role="alert">
            Udalo się!
        </div> -->
        <label for="exampleInputPassword1">Login</label>
        <input type="text" class="form-control" v-model="login" placeholder="Admin">
        <label for="exampleInputPassword1">Hasło</label>
        <input type="password" class="form-control" v-model="pass" placeholder="********">
        <button class="btn" @click="tryLogin" >Zaloguj</button>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import Axios from "axios";

const AdminPanel = Vue.extend({
    props: ["measurement", "test"],
  data(){
        return{
            hide:true,
            login:"",
            pass:"",
            alert:false        
        }
  },
  created(){
      this.hide = true; 
  },
  methods:{
    tryLogin(){
        // if(this.login == "admin" && this.pass == "admin"){
          Axios({
            method: 'post',
            url: "https://optiair.azurewebsites.net/api/token/",
            data: {
                    "username": this.login,
                    "password": this.pass
                    },
            headers: {
                'Content-Type': 'application/json'
            },
        }).then((response) => {
            console.log("LOG IN");
            console.log(response.data.token);
            setCookie("login", response.data.token, 14);
            this.$emit("login", response.data.token);
            
        }).catch((error) => {
            // if(error.message.indexOf("400")>=0){
                this.alert = true;
                setTimeout(()=>{
                    this.alert = false;
                },3000);
            // }
        });


          //api/token  body { "username": "admin", "password": "admin"}           
        // }
    }   
  },  
  watch: { 
        measurement: function(newVal, oldVal) {
            this.hide=false;
        }
    }
})

export default AdminPanel;
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.admin-panel{
  position: absolute;
  bottom: 0px;
  left: 0px;
  z-index: 999;
}
.icon-btn{
  float: left;
  margin: 15px;
  cursor:pointer;
}
.login-form{
  position: fixed;
  top:200px;
  width: 300px;
  
  padding: 15px;
  left: calc(50% - 150px);
    background: rgba(0,0,0, 0.3);
    box-shadow: 0px 0px 22px 0px rgba(0,0,0,0.3);
}
</style>
