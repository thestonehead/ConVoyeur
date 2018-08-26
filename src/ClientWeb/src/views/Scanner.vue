<template>
  <div>
    <video id="video" width="300" height="200" style="border: 1px solid gray"></video>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { BrowserQRCodeReader, VideoInputDevice, Result } from "@zxing/library";

@Component({
	components: {}
})
export default class Scanner extends Vue {
	created() {
		const codeReader = new BrowserQRCodeReader();
		codeReader
			.decodeFromInputVideoDevice(undefined, "video")
			.then((result: Result) => console.log(result.getText()))
			.catch(err => console.error(err));
	}
}
</script>


<style lang="scss">
</style>