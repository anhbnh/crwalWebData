<template>
    <div class="container-fluid">
        <a @click="$router.push('/')"><i class="bi bi-house-fill"></i>Trang Chủ</a>
        <span> > Danh sách thẻ A</span>
        <p>Url : <span>{{Url}}</span></p>
        <div>
            <table class="table table-hover">
             <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">InnerText</th>
                    <th scope="col">Url</th>
                    <th scope="col">XPath</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(atag, rownum) in ATafg">
                    <th scope="col">{{rownum+1}}</th>
                    <th>{{atag.InnerText}}</th>
                    <th>{{atag.Url}}</th>
                    <th>{{atag.XPath}}</th>
                </tr>
            </tbody>
        </table>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            Url_Id: this.$route.params.id,
            Url: this.$route.params.url,
            ATafg: []
        }
    },
    methods: {
        async getData() {
            try {
                const response = await this.$http.get(
                    "api/crawl/GetAllATags?id=" + this.Url_Id
                );
                 this.ATafg = response.data.Data;
            } catch (error) {
                console.log(error);
            }
        },
    },
    created() {
        this.getData();
    },
}
</script>

<style scoped>

</style>
