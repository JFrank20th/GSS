using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace SSH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select CL.Cedula, CL.Nombre, A.Fecha, A.Tiempo, A.Saldo,C.Placa, C.Marca from ALQUILER as A inner join CARRO as C on A.Carro_id = C.Carro_id inner join CLIENTE as CL on A.Cliente_id = CL.Cliente_id", cn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            sda.Fill(tabla);
            dataGridView1.DataSource = tabla;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime dtF1 = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
            string f1 = dtF1.ToString("yyyyMMdd");

            DateTime dtF2 = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
            string f2 = dtF1.ToString("yyyyMMdd");

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select CL.Cedula, CL.Nombre, A.Fecha, A.Tiempo, A.Saldo,C.Placa, C.Marca from ALQUILER as A inner join CARRO as C on A.Carro_id = C.Carro_id inner join CLIENTE as CL on A.Cliente_id = CL.Cliente_id where A.Fecha >= '"+f1+"' and A.Fecha <= '"+ f2+ "'", cn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable tabla = new DataTable();
            sda.Fill(tabla);
            dataGridView1.DataSource = tabla;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString);
            cn.Open();
            string cadena = "select top (1) count(*) as Meses from ALQUILER as A group by month(fecha) order by Meses desc";
            SqlCommand comando = new SqlCommand(cadena, cn);
            SqlDataReader registro = comando.ExecuteReader();
            registro.Read();
            textBox1.Text = registro["Meses"].ToString();
            cn.Close();


            cn.Open();
            string cadena2 = "select top (1) count(*) as Dias from ALQUILER as A group by day(fecha) order by dias desc";
            SqlCommand comando2 = new SqlCommand(cadena2, cn);
            SqlDataReader registro1 = comando2.ExecuteReader();
            registro1.Read();
            textBox2.Text = registro1["Dias"].ToString();
            cn.Close();
        }
    }
}
