using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;
using UnityEngine.UI;


public class ReadQRCode : MonoBehaviour
{
    public ARCameraManager CameraManager;
    public Text txt;

    public string qrText;

    void Update()
    {
        if (CameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            using (image)
            {

                // read barcode raw data
                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.R8, XRCpuImage.Transformation.MirrorY);
                var dataSize = image.GetConvertedDataSize(conversionParams);
                var grayscalePixels = new byte[dataSize];

                unsafe
                {
                    fixed (void* ptr = grayscalePixels)
                    {
                        image.Convert(conversionParams, new System.IntPtr(ptr), dataSize);
                    }
                }

                // decode barcode
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(grayscalePixels, image.width, image.height, RGBLuminanceSource.BitmapFormat.Gray8);

                // ui output
                if (result != null)
                {
                    qrText = result.Text;
                    txt.text = qrText;
                    // sphere = sphere object
                    // cube = cube object

                }
            }
        }
    }
}
