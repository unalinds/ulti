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
using System.IO;
using System.Drawing.Imaging;



namespace DevExProgramacaoLaura
{
    public partial class JuridicalP : Form
    {
        //variaveis

        public int emailJ;
        public int cnpJ;
        public string message;
        public string ValidateCnpj;
        public string connetionString;
        public bool novo;
        public int code;
        public string condition;
        public string image;
        string imgLocation = "";

        public JuridicalP()
        {
            InitializeComponent();
        }

        //conexao com o banco
        SqlConnection conn = new SqlConnection(@"Data Source=CANCUN\SQLEXPRESS;Initial Catalog=exProgramacao;Persist Security Info=True;User ID=sa;Password=zx862");
         SqlCommand  command;
        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Pjuridica";
                SqlDataAdapter SDA = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                SDA.Fill(dt);

                pjuridicaDataGridView.DataSource = dt;
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
        private void Save_Click(object sender, EventArgs e)
        {

            //verificação do email
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(txtEmail.Text, pattern) == false)
            {
                if (txtEmail.Text.All(Char.IsWhiteSpace))
                {
                    emailJ = 1;
                    errorProvider7.Clear();
                }
                else
                {
                    emailJ = 0;
                    errorProvider7.SetError(this.txtEmail, "Insira outro email");
                    return;
                }
                return;
            }

            //verificar o radioButton
            string condition = "";
            if (rbAtivoJ.Checked)
            {
                condition = rbAtivoJ.Text;
            }

            if (rbInativoJ.Checked)
            {
                condition = rbInativoJ.Text;
            }

            foreach (DataGridViewRow linha in pjuridicaDataGridView.Rows)
            {
                if (condition == rbAtivoJ.Text)
                {
                    linha.Cells[0].Style.BackColor = Color.LightSkyBlue;
                }
                else if (condition == rbInativoJ.Text)
                {
                    linha.Cells[0].Style.BackColor = Color.IndianRed;
                }
                else
                { }
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

            //caso não seja inserido nada no campo 'estado' vai retornar um aviso para inserir
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

            //caso não seja inserido nada no campo 'cnpj' vai retornar um aviso para inserir
            if (string.IsNullOrEmpty(mskCnpj.Text.Trim()))
            {
                errorProvider5.SetError(mskCnpj, "Insira um cnpj");
                return;
            }
            else
            {
                errorProvider5.SetError(mskCnpj, string.Empty);
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

            //chamar o validador do cnpj
            string valor = mskCnpj.Text;
            if (ValidateCNPJ.ValidadateCNPJ.IsCnpj(valor))
            {
                cnpJ = 1; errorProvider8.Clear();
            }
            else
            {
                errorProvider8.SetError(mskCnpj, "Insira um cnpj valido");
                return;
            }

            // conexao com o banco para o insert
            try
            {
                conn.Open();
                string query = "INSERT INTO Pjuridica(name, addres, street, city, streetNumber, cep, state, cnpj, ie, cellNumber, landline, email, date, observation,image, status) VALUES('" + txtName.Text + "','" + txtAddres.Text + "','" + txtStreet.Text + "','" + txtCity.Text + "','" + txtStreetNumber.Text + "','" + mskCep.Text + "','" + cbState.Text + "','" + mskCnpj.Text + "','" + mskIe.Text + "','" + mskCellNumber.Text + "','" + mskLandline.Text + "','" + txtEmail.Text + "','" + mskDate.Text + "','" + txtObservation.Text + "','" + condition + "','" + image + "')";
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

            MessageBox.Show(message, "Registro incluído com sucesso!");

            //novo from
            JuridicalP NewForm = new JuridicalP();
            NewForm.Show();
            this.Dispose(false);

            //campos bloqueados
            panel2.Enabled = false;
            panel6.Enabled = false;

        }

        //mascara do cnpj
        private void cnpJ_CheckedChanged(object sender, EventArgs e)
        {
            mskCnpj.Text = "";
            mskCnpj.Mask = "";
            mskCnpj.MaskInputRejected += new MaskInputRejectedEventHandler(cnpj_MaskInputRejected);
        }
        private void cnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        { }

        //botao de limpar
        private void btnClean_Click(object sender, EventArgs e)
        {
            JuridicalP NewForm = new JuridicalP();
            NewForm.Show();
            this.Dispose(false);
        }

        //botao de voltar
        private void btnReturn_Click(object sender, EventArgs e)
        {
            var Main = new Main();
            Main.Show();
        }

        //botao de delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //conexao com o banco para deletar de acordo com o codigo
            try
            {
                conn.Open();
                string query = "DELETE Pjuridica WHERE Code = " + txtCode.Text + " ";
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
            JuridicalP NewForm = new JuridicalP();
            NewForm.Show();
            this.Dispose(false);
        }

        //botao de adicionar cliete
        private void btnCreat_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel4.Enabled = true;
            panel6.Enabled = true;
            btnReturn.Enabled = false;
        }

        public static DataTable DataSource
        { get; set; }

        //botao update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //liberar o panel
            panel2.Enabled = true;
            panel4.Enabled = true;
            panel6.Enabled = true;
            btnReturn.Enabled = false;
            if (pjuridicaDataGridView.SelectedRows.Count > 0)
            {
                //pegar informacoes do grid e conexao com o banco
                int rowIndex = pjuridicaDataGridView.SelectedRows[0].Index;
                try
                {
                    conn.Open();
                    string query = "UPDATE Pjuridica SET  name = @Name, addres = @Addres, street = @Street, city = @City, streetNumber = @StreetNumber, cep = @Cep, state = @State, cnpj = @Cnpj, ie = @Ie, cellNumber = @CellNumber, landline  = @Landline , email = @Email, date = @Date, observation = @Observation, status = @status WHERE code = @Code";
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
                        command.Parameters.AddWithValue("@Cnpj", mskCnpj.Text);
                        command.Parameters.AddWithValue("@Ie", mskIe.Text);
                        command.Parameters.AddWithValue("@CellNumber", mskCellNumber.Text);
                        command.Parameters.AddWithValue("@LandLine", mskLandline.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Date", mskDate.Text);
                        command.Parameters.AddWithValue("@Observation", txtObservation.Text);
                        command.Parameters.AddWithValue("@status", condition);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Registro atualizado com sucesso!");
                        JuridicalP NewForm = new JuridicalP();
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
            //conexao com o banco para deletar de acordo com o codigo
            try
            {
                conn.Open();
                string query = "DELETE Pjuridica WHERE Code = " + txtCode.Text + " ";
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
            JuridicalP NewForm = new JuridicalP();
            NewForm.Show();
            this.Dispose(false);
        }

        //quando clicar no grid as informacoes vao para os campos
        private void pjuridicaDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (pjuridicaDataGridView.SelectedRows.Count >= 0)
                {
                    txtCode.Text = pjuridicaDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                    txtName.Text = pjuridicaDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    txtAddres.Text = pjuridicaDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                    txtStreet.Text = pjuridicaDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                    txtCity.Text = pjuridicaDataGridView.SelectedRows[0].Cells[4].Value.ToString();
                    txtStreetNumber.Text = pjuridicaDataGridView.SelectedRows[0].Cells[5].Value.ToString();
                    mskCep.Text = pjuridicaDataGridView.SelectedRows[0].Cells[6].Value.ToString();
                    cbState.Text = pjuridicaDataGridView.SelectedRows[0].Cells[7].Value.ToString();
                    mskCnpj.Text = pjuridicaDataGridView.SelectedRows[0].Cells[8].Value.ToString();
                    mskIe.Text = pjuridicaDataGridView.SelectedRows[0].Cells[9].Value.ToString();
                    mskLandline.Text = pjuridicaDataGridView.SelectedRows[0].Cells[10].Value.ToString();
                    mskCellNumber.Text = pjuridicaDataGridView.SelectedRows[0].Cells[11].Value.ToString();
                    txtEmail.Text = pjuridicaDataGridView.SelectedRows[0].Cells[12].Value.ToString();
                    mskDate.Text = pjuridicaDataGridView.SelectedRows[0].Cells[13].Value.ToString();
                    txtObservation.Text = pjuridicaDataGridView.SelectedRows[0].Cells[14].Value.ToString();
                    condition = pjuridicaDataGridView.SelectedRows[0].Cells[15].Value.ToString();
                }
            }
            catch (Exception)
            { }
 
        }

    //colorir o grid de acordo com a condicao(ativo ou inativo)
        private void pjuridicaDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
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
                    if (condition == rbAtivoJ.Text)
                    {
                        cell.Style.BackColor = Color.LightSkyBlue;
                    }
                    //Caso o cliente seja inativo a linha vai ficar vermelha
                    else if (condition == rbInativoJ.Text)
                    {
                        cell.Style.BackColor = Color.IndianRed;
                    }
                    else
                    { }
                }
            }

         }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel4.Enabled = false;
            panel5.Enabled = false;
            btnReturn.Enabled = true;
        }

        private void pjuridicaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pjuridicaBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.exProgramacaoDataSet);
        }

        private void JuridicalP_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'exProgramacaoDataSet.Pjuridica' table. You can move, or remove it, as needed.
            this.pjuridicaTableAdapter.Fill(this.exProgramacaoDataSet.Pjuridica);

        }
        private void btnSelectImage_Click(object sender, EventArgs e)
        {   }
    }
}
