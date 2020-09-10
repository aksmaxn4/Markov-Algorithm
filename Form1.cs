using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)//запрет ввода символов кроме a,b и BackSpace
        {
            if (e.KeyChar.ToString() == "a" || e.KeyChar.ToString() == "b" || e.KeyChar == (char)Keys.Back) e.Handled = false;
            else e.Handled = true;
        }

        public void NAM()//запуск НАМ
        {
            FirstRule("*A", "a*");//запуск первого правила со строками, которую хотим заменить(первый аргумент) и которой хотим заменить(второй аргумент)
           
        }

        public void FirstRule(string substring,string rep)//первое правило
        {
            if (textBox2.Text.Contains(substring))//проверка на содержание заменяемой строки
            {
                if (radioButton1.Checked == false)//проверка, как выполняется алгоритм: по шагам или автоматически
                    label5.BackColor = Color.Red;//подсвечивание соответствующего правила
                ChangeAndToStart(substring, rep);//замена заменяемой строки на строку-заменитель
            }
            else SecondRule("*B", "b*");//переход в следующее правило
            //справедливо для всех остальных правил
           
        }
        public void SecondRule(string substring, string rep)//второе правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label6.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else ThirdRule("*", "");
        }
        public void ThirdRule(string substring, string rep)//терминальное правило
        {
            if (textBox2.Text.Contains(substring))
            {
                string original = textBox2.Text;
                int ind = original.IndexOf(substring);
                textBox2.Text = original.Remove(ind, substring.Length).Insert(ind, rep);
                button2.Enabled = false;
                button1.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                if (radioButton1.Checked == false)
                    label7.BackColor = Color.Red;

                Finish();//функция, отвечающая за конец выполнения алгоритма
                return;
            }
            else FourthRule("A2", "");
        }
        public void FourthRule(string substring, string rep)//четвертое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label8.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else FifthRule("B2", "");
        }
        public void FifthRule(string substring, string rep)//пятое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label9.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else SixthRule("a", "A1");
        }
        public void SixthRule(string substring, string rep)//шестое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label10.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else SeventhRule("b", "B1");
        }
        public void SeventhRule(string substring, string rep)//седьмое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label11.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else EigthRule("1A", "A1");
        }
        public void EigthRule(string substring, string rep)//восьмое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label12.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else NinethRule("1B", "B1");
        }
        public void NinethRule(string substring, string rep)//девятое правило
        {
            if (textBox2.Text.Contains(substring)) { if(radioButton1.Checked==false) label13.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else TenthRule("11", "2");
        }
        public void TenthRule(string substring, string rep)//десятое правило
        {
            if (textBox2.Text.Contains(substring)) { if (radioButton1.Checked == false) label14.BackColor = Color.Red; ChangeAndToStart(substring, rep); }
            else EleventhRule();
        }
        public void EleventhRule()//11 правило
        {
            textBox2.Text = textBox2.Text.Insert(0, "*");//вставка в начало строки *
            if (radioButton1.Checked == false)
                label15.BackColor = Color.Red;
            if(radioButton1.Checked==true) NAM();//проверка если поставлен автоматический режим
        }

        public void ChangeAndToStart(string substring,string rep)//функция замены одной строки на другую
        {
            string original = textBox2.Text;
            int ind = original.IndexOf(substring);
            textBox2.Text = original.Remove(ind, substring.Length).Insert(ind, rep);//удление по индексу заменяемой строки и вставка строки-заменителя
            if (radioButton1.Checked == true) NAM();
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length % 2 == 0)//проверка на четное кол-во букв
            {
                textBox2.Text = textBox1.Text;
                label3.Text = "Исходное слово: " + textBox1.Text;//ввод исходного слова
                textBox1.Clear();
                button2.Enabled = true;
                
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                
                label3.Visible = true;
            }
            else MessageBox.Show("Введите четное количество букв");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            label5.BackColor = Color.White;//окрашивание всех правил в белый цвет
            label6.BackColor = Color.White;
            label7.BackColor = Color.White;
            label8.BackColor = Color.White;
            label9.BackColor = Color.White;
            label10.BackColor = Color.White;
            label11.BackColor = Color.White;
            label12.BackColor = Color.White;
            label13.BackColor = Color.White;
            label14.BackColor = Color.White;
            label15.BackColor = Color.White;
            NAM();//запуск алгоритма
        }
        private void Finish()
        {
            if (textBox2.Text.Length % 2 == 0)
            {
                if (MessageBox.Show("Алгоритм завершен. Использовать алгоритм для полученного слова?", "Успех", MessageBoxButtons.OKCancel) == DialogResult.OK)//предложение использовать полученное слово в качестве исходного
                {
                    label3.Text = "Исходное слово: " + textBox2.Text;
                    button2.Enabled = true;
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                }

            }
            else MessageBox.Show("Алгоритм завершен!");
        }
    }
}
