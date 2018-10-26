import { BrowserQRCodeReader, VideoInputDevice, Exception } from '@zxing/library';

if (document.getElementById("video") === undefined) {
    throw new Exception("No video element to use.");
}



/*var videoInputDevices = codeReader.getVideoInputDevices();
const firstDeviceId = videoInputDevices[0].deviceId;
*/


async function SetupScanner() {
    const codeReader = new BrowserQRCodeReader();

    const result = await codeReader.decodeFromInputVideoDevice(undefined, 'video');
    CheckScan(result.getText());
    SetupScanner();
        /*.then(result => CheckScan(result.getText()))
        .catch(err => console.error(err))
        .then(r => codeReader.reset());*/
}

function CheckScan(text : string) {
    console.log("Scanned: " + text);
    const spinner = document.getElementsByClassName("spinner")[0];
    spinner.classList.remove("hide");
    //document.getElementById("video").classList.add("hide");

    fetch("./Scanning", {
        method: "post",
        headers: { "Content-type": "application/json" },
        body: JSON.stringify({
            ScannedText: text
        })
    }).then((data) => {
        spinner.classList.add("hide");
        data.text().then(m => alert(m));
        })
}

