import { createRouter, createWebHistory } from 'vue-router';

import AbstractPage from '../components/pages/AbstractPage.vue';
import DevicePage from '../components/pages/DevicePage.vue';
import LoanPage from '../components/pages/LoanPage.vue';
import ReportPage from '../components/pages/ReportPage.vue';
import SystemPage from '../components/pages/SystemPage.vue';
// import ErrorPage from '../components/pages/ErrorPage.vue';

const routes = [
    { path: '/', component: AbstractPage },
    { path: '/device', component: DevicePage },
    { path: '/loan', component: LoanPage },
    { path: '/report', component: ReportPage },
    { path: '/system', component: SystemPage },
    // { path: '/:pathMatch(.*)*', component: ErrorPage },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
