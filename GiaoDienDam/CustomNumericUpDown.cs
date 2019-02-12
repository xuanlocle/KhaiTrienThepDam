using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDam
{

    public class CustomNumericUpDown : NumericUpDown
    {


        int[] defaultValue;

        public override void UpButton()
        {
           
            if(Tag =="@")
                defaultValue = new int[] { 50, 75, 100, 150, 200 };
            else 
            if (Tag == "!")
                defaultValue = new int[] { 6, 8, 10, 12 };
            else
                defaultValue = new int[] { 14, 16, 18, 20, 22, 25, 28, 32, 40 };


            int value = (int)Value;
            //base.UpButton();
            //MessageBox.Show("Up");
            if (value >= defaultValue.Max())
            {
                Value = defaultValue.Max();
            }
            else if (value <= defaultValue.Min())
            {
                Value = defaultValue.Min();
            }



            if (value >= defaultValue.Min() && value <= defaultValue.Max())
                for (int i = 0; i < defaultValue.Length; i++)
                {
                    if (defaultValue[i] >= (int)Value - 1)
                    {
                        Value = defaultValue[i + 1];
                        BackColor = Color.PaleGreen;
                        break;
                    }
                }
        }
        public override void DownButton()
        {
            //base.DownButton();
            //MessageBox.Show("Down");
            int value = (int)Value;
            if (Tag == "@")
                defaultValue = new int[] { 50, 75, 100, 150, 200 };
            else
            if (Tag == "!")
                defaultValue = new int[] { 6, 8, 10, 12 };
            else
                defaultValue = new int[] { 14, 16, 18, 20, 22, 25, 28, 32, 40 };


            //MessageBox.Show("Up");
            if (value >= defaultValue.Max())
            {
                Value = defaultValue.Max();
            }
            else if (value <= defaultValue.Min())
            {
                Value = defaultValue.Min();
            }



            if (value >= defaultValue.Min() && value <= defaultValue.Max())
                for (int i = 0; i < defaultValue.Length; i++)
                {
                    if (defaultValue[i] >= (int)Value)
                    {
                        Value = defaultValue[i - 1];
                        BackColor = Color.PaleGreen;
                        break;
                    }
                }
        }
    }

}
