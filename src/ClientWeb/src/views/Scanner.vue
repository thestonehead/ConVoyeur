<template>
		<div>
		<video id="video" width="100%" height="400" style="border: 1px solid gray"></video>
	<div>
		{{ReadResult}}
	</div>
    <button @click="reset">RESET</button>
	</div>
</template>

<script lang="ts">
import { Component } from "vue-property-decorator";
import Vue from "vue";
import { BrowserQRCodeReader, VideoInputDevice, Result } from "@zxing/library";

@Component({
	components: {}
})
export default class Scanner extends Vue {
	ReadResult: string = "N/A";
	codeReader: BrowserQRCodeReader = new BrowserQRCodeReader();
	mounted() {
		this.read();
	}

	read() {
		this.codeReader
			.decodeFromInputVideoDevice(undefined, "video")
			.then((result: Result) => {
				console.log(result.getText());
                this.ReadResult = result.getText();
                this.showResult();
				this.reset();
			})
			.catch(err => {
				console.error(err);
			});
    }
    
    reset(){
        this.codeReader.reset();
		this.read();
    }

	showResult() {
		this.$modal.show("dialog", {
			title: "QRCode Scanned!",
			text: "Scanned: " + this.ReadResult,
			buttons: [
				{
                    title: "Confirm",
                    default: true,
					handler: () => {
						alert("Woot!");
					}
				},
				{
					title: "Close"
				}
			]
		});
	}
}
</script>


<style lang="scss">
</style>