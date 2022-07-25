
<template>
  <div class="container">
    <div class="row AddLink">
        <div class="col col-10">
            <input type="text" v-model="NewUrl" class="form-control" placeholder="Nhập link"/>
        </div>
        <div class="col col-2">
            <button @click ="SaveUrl" class="btn btn-primary">Lưu</button>
        </div>
    </div>
    <div class="row">
        <table class="table table-hover">
             <thead>
                <tr>
                    <th scope="col" class="text-center">#</th>
                    <th scope="col">Url</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(link, rownum) in Links">
                    <th scope="col" class="text-center">{{rownum+1}}</th>
                    <th>{{link.Link}}</th>
                    <th width="190px">
                        <div class="text-right">
                            <a @click ="DeleteUrl(link.PageId)"><i class="bi bi-trash"></i>Xóa</a> |
                            <a @click="$router.push({ name: 'ATags', params: { id: link.PageId, url: link.Link }})"><i class="bi-eye"></i>Xem dữ liệu</a>
                            <!-- <router-link :to="{ name: 'ATags', params: { id: link.PageId, url: link.Link }}" class="link">Xem dữ liệu</router-link> -->
                        </div>
                    </th>
                </tr>
            </tbody>
        </table>
    </div>
  </div>
</template>

<script>
export default {
    data(){
        return{
            NewUrl: "",
            Links: []
        }
    },
    methods: {
        async getData() {
            try {
                const response = await this.$http.get(
                    "api/crawl/GetAllUrls"
                );
                // JSON responses are automatically parsed.
                 this.Links = response.data.Data;
            } catch (error) {
                console.log(error);
            }
        },

        async SaveUrl(){
            if (this.NewUrl.trim() == "")
            {
                window.alert("Link is not empty!");
                return;
            }
            try {
                const _url = {"PageId": 0, "Link": this.NewUrl}
                let response = await this.$http.post(
                     "api/crawl/CreateUrl",
                     _url
                );
                 // JSON responses are automatically parsed.
               if (response.data.Success == true){
                this.getData();
               }
                 console.log(response.data);
            } catch (error) {
                console.log(error);
            }
        },
        async DeleteUrl(Url_Id){
            if(confirm("Xác nhận xóa?"))
            {
                try {
                    let response = await this.$http.delete(
                        "api/crawl/DeleteUrl?UrlId="+ Url_Id
                    );
                    // JSON responses are automatically parsed.
                if (response.data.Success == true){
                    this.getData();
                }
                } catch (error) {
                    console.log(error);
                }
            }
        }
    },
    created() {
        this.getData();
    },
}
</script>

<style scoped>
.AddLink{
    margin-top: 50px;
    margin-bottom: 10px;
}

</style>
