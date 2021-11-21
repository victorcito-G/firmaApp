using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SignaturePad.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace firmaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignatureSample : ContentPage
    {
        public object CachedFileManager { get; private set; }

        public SignatureSample()
        {
            InitializeComponent();

            
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                lblBase64Value.Text = base64Val;
                imgSignature.Source = ImageSource.FromStream(() => mStream);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
 

        private async void btnlista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync (new Views.ListaFirmasPage());
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {

            var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
            var mStream = (MemoryStream)image;
            byte[] data = mStream.ToArray();
            string base64Val = Convert.ToBase64String(data);
            lblBase64Value.Text = base64Val;
            imgSignature.Source = ImageSource.FromStream(() => mStream);
            //var sandia = new Xamarin.Forms.ImageSource(data);



            try
            {
                var firmas = new Models.Firmas
                {
                    //latitud = Convert.ToDouble(this.txtlatitud.Text),
                    //longitud = Convert.ToDouble(this.txtlongitud.Text),
                    name = this.txtnombre.Text,
                    descripcion = this.txtdescrip.Text,
                    //MiImagen = imgSignature.Source = ImageSource.FromStream(() => mStream)

                    MiImagen = data

                };

                var resultado = await App.Basedatos03.GrabarFirma(firmas);


                if (resultado == 1)
                {
                    await DisplayAlert("Mensaje", "Ingresada Exitosamente", "OK");
                    txtnombre.Text = "";
                    txtdescrip.Text = "";
                    
                }
                else
                {
                    await DisplayAlert("Mensaje", "Error, No se logro guardar Ubicacion", "OK");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message.ToString(), "OK");

            }
        }

        private async void btnmemoria_Clicked(object sender, EventArgs e)
        {
            var directory = new Java.IO.File("/mnt/sdcard", "ImagenesFirmas").ToString();

            

            //Me fijo si no existe la carpeta "ImagenesFirmas" y la creo
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //Combino los directorios (la ruta de la carpeta y el nombre de la imagen)
            var path = System.IO.Path.Combine(directory, "FirmaTest.jpg");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Aquí debería guardar la imagen
            //using (var bitmap = await signature.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 3f)) //Aquí es donde se queda trabado el método
            //using (var dest = File.OpenWrite(path))
            //{
            //    await bitmap.CopyToAsync(dest);
            //}
            //Aquí debería guardar la imagen
            var bitmap = await signature.GetImageStreamAsync(SignatureImageFormat.Png, Color.Black, Color.White, 1f);
            var dest = System.IO.File.OpenWrite(path);
            bitmap.CopyToAsync(dest);
        }

        private async void btnmemoria2_Clicked(object sender, EventArgs e)
        {

        }

        public async void SaveImage(string filepath)
        {
    //        var imageData = System.IO.File.ReadAllBytes(filepath);
    //        var filename = System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

    //        if (Device.Idiom == TargetIdiom.Desktop)
    //        {
    //            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
    //            savePicker.SuggestedStartLocation =
    //                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
    //            savePicker.SuggestedFileName = filename;
    //            savePicker.FileTypeChoices.Add("JPEG Image", new List<string>() { ".jpg" });

    //            var file = await savePicker.PickSaveFileAsync();

    //            if (file != null)
    //            {
    //                CachedFileManager.DeferUpdates(file);
    //                await FileIO.WriteBytesAsync(file, imageData);
    //                var status = await CachedFileManager.CompleteUpdatesAsync(file);

    //                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
    //                    System.Diagnostics.Debug.WriteLine("Saved successfully"));
    //            }
    //        }
    //        else
    //        {
    //            StorageFolder storageFolder = KnownFolders.SavedPictures;
    //            StorageFile sampleFile = await storageFolder.CreateFileAsync(
    //                filename + ".jpg", CreationCollisionOption.ReplaceExisting);
    //            await FileIO.WriteBytesAsync(sampleFile, imageData);
    //        }
       }
    }
}