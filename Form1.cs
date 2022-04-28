using Newtonsoft.Json;

namespace WinFormsApp1_REST
{
    public partial class Form1 : Form
    {
        public HttpClient _client;
        public HttpResponseMessage _response;
        public Form1()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://mocki.io/v1/"); //sample api
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Fetching data...";
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var listCS = await GetList();
                dataGridView1.DataSource = listCS;
                label2.Text = "Data load success!";
            }
            catch (Exception ex)
            {
                label2.Text = "Please check network connection! " + ex.Message;
            }
        }

        public async Task<CustomersModel[]> GetList()
        {
            _response = await _client.GetAsync($"d4867d8b-b5d5-4a48-a4ab-79131b5809b8");
            var json = await _response.Content.ReadAsStringAsync();
            var listCS = JsonConvert.DeserializeObject<CustomersModel[]>(json);
            return listCS;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                label2.Text = "";
                //-----------------------------------------------------------------
                File.Delete("tempfile.tmp");

                const string tempfile = "tempfile.tmp";
                System.Net.WebClient webClient = new System.Net.WebClient();

                Console.WriteLine("Downloading file....");

                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                webClient.DownloadFile("http://africau.edu/images/default/sample.pdf", tempfile);
                sw.Stop();

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(tempfile);
                long speed = (sw.Elapsed.Seconds > 0) ? fileInfo.Length / sw.Elapsed.Seconds : fileInfo.Length;

                string s = "";
                //s = s + "Download duration: " + sw.Elapsed;
                //s += " File size: " + fileInfo.Length.ToString("N0");
                s += " Network Speed: " + speed.ToString("N0");
                //-----------------------------------------------------------------
                label1.Text = s + " bps";
            }
            catch (Exception ex)
            {
                label2.Text = "Please check network connection! " + ex.Message;
            }
        }
    }

    public class CustomersModel
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}