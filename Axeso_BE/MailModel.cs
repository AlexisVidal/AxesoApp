using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BE
{
    public class MailModel
    {
        public int id { get; set; }
        public string email { get; set; }
        public string newPassword { get; set; }
        public string AppName { get; set; }
        public string PasswordRecovery { get; set; }
        public string PasswordYourNew { get; set; }
        public string PasswordDontForget { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string AdminUser { get; set; }
        public string AdminPassWord { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPName { get; set; }
        public string yournewp { get; set; }
        public string dontforg { get; set; }
    }
}
