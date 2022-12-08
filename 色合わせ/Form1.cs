using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 色合わせ
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();

        List<string> icons = new List<string>()
            {
                "!","!","N","N",",",",","k","k","b","b","v","v","w","w","z","z"
            };

        private void AssignIconsToSquares()
        {
            //パネルのコントロールを把握？
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    //ラベルにランダムで文字を代入
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    //アイコンを非表示
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
               }
            }
        }

    public Form1()
        {
            InitializeComponent();

            //アイコンをランダムに配置する関数
            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //タイマーが開始しているか確認
            if (timer1.Enabled == true)
                return;

            Label clickLabel = sender as Label;

            if(clickLabel != null)
            {
                if (clickLabel.ForeColor == Color.Black)
                    return;

                if(firstClicked == null)
                {
                    //1クリック目の文字を黒にする
                    firstClicked = clickLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

               //1クリック目の文字を黒にする
                secondClicked = clickLabel;
                secondClicked.ForeColor = Color.Black;

                //勝敗チェック
                CheckForWinnr();

                //１回目と２回目が同じなら保持する
                //異なればIFを抜けてタイマーが開始
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinnr()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                //すべてのラベルを確認し一致しているか判別
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("your all clear");
            Close();
        }
    }
}
