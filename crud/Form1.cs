using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btncadastrar_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Application.StartupPath+@"\empresa.accdb";
            string query = "INSERT INTO funcionarios(nome,nascimento,cpf,salario,telefone,endereço)values(@nome,@nascimento,@cpf,@salario,@telefone,@endereço)";

            OleDbConnection olecon = new OleDbConnection(strcon);

            OleDbCommand comando = new OleDbCommand(query, olecon);

            comando.Parameters.Add("@nome", OleDbType.VarChar).Value = txtnome.Text;
            comando.Parameters.Add("@nascimento", OleDbType.VarChar).Value = txtnascimento.Text;
            comando.Parameters.Add("@cpf", OleDbType.VarChar).Value = txtcpf.Text;
            comando.Parameters.Add("@salario", OleDbType.VarChar).Value = decimal.Parse(txtsalario.Text);
            comando.Parameters.Add("@telefone", OleDbType.VarChar).Value = txttelefone.Text;
            comando.Parameters.Add("@endereço", OleDbType.VarChar).Value = txtendereço.Text;

            try
            {
                olecon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                olecon.Close();
            }
        }

        private void btnpesquisar_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\empresa.accdb";
            string query = "select *  from funcionarios where cpf =@cpf";

            OleDbConnection olecon = new OleDbConnection(strcon);

            OleDbCommand comando = new OleDbCommand(query, olecon);

            comando.Parameters.Add("@cpf", OleDbType.VarChar).Value = txtcpfpesquisa.Text;

            try
            {
                if (txtcpfpesquisa.Text == "")
                {
                    throw new Exception("Você precisa digitar um CPF para pesquisar!");
                }

                olecon.Open();

                OleDbDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    throw new Exception("CPF não cadastrado!");
                }
                else
                {
                    dr.Read();

                    txtid.Text = Convert.ToString(dr["id_funcionario"]);
                    txtnome.Text = Convert.ToString(dr["nome"]);
                    txttelefone.Text = Convert.ToString(dr["telefone"]);
                    txtnascimento.Text = Convert.ToString(dr["nascimento"]);
                    txtcpf.Text = Convert.ToString(dr["cpf"]);
                    txtsalario.Text = Convert.ToString(dr["salario"]);
                    txtendereço.Text = Convert.ToString(dr["endereço"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                olecon.Close();
            }
        }

        private void btnalterar_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\empresa.accdb";
            string query = "update funcionarios set nome=@nome,nascimento=@nascimento,cpf=@cpf,salario=@salario,telefone=@telefone,endereço=@endereço where cpf=@cpf";

            OleDbConnection olecon = new OleDbConnection(strcon);

            OleDbCommand comando = new OleDbCommand(query, olecon);

            comando.Parameters.Add("@nome", OleDbType.VarChar).Value = txtnome.Text;
            comando.Parameters.Add("@nascimento", OleDbType.VarChar).Value = txtnascimento.Text;
            comando.Parameters.Add("@cpf", OleDbType.VarChar).Value = txtcpf.Text;
            comando.Parameters.Add("@salario", OleDbType.VarChar).Value = decimal.Parse(txtsalario.Text);
            comando.Parameters.Add("@telefone", OleDbType.VarChar).Value = txttelefone.Text;
            comando.Parameters.Add("@endereço", OleDbType.VarChar).Value = txtendereço.Text;

            try
            {
                olecon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Alteração concluida!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                olecon.Close();
            }
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\empresa.accdb";

            if (MessageBox.Show("Você realmente deseja excluir esse funcionário?",
                "Cuidado!", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação Cancelada!");
            }
            else
            {
                string query = "delete from funcionarios where cpf = @cpf";

                OleDbConnection olecon = new OleDbConnection(strcon);

                OleDbCommand comando = new OleDbCommand(query, olecon);

                comando.Parameters.Add("@cpf", OleDbType.VarChar).Value = txtcpf.Text;

                try
                {
                    olecon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro excluido!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    olecon.Close();
                }
            }
        }
    }
}
