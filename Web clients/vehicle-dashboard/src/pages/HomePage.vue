<template>
    <div class="main-page">
        <span class="title"> Enter vehicle id </span>
        <input type="number" step="1" min="0" v-model="vehicleId"/>
        <button class="button btn-primary dugme" @click="goToVehicle"> Get data </button>
    </div>
</template>

<script>
export default({
    data() {
        return{
            vehicleId: 0
        }
    },
    methods:
    {
        goToVehicle()
        {
            this.$store.state.current_vehicle = this.vehicleId
            this.$router.push({
                name: "VehiclePage", 
                params: {
                    id: this.$store.state.current_vehicle
                }
            })
        }
    }
})
</script>

<style scoped>
    .main-page
    {
        padding:15px;
        height: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding-top: 50px;
    }

    .dugme
    {
        margin-top:20px;
    }
</style>