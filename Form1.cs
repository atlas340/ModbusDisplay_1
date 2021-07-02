using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;


namespace ModbusDisplay
{
    public partial class FormModbus : Form
    {
        ModbusClient modbusClient = new ModbusClient("COM8");
        public double readA, readB;
        public FormModbus()
        {
            InitializeComponent();
        }

        private void FormModbus_Load(object sender, EventArgs e)
        {
            modbusClient.Baudrate = 9600;
            modbusClient.Parity = System.IO.Ports.Parity.None;
            modbusClient.StopBits = System.IO.Ports.StopBits.One;
            modbusClient.ConnectionTimeout = 300;
            modbusClient.Connect();
       
            timer1.Interval = 100;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            Read();
            
        }
        private void Read()
        {
            readA = modbusClient.ReadHoldingRegisters(8, 1)[0]*0.1;

            aGauge1.Value = Convert.ToInt32(readA);
            label1.Text = readA.ToString("0.0") + "A";
        }

        private void FormModbus_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void aGauge1_ValueInRangeChanged(object sender, ValueInRangeChangedEventArgs e)
        {

        }
    }
}
