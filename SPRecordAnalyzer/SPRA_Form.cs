using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;

namespace SPRecordAnalyzer
{
    public partial class SPRA_Form : Form
    {
        private Device device;

        public SPRA_Form()
        {
            InitializeComponent();
        }

        private void buttonOpenGPIB_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Intialize the device
                device = new Device((int)0, (byte)numericUpDownAdress.Value, (byte)0);
                SetupControlState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void SetupControlState(bool isSessionOpen)
        {
            numericUpDownAdress.Enabled = !isSessionOpen;
            buttonOpen.Enabled = !isSessionOpen;
            buttonClose.Enabled = isSessionOpen;
            textBoxStringToWrite.Enabled = isSessionOpen;
            buttonWrite.Enabled = isSessionOpen;
            buttonRead.Enabled = isSessionOpen;
            textBoxStringRead.Enabled = isSessionOpen;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                device.Dispose();
                SetupControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            try
            {
                device.Write(ReplaceCommonEscapeSequences(textBoxStringToWrite.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                textBoxStringRead.Text = InsertCommonEscapeSequences(device.ReadString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
