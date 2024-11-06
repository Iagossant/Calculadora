using Microsoft.VisualBasic.Devices;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        double valor1 = 0, valor2 = 0;
        string operacao, num;
        bool selecionado = false, calculado = false;
        string[] operador = { "+", "-", "x", "÷" };

        public Form1()
        {
            InitializeComponent();
        }
        private void BtnNumerico_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (calculado)
            {
                lblVisor.Text = lblResultado.Text;
                lblResultado.Text = "";
                calculado = false;
                return;
            }
            lblVisor.Text += btn.Text;
            if (selecionado) { PreResultado(); }
        }
        private void BtnOperacao_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            operacao = btn.Text;
            if (selecionado)
            {
                lblVisor.Text = lblResultado.Text;
                lblResultado.Text = "";
                lblVisor.Text += operacao;
                ReceberValor(ref valor1);
                return;
            }
            ReceberValor(ref valor1);
            lblVisor.Text += operacao;
            selecionado = true;
        }
        private void BtnRaiz_Click(object sender, EventArgs e)
        {
            if (!operador.Any(lblVisor.Text.Contains) && lblVisor.Text.Length != 0)
            {
                double raiz = Math.Sqrt(Convert.ToDouble(lblVisor.Text));
                lblResultado.Text = raiz.ToString();
            }
        }
        private void BtnPotencia_Click(object sender, EventArgs e)
        {
            if(lblVisor.Text.Length != 0)
                lblVisor.Text += '²';

            if (selecionado)
                lblVisor.Text += '²';
        }
        private void BtnPonto_Click(object sender, EventArgs e)
        {
            if (!lblVisor.Text.Contains("."))
            {
                lblVisor.Text += '.';
            }
        }
        private void BtnIgual_Click(object sender, EventArgs e)
        {
            Calcular();
        }
        private void BtnApagar_Click(object sender, EventArgs e)
        {
            if (lblVisor.Text.Length != 0)
                lblVisor.Text = lblVisor.Text.Substring(0, lblVisor.Text.Length - 1);
            else
                valor1 = 0;
            if (lblResultado.Text.Length != 0)
                lblResultado.Text = "";
            else
                calculado = false;
            if (!operador.Any(lblVisor.Text.Contains))
                operacao = "";
                selecionado = false;
        }
        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            lblVisor.Text = lblResultado.Text = "";
            valor1 = valor2 = 0;
            operacao = "";
            selecionado = calculado = false;
        }
        private void ReceberValor(ref double valor)
        {
            if (selecionado)
            {
                try
                {
                    string AKG = valor1 + operacao;
                    valor = Convert.ToDouble(lblVisor.Text.Split(AKG));
                    return;
                }
                catch(System.InvalidCastException) { valor = valor1; return; }
            }
            if (double.TryParse(lblVisor.Text, out valor))
            {
                return;
            }
        }
        private void Calcular()
        {
            ReceberValor(ref valor2);
            switch (operacao)
            {
                case "+":
                    lblResultado.Text = (valor1 + valor2).ToString();
                    break;
                case "-":
                    lblResultado.Text = (valor1 - valor2).ToString();
                    break;
                case "x":
                    lblResultado.Text = (valor1 * valor2).ToString();
                    break;
                case "÷":
                    lblResultado.Text = (valor1 / valor2).ToString();
                    break;
            }
            calculado = true;
        }
        private void PreResultado()
        {
            string[] valores = lblVisor.Text.Split(operacao);
            double[] numero = valores.Select(double.Parse).ToArray();
            switch (operacao)
            {
                case "+":
                    lblResultado.Text = (numero[0] + numero[1]).ToString();
                    break;
                case "-":
                    lblResultado.Text = (numero[0] - numero[1]).ToString();
                    break;
                case "x":
                    lblResultado.Text = (numero[0] * numero[1]).ToString();
                    break;
                case "÷":
                    lblResultado.Text = (numero[0] / numero[1]).ToString();
                    break;
            }
            calculado = true;
        }
    }
}