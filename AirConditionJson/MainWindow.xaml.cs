using AirConditionJson.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
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


namespace AirConditionJson
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 解析即时空气质量信息
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string buildAQINow(string json)
        {
            //json.Where
            int start = json.IndexOf("citynow");
            start = start + ("citynow").Length + 2;
            int end = json.IndexOf("}", start);
            return json.Substring(start, end - start + 1);
        }

        /// <summary>
        /// 将即时查询信息转换为AirInfo对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>AirInfo对象</returns>
        private AirInfo buildAirInfoObject(string json)
        {
            AirInfo airInfo = JsonConvert.DeserializeObject<AirInfo>(json);
            return airInfo;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            string cookie;
            string url = "http://web.juhe.cn:8080/environment/air/cityair?city=beijing&key=31c1dfaa5d358fbb428720e7da8add10";
            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("GB2312");


            //string temp = GetHtml(url, out cookie);
            //temp = buildAQINow(temp);
            //AirInfo airInfo = buildAirInfoObject(temp);

            //airInfoStackPanel.DataContext = new AirInfoViewModel(airInfo);

            //city.Content = "北京";


            //WebRequest request = WebRequest.Create(url);


            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;

                IAsyncResult result =
                    (IAsyncResult)request.BeginGetResponse(new AsyncCallback(RespCallback), request);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RespCallback(IAsyncResult ar)
        {
            WebRequest request = (WebRequest)ar.AsyncState;
            // End the operation.
            Stream postStream = request.EndGetResponse(ar).GetResponseStream();
            string htmlContent = new StreamReader(postStream, Encoding.UTF8).ReadToEnd();

            string temp = buildAQINow(htmlContent);
            AirInfo airInfo = buildAirInfoObject(temp);


            Dispatcher.BeginInvoke(
                (Action)delegate()
                {
                    airInfoStackPanel.DataContext = new AirInfoViewModel(airInfo);
                    city.Content = "北京";
                });





        }

        /// <summary>
        /// 查询Html
        /// </summary>
        /// <param name="URL">请求地址</param>
        /// <param name="cookie">cookie</param>
        /// <returns>字符串</returns>
        public string GetHtml(string URL, out string cookie)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();

                string htmlContent = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                cookie = response.Headers.Get("Set-Cookie");

                return htmlContent;
            }
            catch
            {
                cookie = "";
                return "";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;

                StreamReader sr = new StreamReader(filename, Encoding.UTF8);
                //TextBlock1.Text = sr.ReadToEnd();
                TextBlock1.Text = buildAQINow(sr.ReadToEnd());

                AirInfo account = JsonConvert.DeserializeObject<AirInfo>(TextBlock1.Text);

                MessageBox.Show(account.ToString());

                sr.Close();
            }
        }
    }



}
