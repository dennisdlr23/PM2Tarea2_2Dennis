using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM2E2_2.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2_2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSignatureInformation : ContentPage
    {
        public ShowSignatureInformation(Signatures signatures)
        {
            InitializeComponent();
            txtName.Text = signatures.name;
            txtDescription.Text = signatures.description;
            imageSignature.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(signatures.imageCode)));
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignaturesList());
        }
    }
}