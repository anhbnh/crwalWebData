import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios';
import VueAxios from 'vue-axios'
import router from './router'

axios.defaults.baseURL = 'http://139.99.43.170:5000/';

const app = createApp(App)
app.use(VueAxios, axios)
app.use(router)
app.provide('axios', app.config.globalProperties.axios)
app.mount('#app')
