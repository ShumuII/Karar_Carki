using System.Diagnostics;
using System.Security.Cryptography;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Policy;

namespace KararCarki
{
    public partial class Form1 : Form
    {



        private String lastUrl;
        int randomNum;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {

            if (!(guna2ComboBox1 == null))
            {

                for (int i = 0; i < 40; i++)
                {
                    await Task.Delay(100);
                    randomNum = RandomNumberGenerator.GetInt32(guna2ComboBox1.Items.Count);
                    label2.Text = guna2ComboBox1.Items[randomNum].ToString();
                }








                guna2ComboBox1.SelectedIndex = randomNum;
                String selectedOne = guna2ComboBox1.Items[guna2ComboBox1.SelectedIndex].ToString();
                selectedOne.Replace(' ', '+');
                String izle = "+izle";
                String dynamicUrl = "https://yandex.com.tr/search/?text=";
                lastUrl = dynamicUrl + selectedOne + izle;



                
                this.Width = 720;

                string titleN, yearN, posterN, urlm;

                apiOp api0p = new apiOp();
                urlm = api0p.createUrl(selectedOne);
                (titleN, yearN, posterN) = await api0p.taskname(urlm);

                label5.Text = titleN;
                label6.Text = yearN;
                guna2PictureBox1.ImageLocation = posterN;







            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                guna2ComboBox1.Items.Add(guna2TextBox1.Text);
                Label labeltext = new Label();
                labeltext.Text = guna2TextBox1.Text;
                labeltext.BackColor = Color.FromArgb(100, 255, 255, 255);
                labeltext.ForeColor = Color.White;
                labeltext.Font = new Font("Arial", 10, FontStyle.Bold);
                labeltext.AutoSize = false;
                labeltext.Size = new Size(200, 40);
                labeltext.Padding = new Padding(5);
                flowLayoutPanel1.Controls.Add(labeltext);
                guna2TextBox1.Clear();
                e.SuppressKeyPress = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(lastUrl) { UseShellExecute = true });
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(lastUrl) { UseShellExecute = true });
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_3(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_SizeChanged(object sender, EventArgs e)
        {

        }
    }

    public class SelectResult
    {
        public string Title {  get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
    }

    public class Selected
    {
        public List<SelectResult> Search { get; set; }
    }

    public class apiOp
    {
        public string createUrl(string movie)
        {
            movie = movie.Replace(" ", "+");
            return "https://www.omdbapi.com/?s=" + movie + "&type=movie&apikey=735f715e";
        }

        public async Task<(string title, string year, string poster)> taskname(string url)
        {
            using (HttpClient client = new HttpClient()) {
                try
                {
                    string jsonString = await client.GetStringAsync(url);

                    Selected response = JsonConvert.DeserializeObject<Selected>(jsonString);

                    return (
                    response.Search.First().Title,
                    response.Search.First().Year,
                    response.Search.First().Poster
                    );
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluþtu: {ex.Message}");
                    return (null, null, null);
                }
            }
        }
    }
}

