using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        // делегат для манипулирования компонентом Label
        public delegate void DelegateForTime(Label label);
        // поле типа делегата
        DelegateForTime DelTime;
        // поток
        Thread t1;
        bool turn = true; //true = X turn; false = Y turn
        int turn_count = 0;

        public Form1()
        {

            InitializeComponent();
            DelTime = new DelegateForTime(StartTime);// указываем метод делегату
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t1 = new Thread(LabelTime); // создаем поток
            t1.IsBackground = true; // задаем фоновый режим
            t1.Priority = ThreadPriority.Lowest; // указываем самый низкий приоритет
            t1.Start(); // стартуем

        }
        void StartTime(Label label)
        {
            // выводим всегда две цифры
            // (00:00)
            string s = DateTime.Now.Hour.ToString("00");
            s += " : ";
            s += DateTime.Now.Minute.ToString("00");
            s += " : " + DateTime.Now.Second.ToString("00");
            label.Text = s;
        }
        void LabelTime()
        {
            // бесконечный цикл
            while (true)
            {
                Invoke(DelTime, Label);// запускаем метод с главного потока          
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tic Tac Toe by Vladislav Mazur", "About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
                b.Text = "X";
            else
                b.Text = "O";
            turn = !turn;
            b.Enabled = false;
            turn_count++;
            checkForWinner();
        }
        private void checkForWinner()
        {
            bool there_is_a_winner = false;
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && !A1.Enabled)
                there_is_a_winner = true;
            if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && !B1.Enabled)
                there_is_a_winner = true;
            if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && !C1.Enabled)
                there_is_a_winner = true;

            //vertical

            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && !A1.Enabled)
                there_is_a_winner = true;
            if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && !A2.Enabled)
                there_is_a_winner = true;
            if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && !A3.Enabled)
                there_is_a_winner = true;

            //end vertical
            //diagonal
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && !A1.Enabled)
                there_is_a_winner = true;
            if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && !A3.Enabled)
                there_is_a_winner = true;
            //end diagonal

            if (there_is_a_winner)
            {
                disableButtons();
                String winner = "";
                if (turn)
                    winner = "0";
                else
                    winner = "x";
                MessageBox.Show(winner + " Wins ");
            }
            else
            {
                if (turn_count == 9)
                    MessageBox.Show("DRAW");
            }
        }
        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }
            }
            catch { }
        }

    }
}
