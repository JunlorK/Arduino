using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace T1
{
    public partial class Form1 : Form
    {
        private static int turn = 0;
        public Form1()
        {
            InitializeComponent();
        }
        delegate void SetTextCallback(string text);
        string data = "";
        private void GetText(string text) 
         { 
              if (this.label1.InvokeRequired) 
              { 
                  SetTextCallback d = new SetTextCallback (GetText); 
                  this.Invoke(d, new object [] { text }); 
              } 
              this.label1.Text= text; 
            ////
         } 
        // Read/Update to textbox
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            serialPort1.PortName = comboBox1.Text;
            serialPort1.BaudRate = 57600;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;
            serialPort1.Handshake = Handshake.None;
            serialPort1.RtsEnable = true;
            serialPort1.Open();
            serialPort1.Write(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Write("bat");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Write("tat");
        }

        private void serialPort1_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                //string RxString =sp.ReadExisting();
                //GetText(RxString);
                data += sp.ReadExisting();
                Debug.Print(data);
                if (data.Contains('#'))
                {
                    if (turn == 1)
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            label1.Text = data;
                        }));
                    if (data == "Sai pass#")
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            MessageBox.Show(" Sai pass ");
                        }));
                    else
                    if (data == "Dung pass#") 
                    {
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            MessageBox.Show(" Ket noi thanh cong! ");
                        }));
                        turn = 1;    
                    }
                    data = "";
                } 
            }
            catch (System.TimeoutException) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("Dong");
                serialPort1.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
