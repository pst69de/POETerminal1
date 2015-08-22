using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POETerminal1
{
    public partial class FormPWM : Form
    {
        MainForm _main;
        string _node;

        public FormPWM()
        {
            InitializeComponent();
        }

        public MainForm Main
        {
            get { return _main; }
            set { _main = value; }
        }

        public string Node
        {
            get { return _node; }
            set { _node = value; }
        }

        public int FrequencyPWM
        {
            get
            {
                int MainFreq;
                Int32.TryParse(textBoxFreq.Text, out MainFreq);
                return MainFreq;
            }
            set { textBoxFreq.Text = value.ToString(); }
        }

        public int WidthPWM
        {
            get
            {
                int MainWidth;
                Int32.TryParse(textBoxWidth.Text, out MainWidth); return MainWidth;
            }
            set { textBoxWidth.Text = value.ToString(); }
        }

        public int PhasePWM
        {
            get
            {
                int MainPhase; Int32.TryParse(textBoxPhase.Text, out MainPhase);
                return MainPhase;
            }
            set { textBoxPhase.Text = value.ToString(); }
        }

        private void buttonFreqPlus_Click(object sender, EventArgs e)
        {
            int AddFreq;
            int MainFreq;
            if (Int32.TryParse(textBoxFreqPlus.Text, out AddFreq)) 
            {
                if (Int32.TryParse(textBoxFreq.Text, out MainFreq))
                {
                    textBoxFreq.Text = (MainFreq + AddFreq).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\">" + textBoxFreq.Text + "</pwm>");
                }
            }
        }

        private void buttonFreqMinus_Click(object sender, EventArgs e)
        {
            int SubFreq;
            int MainFreq;
            if (Int32.TryParse(textBoxFreqMinus.Text, out SubFreq))
            {
                if (Int32.TryParse(textBoxFreq.Text, out MainFreq))
                {
                    textBoxFreq.Text = (MainFreq - SubFreq).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\">" + textBoxFreq.Text + "</pwm>");
                }
            }
        }

        private void buttonFreqSet_Click(object sender, EventArgs e)
        {
            int MainFreq;
            if (Int32.TryParse(textBoxFreq.Text, out MainFreq))
            {
                _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\">" + textBoxFreq.Text + "</pwm>");
            }
        }

        private void buttonWidthPlus_Click(object sender, EventArgs e)
        {
            int AddWidth;
            int MainWidth;
            if (Int32.TryParse(textBoxWidthPlus.Text, out AddWidth))
            {
                if (Int32.TryParse(textBoxWidth.Text, out MainWidth))
                {
                    textBoxWidth.Text = (MainWidth + AddWidth).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><width>" + textBoxWidth.Text + "</width></pwm>");
                }
            }
        }

        private void buttonWidthMinus_Click(object sender, EventArgs e)
        {
            int SubWidth;
            int MainWidth;
            if (Int32.TryParse(textBoxWidthMinus.Text, out SubWidth))
            {
                if (Int32.TryParse(textBoxWidth.Text, out MainWidth))
                {
                    textBoxWidth.Text = (MainWidth - SubWidth).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><width>" + textBoxWidth.Text + "</width></pwm>");
                }
            }
        }

        private void buttonWidthSet_Click(object sender, EventArgs e)
        {
            int MainWidth;
            if (Int32.TryParse(textBoxWidth.Text, out MainWidth))
            {
                textBoxWidth.Text = (MainWidth).ToString();
                _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><width>" + textBoxWidth.Text + "</width></pwm>");
            }
        }

        private void buttonPhasePlus_Click(object sender, EventArgs e)
        {
            int AddPhase;
            int MainPhase;
            if (Int32.TryParse(textBoxPhasePlus.Text, out AddPhase))
            {
                if (Int32.TryParse(textBoxPhase.Text, out MainPhase))
                {
                    textBoxPhase.Text = (MainPhase + AddPhase).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><phase>" + textBoxPhase.Text + "</phase></pwm>");
                }
            }
        }

        private void buttonPhaseMinus_Click(object sender, EventArgs e)
        {
            int SubPhase;
            int MainPhase;
            if (Int32.TryParse(textBoxPhaseMinus.Text, out SubPhase))
            {
                if (Int32.TryParse(textBoxPhase.Text, out MainPhase))
                {
                    textBoxPhase.Text = (MainPhase - SubPhase).ToString();
                    _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><phase>" + textBoxPhase.Text + "</phase></pwm>");
                }
            }
        }

        private void buttonPhaseSet_Click(object sender, EventArgs e)
        {
            int MainPhase;
            if (Int32.TryParse(textBoxPhase.Text, out MainPhase))
            {
                textBoxPhase.Text = (MainPhase).ToString();
                _main.SerialOut("<pwm node=\"" + _node + "\" id=\"1\"><phase>" + textBoxPhase.Text + "</phase></pwm>");
            }
        }

        private void FormPWM_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
