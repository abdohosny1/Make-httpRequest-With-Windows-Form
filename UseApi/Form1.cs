using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseApi.Model;

namespace UseApi
{
    public partial class Form1 : Form
    {
        HttpClient httpClient ;

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7030/api/");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
         HttpResponseMessage httpResponse=  httpClient.GetAsync("Students").Result;

            if(httpResponse.IsSuccessStatusCode)
            {
                var res = httpResponse.Content.ReadAsAsync<List<StudentData>>().Result;
                dataGridView1.DataSource = res;
                //comboBox2.DisplayMember = "Name";
                //comboBox2.ValueMember = "Id";
                comboBox2.DataSource = res.Select( es=> es.Id.ToString()).ToList();
            }
            else
            {
                MessageBox.Show("Error");
            }
           var responseDea = httpClient.GetAsync("Departments").Result;
            if (responseDea.IsSuccessStatusCode)
            {
                var res = responseDea.Content.ReadAsAsync<List<DepartmentData>>().Result;
                comboBox1.DisplayMember = "Name";   
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = res;

            }
            else
            {
                MessageBox.Show("error GetAll", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newStudent=new StudentData()
            {
                Age =int.Parse(txt_age.Text),
                City=txt_city.Text,
                Name=txt_name.Text,
                DeptId=(int)comboBox1.SelectedValue,

            };
         var res=   httpClient.PostAsJsonAsync("Students", newStudent).Result;

            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Success Add", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_city.Text = txt_name.Text = txt_age.Text = "";
                Form1_Load(null,null);
            }
            else
            {
                MessageBox.Show("error Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // int id=(int)comboBox2.SelectedValue;
            int id = int.Parse(comboBox2.Text);

            var res = httpClient.DeleteAsync($"Students/{id}").Result;

            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Success Delete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1_Load(null, null);

            }
            else
            {
                MessageBox.Show("error delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  int id =(int) comboBox2.SelectedValue;
            int id = int.Parse(comboBox2.Text);

            var response = httpClient.GetAsync($"Students/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<StudentData>().Result;

                //  res.Age = int.Parse(txt_age.Text);
                txt_age.Text=res.Age.ToString();
                txt_name.Text=res.Name.ToString();
                txt_city.Text=res.City.ToString();
                comboBox1.SelectedValue = res.DeptId;

               // res.DeptId = comboBox1.SelectedIndex;



            }
            else
            {
                MessageBox.Show("error Get by Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox2.Text);

            var response = httpClient.GetAsync($"Students/{id}").Result;

           
                var findStudemt = response.Content.ReadAsAsync<StudentData>().Result;

                findStudemt.Age = int.Parse(txt_age.Text);
                findStudemt.Name = txt_name.Text;
                findStudemt.City=txt_city.Text;
                findStudemt.DeptId = (int)comboBox1.SelectedValue;
            

            

          

            var res = httpClient.PutAsJsonAsync($"Students/{id}", findStudemt).Result;

            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Success Update", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1_Load(null, null);

            }
            else
            {
                MessageBox.Show("error delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

