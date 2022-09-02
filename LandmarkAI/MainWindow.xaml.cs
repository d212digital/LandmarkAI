using LandmarkAI.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image File (*.png; *.jpg) | *.png;*.jpg;*.jpeg | All Files (*.*) |*.* ";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            
            
            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(filename));

                MakePredictionAsync(filename);

            }
        }
        private async void MakePredictionAsync(string filename) 
        {
            string url = "https://uksouth.api.cognitive.microsoft.com/customvision/v3.0/Prediction/9c16761b-c3fc-488e-9fc4-3afc455faa5b/classify/iterations/Iteration1/image";
            string prediction_key = "254fed79ab994991895e9df5e";
            string content_type = "application/octet-stream";
            var file = File.ReadAllBytes(filename);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);
                
                using(var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(content_type);
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions;
                    predictionsListView.ItemsSource = predictions;
                }
            }
        }
    }
}
