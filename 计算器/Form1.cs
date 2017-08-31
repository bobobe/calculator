using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 计算器
{
    public partial class Form1 : Form
    {
        private string prekey = "z";//记录上一个按键
        private string presignkey = "z";//记录上一个符号键
        private double result = 0;//记录当前结果
        private double reg = 0;//记录当前存储器的值
        //private string tex = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void wprekey(Button b)//记录当前按键到prekey
        {
             prekey = b.Text.ToString();
        }

        private void setonclick(object sender, MouseEventArgs e)
        {
            string tex;
            
            if (label2.Text == "0" && (sender as Button).Text == "0")
            {
                label2.Text = "0";
                return;
            }

            if ((sender as Button).Text.ToString() == ".")
            {
                if (label2.Text.ToString().IndexOf(".") > -1)
                {
                    wprekey(sender as Button);
                    return;
                }
                else
                {
                    if (label2.Text == "0")
                    {
                        label2.Text = "0.";
                        wprekey(sender as Button);
                        return;
                    }
                    
                }
                    
                    
            }
            tex = (sender as Button).Text.ToString();
            if (prekey.ToCharArray()[0] >= 48 && prekey.ToCharArray()[0] <= 57 && prekey.Length < 2 || prekey.Equals("."))
                label2.Text += tex;
            else
                label2.Text = tex;
            wprekey(sender as Button);//记录按键
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label2.Text ="0";
            label3.Text = "";
            result = 0;//当前结果清零
            wprekey(sender as Button);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length != 1)
            {
                if (label2.Text != "")
                    label2.Text = label2.Text.Substring(0, label2.Text.ToString().Length - 1);
            }
            else
                label2.Text = "0";
            wprekey(sender as Button);
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label2.Text);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Text = Clipboard.GetText();
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = Clipboard.GetText();
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label2.Text);
        }

       /* private void button21_Click(object sender, EventArgs e)
        {
            label3.Text += (label2.Text + button21.Text);
            result += double.Parse(label2.Text);
            label2.Text = result.ToString();
            wprekey(button21);
        }*/

        private void button27_Click(object sender, EventArgs e)
        {
            if (label3.Text == "")
                return;
            switch(presignkey)
            {
                case "+": result += double.Parse(label2.Text); break;
                case "-": result -= double.Parse(label2.Text); break;
                case "*": result *= double.Parse(label2.Text); break;
                case "/":
                    if (int.Parse(label2.Text) == 0)
                    {
                        label2.Text = "除数不能为0";
                        return;
                    }
                    else
                    {
                        result /= double.Parse(label2.Text);
                    }
                    break;
            }
            label3.Text = "";
            label2.Text = result.ToString();
            wprekey(button27);

        }

        private void button9_Click(object sender, EventArgs e)//取相反数
        {
            double d = double.Parse(label2.Text);
            d = -d;
            label2.Text = d.ToString();
        }

        private void setclick1(object sender, MouseEventArgs e)
        {
              if (label3.Text == "") //||prekey == "√"||prekey == "1/X")
              {
                 result = double.Parse(label2.Text);
                 //if(label3.Text == "")
                    label3.Text += (label2.Text + (sender as Button).Text);
                 //else
                    //label3.Text += (sender as Button).Text;
                 label2.Text = result.ToString();
                 presignkey = (sender as Button).Text;//记录符号键
                 wprekey(sender as Button);
                 return;
              }

            //result = double.Parse(label2.Text);
            switch(presignkey)
            {
                case "+": result += double.Parse(label2.Text); break;
                case "-": result -= double.Parse(label2.Text); break;
                case "*": result *= double.Parse(label2.Text); break;
                case "/":
                    if (int.Parse(label2.Text) == 0)
                    {
                        label2.Text = "除数不能为0";
                        return;
                    }
                    else
                    {
                        result /= double.Parse(label2.Text);
                    }
                    break;
            }
            if (!prekey.Equals("√") && !prekey.Equals("1/X"))
                label3.Text += (label2.Text + (sender as Button).Text);
            else
                label3.Text += (sender as Button).Text;
            label2.Text = result.ToString();
            presignkey = (sender as Button).Text;//记录符号键
            wprekey(sender as Button);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            wprekey(button11);
        }

        private void setclick2(object sender, MouseEventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "√":
                    label3.Text += ("sqrt(" + label2.Text + ")");
                    label2.Text = Math.Sqrt(Double.Parse(label2.Text)).ToString();
                    break;
                case "%": break;
                case "1/X":
                    if(label2.Text=="0")
                    {
                        label2.Text="除数不能为0";
                        return;
                    }
                    label3.Text += ("reciproc(" + label2.Text + ")");
                    label2.Text = (1 / Double.Parse(label2.Text)).ToString();
                    break;
            }
            wprekey(sender as Button);
        }

        private void button1_Click(object sender, EventArgs e)//清空存储器
        {
            label1.Text = "";
            reg = 0;
            wprekey(button1);
        }

        private void button2_Click(object sender, EventArgs e)//显示存储器数据
        {
            label2.Text = reg.ToString();
            wprekey(button2);
        }

        private void button3_Click(object sender, EventArgs e)//存到存储器数据
        {
            if(!label2.Text.Equals("0"))
            {
                label1.Text = "M";
                reg = Double.Parse(label2.Text);
            }
            wprekey(button3);
        }

        private void button4_Click(object sender, EventArgs e)//存储器加法
        {
            if (!label2.Text.Equals("0"))
            {
                label1.Text = "M";
            }
            reg += Double.Parse(label2.Text);
            wprekey(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!label2.Text.Equals("0"))
            {
                label1.Text = "M";
            }
            reg -= Double.Parse(label2.Text);
            wprekey(button5);
        }
    }
}
