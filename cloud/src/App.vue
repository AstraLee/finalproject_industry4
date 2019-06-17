<template>
  <v-app>
    <v-content>
      <v-container fill-height fluid class="wrap">
        <DataViewer/>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import DataViewer from "./components/DataViewer";

export default {
  name: "App",
  components: {
    DataViewer
  },
  data() {
    return {
      //
    };
  }
};
</script>

<style lang="scss">
.wrap {
  display:flex;
  justify-content: center;
}
</style>

