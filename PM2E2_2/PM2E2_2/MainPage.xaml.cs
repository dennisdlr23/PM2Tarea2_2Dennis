using PM2E2_2.Model;
using PM2E2_2.View;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM2E2_2
{
    public partial class MainPage : ContentPage
    {
        string valueBase64;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Png);
            // var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
            var mStream = (MemoryStream)image;
            byte[] data = mStream.ToArray();
            valueBase64 = Convert.ToBase64String(data);

            if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace(txtDescription.Text))
            {
                await DisplayAlert("Error", "Se deben llenar los campos de nombre y descripcion", "Aceptar");
            }

            var signatureToSave = new Signatures
            {
                imageCode = valueBase64,
                name = txtName.Text,
                description = txtDescription.Text
            };

            var result = await App.BaseDatos.saveSignature(signatureToSave);

            if (result != 1)
            {
                await DisplayAlert("Error", "Ocurrio un error, porfavor intente de nuevo", "Aceptar");
            }

            await DisplayAlert("Aviso", "Se ha guardado correctamente", "Aceptar");
            limpiar();

        }

        private  void btnClear_Clicked(object sender, EventArgs e)
        {
            PadView.Clear();
            txtDescription.Text = "";
            txtName.Text = "";
        }

        private async void btnViewSignature_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushModalAsync(new SignaturesList());
        }

        public void limpiar()
        {
            PadView.Clear();
            txtDescription.Text = "";
            txtName.Text = "";
        }
    }
}
