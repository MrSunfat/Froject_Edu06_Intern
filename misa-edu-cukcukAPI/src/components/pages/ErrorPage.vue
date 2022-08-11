<template>
    <div class="error-page d-flex">
        <h1 class="error-page__title">
            Oops
        </h1>
        <h4 class="error-page__subtitle">
            Page Not Found
        </h4>
        <router-link to="/" class="go-home">Đi đến trang Tổng quan</router-link>
    </div>
</template> 

<script>
export default {
    name: "ErrorPage"
}
</script>

<style scoped>
.error-page {
    margin: 0 auto;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100%;
}

.error-page__title {
    font-size: 45px;
    color: var(--error-color);
}

.error-page__subtitle {
    text-transform: uppercase;
    margin-top: 20px;
}

.go-home {
    display: block;
    margin-top: 25px;
    text-decoration: none;
    padding: 12px;
    background: var(--main-color);
    color: #fff;
    border-radius: 4px;
}

</style>