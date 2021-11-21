using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace firmaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaFirmasPage : ContentPage
    {
        public ListaFirmasPage()
        {
            InitializeComponent();

            
        }

        // mostras lista de personas 

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var listafirmas = await App.Basedatos03.ObtenerListaFirmas();
            //lstFirmas.ItemsSource = listafirmas;
            Lista.ItemsSource = listafirmas;
        }

        private void lstFirmas_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}