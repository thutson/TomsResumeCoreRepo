var vueApp = new Vue({
    el: '#vueWorkHistory',
    data() {
        return {
            workHistory: null,
            loading: true,
            errored: false
        };
    },
    mounted() {
        axios
            .get("/api/WorkHistory")
            .then(response => (this.workHistory = response.data))
            .catch(error => {
                console.log(error);
                this.errored = true;
            })
            .finally(setTimeout(() => {
                this.loading = false;
            }, 2000));
    }
})