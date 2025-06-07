using System;
using System.Linq;
using System.Windows.Forms;
using Accounting.DataLayer.Context;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmLogin : Form
    {
        public bool IsEdit = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (IsEdit)
                    {
                        var Login = db.LoginRepository.Get().First();
                        Login.UserName = txtUserName.Text;
                        Login.password = txtPassword.Text;
                        db.LoginRepository.Update(Login);
                        db.save();
                        Application.Restart();
                    }
                    else
                    {
                        if (db.LoginRepository.Get(l => l.UserName == txtUserName.Text && l.password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("کاربر یافت نشد");
                        }
                    }
                }
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره تغییرات";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var login = db.LoginRepository.Get().First();
                    txtUserName.Text = login.UserName;
                    txtPassword.Text = login.password;
                }

            }
        }
    }
}
