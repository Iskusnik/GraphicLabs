using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2pointsNET4_8
{
    public static class ReturnNumericDialog
    {

        public static int ShowDialog(string text, string value)
        {
            Form prompt = new Form();
            prompt.Width = 224;
            prompt.Height = 105;
            prompt.Text = text;
            Label textLabel = new Label() { Left = 12, Top = 12, Text = text, Width = 20 };
            NumericUpDown inputBox = new NumericUpDown() { Left = 96, Top = 12, Width = 100, Height = 20, Minimum = -100000, Maximum = 10000};
            inputBox.Value = (int)Math.Floor(float.Parse(value));

            Button confirmation = new Button() { Text = "Ok", Left = 12, Top = 39, Width = 184, Height = 23 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return (int)inputBox.Value;
        }
    }
}
