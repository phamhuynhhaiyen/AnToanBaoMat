using MaHoaDES.DoiTuong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace De6_DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int MaHoaHayGiaiMa = 1;

        DES64Bit MaHoaDES64;
        Khoa Khoa;
        //Thread thread;
        public static string TenTienTrinh = "";
        public static int GiaiDoan = -1;
        private static int Dem = 0;
        private void EnableHoacDisableNut(bool b)
        {
            btnOpenFile.Enabled = btnSaveFile.Enabled = btnGiaiMa.Enabled = btnMaHoa.Enabled  = b;
        }
        private void MaHoa()
        {
            MaHoaDES64 = new DES64Bit();
            TenTienTrinh = "";
            GiaiDoan = 0;
            Dem = 0;
            Khoa = new Khoa(txtKey.Text);
            if (MaHoaHayGiaiMa == 1)
            {

                MaHoaDES64 = new DES64Bit();
                GiaiDoan = 0;
                GiaiDoan = 1;
                string kq = MaHoaDES64.ThucHienDESText(Khoa, txtInput.Text, 1);
                txtOutput.Text = kq;
                GiaiDoan = 2;
                GiaiDoan = 3;
                MessageBox.Show("Mã hóa thành công");
            }
            else
            {
                MaHoaDES64 = new DES64Bit();
                GiaiDoan = 0;
                GiaiDoan = 1;
                string kq = MaHoaDES64.ThucHienDESText(Khoa, txtInput.Text, -1);
                txtOutput.Text = kq;
                if (kq == "")
                {
                    return;
                }
                GiaiDoan = 2;
                GiaiDoan = 3;
                MessageBox.Show("Giải mã thành công");
            }
    }
        private void btnMaHoa_Click(object sender, EventArgs e)
        {
            if(txtInput.Text != string.Empty)
            {
                if (txtKey.TextLength < 16)
                {
                    MessageBox.Show("Vui lòng nhập đủ 16 ký tự khoá");
                }
                else
                {
                    MaHoaHayGiaiMa = 1;
                    EnableHoacDisableNut(false);
                    MaHoa();
                    EnableHoacDisableNut(true);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thêm dữ liệu");
            }

            
        }

        

        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !(ChuoiHexa.BoHexa.Contains(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void btnGiaiMa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput.Text != string.Empty)
                {
                    if (txtKey.TextLength < 16)
                    {
                        MessageBox.Show("Vui lòng nhập đủ 16 ký tự");
                    }
                    else
                    {
                        MaHoaHayGiaiMa = -1;
                        EnableHoacDisableNut(false);
                        MaHoa();
                        EnableHoacDisableNut(true);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng thêm dữ liệu");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Không thể giải mã!");
                EnableHoacDisableNut(true);
            }

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Chọn file(txt) | *.txt;";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    string str;
                    //doc tat ca du lieu trong file luu vao str;
                    str = sr.ReadToEnd();
                    txtInput.Text = str;
                    sr.Close();
                    fs.Close();
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //Hiển thị thư mục này đầu tiên
            saveFileDialog.InitialDirectory = @"E:\";
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtOutput.Text);
                MessageBox.Show("Lưu file thành công!");
            }
                
        }
    }
}
