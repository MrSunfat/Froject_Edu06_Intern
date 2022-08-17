import { createApp } from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import 'devextreme/dist/css/dx.light.css';

const app = createApp(App);
app.use(router);
app.use(store);
app.mount('#app');
