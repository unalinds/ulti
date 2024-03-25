using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace DevExProgramacaoLaura
{
    public partial class NaturalP : Form
    {
        //variaveis
        public int emailF;
        public int cpF;
        public string mensagem;
        public string ValidateCpf;
        public string connetionString;
        public bool novo;
        public int code;
        public string situacao;

        public NaturalP()
        {
            InitializeComponent();
        }

        //conexao com o banco
        SqlConnection conn = new SqlConnection(@"Data Source=CANCUN\SQLEXPRESS;Initial Catalog=exProgramacao;Persist Security Info=True;User ID=sa;Password=zx862");

        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Pfisic";
                SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                SDA.Fill(dt);

                pfisicDataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //botao de salvar
        private void save_Click(object sender, EventArgs e)
        {

            //verificação do email
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(txtEmail.Text, pattern) == false)
            {
                if (txtEmail.Text.All(Char.IsWhiteSpace))
                {
                    emailF = 1;
                    errorProvider7.Clear();
                }
                else
                {
                    emailF = 0;
                    errorProvider7.SetError(this.txtEmail, "Insira outro email");
                    return;
                }
            }

            //verificar o radioButton
            if (rbAtivoF.Checked)
            {
                situacao = rbAtivoF.Text;
            }

            if (rbInativoF.Checked)
            {
                situacao = rbInativoF.Text;
            }

            //caso não seja inserido nada no campo 'nome' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                errorProvider1.SetError(txtName, "Insira um nome");
                return;
            }
            else
            {
                errorProvider1.SetError(txtName, string.Empty);
            }

            //caso não seja inserido nada no campo 'endereço' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(txtAddres.Text.Trim()))
            {
                errorProvider2.SetError(txtAddres, "Insira um endereço");
                return;
            }
            else
            {
                errorProvider2.SetError(txtAddres, string.Empty);
            }

            //caso não seja inserido nada no campo 'estafo' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(cbState.Text.Trim()))
            {
                errorProvider3.SetError(cbState, "Insira um estado");
                return;
            }
            else
            {
                errorProvider3.SetError(cbState, string.Empty);
            }

            //caso não seja inserido nada no campo 'cidade' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(txtCity.Text.Trim()))
            {
                errorProvider4.SetError(txtCity, "Insira uma cidade");
                return;
            }
            else
            {
                errorProvider4.SetError(txtCity, string.Empty);
            }

            //caso não seja inserido nada no campo 'cpf' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(mskCpf.Text.Trim()))
            {
                errorProvider5.SetError(mskCpf, "Insira um cpf");
            }
            else
            {
                errorProvider5.SetError(mskCpf, string.Empty);
            }

            //caso não seja inserido nada no campo 'data' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(mskDate.Text.Trim()))
            {
                errorProvider6.SetError(mskDate, "Insira uma data");
                return;
            }
            else
            {
                errorProvider6.SetError(mskDate, string.Empty);
            }
            //chamar o validador do cpf

            string valor = mskCpf.Text;
            if (ValidateCPF.ValidadateCPF.IsCpf(valor))
            {
                cpF = 1;
                errorProvider8.Clear();
            }
            else
            {
                errorProvider8.SetError(mskCpf, "Insira um cpf valido");
                return;
            }

            // conexao com o banco para o insert
            try
            {
                conn.Open();
                string query = "INSERT INTO Pfisic(name, addres, street, city, streetNumber, cep, state, cpf, rg, cellNumber, landline, email, date, observation, status) VALUES('" + txtName.Text + "','" + txtAddres.Text + "','" + txtStreet.Text + "','" + txtCity.Text + "','" + txtStreetNumber.Text + "','" + mskCep.Text + "','" + cbState.Text + "','" + mskCpf.Text + "','" + mskRg.Text + "','" + mskCellNumber.Text + "','" + mskLandline.Text + "','" + txtEmail.Text + "','" + mskDate.Text + "','" + txtObservation.Text + "','" + situacao + "')";
                SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
                SDA.SelectCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error" + ex);
            }
            finally
            {
                conn.Close();
            }


            //botao de voltar fica verdadeiro
            btnReturn.Enabled = true;

            MessageBox.Show(mensagem, "Registro incluído com sucesso!");

            //novo from
            NaturalP NewForm = new NaturalP();
            NewForm.Show();
            this.Dispose(false);

            //campos bloqueados
            panel2.Enabled = false;
            panel5.Enabled = false;
        }

        //mascara do cpf
        private void cpf_CheckedChanged(object sender, EventArgs e)
        {
            mskCpf.Text = "";
            mskCpf.Mask = " ";
            mskCpf.MaskInputRejected += new MaskInputRejectedEventHandler(cpf_MaskInputRejected);
        }

        private void cpf_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        { }

        //botao de limpar
        private void btnClean_Click(object sender, EventArgs e)
        {
            NaturalP NewForm = new NaturalP();
            NewForm.Show();
            this.Dispose(false);
        }

        //botao de voltar
        private void btnReturn_Click(object sender, EventArgs e)
        {
            var Main = new Main();
            Main.Show();
        }
        public static DataTable DataSource
        {     get;      set;    }

        //botao para inserir
        private void btnCreat_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel4.Enabled = true;
            panel5.Enabled = true;
            btnReturn.Enabled = false;
        }

        //conferir que vai ser apenas numeros no 'numero da rua'
        private void txtStreetNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("este campo aceita somente numero");
            }
        }

        //botao de delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //conexao com o banco para deletar de acordo com o codigo
                conn.Open();
                string query = "DELETE Pfisic WHERE Code = " + txtCode.Text + " ";
                SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
                SDA.SelectCommand.ExecuteNonQuery();

                MessageBox.Show("Cadastro deletado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex);
            }
            finally
            {
                conn.Close();
            }
            LoadData();

            //novo form
            NaturalP NewForm = new NaturalP();
            NewForm.Show();
            this.Dispose(false);
        }
        //botao de update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //liberar o panel
            panel2.Enabled = true;
            panel4.Enabled = true;
            panel5.Enabled = true;
            btnReturn.Enabled = false;

            //pegar informacoes do grid e conexao com o banco
            if (rbAtivoF.Checked)
            {
                situacao = rbAtivoF.Text;
            }

            if (rbInativoF.Checked)
            {
                situacao = rbInativoF.Text;
            }

            if (pfisicDataGridView.SelectedRows.Count > 0)
            {
                int rowIndex = pfisicDataGridView.SelectedRows[0].Index;
                try
                {
                    conn.Open();
                    string query = "UPDATE Pfisic SET  name = @Name, addres = @Addres, street = @Street, city = @City, streetNumber = @StreetNumber, cep = @Cep, state = @State, cpf = @Cpf, rg = @Rg, cellNumber = @CellNumber, landline  = @Landline , email = @Email, date = @Date, observation = @Observation, status = @status WHERE code = @Code";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Code", txtCode.Text);
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Addres", txtAddres.Text);
                        command.Parameters.AddWithValue("@Street", txtStreet.Text);
                        command.Parameters.AddWithValue("@City", txtCity.Text);
                        command.Parameters.AddWithValue("@StreetNumber", txtStreetNumber.Text);
                        command.Parameters.AddWithValue("@Cep", mskCep.Text);
                        command.Parameters.AddWithValue("@State", cbState.Text);
                        command.Parameters.AddWithValue("@Cpf", mskCpf.Text);
                        command.Parameters.AddWithValue("@Rg", mskRg.Text);
                        command.Parameters.AddWithValue("@CellNumber", mskCellNumber.Text);
                        command.Parameters.AddWithValue("@LandLine", mskLandline.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Date", mskDate.Text);
                        command.Parameters.AddWithValue("@Observation", txtObservation.Text);
                        command.Parameters.AddWithValue("@status", situacao);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Registro atualizado com sucesso!");
                        NaturalP NewForm = new NaturalP();
                        NewForm.Show();
                        this.Dispose(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar registro: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                    LoadData(); // Recarregar dados 
                }
            }
            else
            { }
        }

        //quando clicar no grid as informacoes vao para os campos
        private void pfisicDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (pfisicDataGridView.SelectedRows.Count >= 0)
                {
                    txtCode.Text = pfisicDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    txtName.Text = pfisicDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    txtAddres.Text = pfisicDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                    txtStreet.Text = pfisicDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                    txtCity.Text = pfisicDataGridView.SelectedRows[0].Cells[4].Value.ToString();
                    txtStreetNumber.Text = pfisicDataGridView.SelectedRows[0].Cells[5].Value.ToString();
                    mskCep.Text = pfisicDataGridView.SelectedRows[0].Cells[6].Value.ToString();
                    cbState.Text = pfisicDataGridView.SelectedRows[0].Cells[7].Value.ToString();
                    mskCpf.Text = pfisicDataGridView.SelectedRows[0].Cells[8].Value.ToString();
                    mskRg.Text = pfisicDataGridView.SelectedRows[0].Cells[9].Value.ToString();
                    mskLandline.Text = pfisicDataGridView.SelectedRows[0].Cells[10].Value.ToString();
                    mskCellNumber.Text = pfisicDataGridView.SelectedRows[0].Cells[11].Value.ToString();
                    txtEmail.Text = pfisicDataGridView.SelectedRows[0].Cells[12].Value.ToString();
                    mskDate.Text = pfisicDataGridView.SelectedRows[0].Cells[13].Value.ToString();
                    txtObservation.Text = pfisicDataGridView.SelectedRows[0].Cells[14].Value.ToString();
                    situacao = pfisicDataGridView.SelectedRows[0].Cells[15].Value.ToString();
                }
            }
            catch (Exception)
            { }
        }


        private void pfisicDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            // Verifica se o estado da linha é selecionado
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                // Obtém a linha selecionada
                DataGridViewRow selectedRow = e.Row;

                // Itera sobre as células da linha selecionada
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    //Caso o cliente seja ativo a linha vai ficar azul
                    if (situacao == rbAtivoF.Text)
                    {
                        cell.Style.BackColor = Color.LightSkyBlue;
                    }
                    //Caso o cliente seja inativo a linha vai ficar vermelha
                    else if (situacao == rbInativoF.Text)
                    {
                        cell.Style.BackColor = Color.IndianRed;
                    }
                    else
                    { }
                }
            }
        }

        //botao de cancelar
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel4.Enabled = false;
            panel5.Enabled = false;
            btnReturn.Enabled = true;
        }

        private void pfisicBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pfisicBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.exProgramacaoDataSet);
        }

        private void NaturalP_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'exProgramacaoDataSet.Pfisic' table. You can move, or remove it, as needed.
            this.pfisicTableAdapter.Fill(this.exProgramacaoDataSet.Pfisic);

      }
    }
}
