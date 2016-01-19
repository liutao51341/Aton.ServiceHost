using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aton.Services.Host;
using Aton.ServiceHost.ServiceConfigHandler;
using System.Reflection;

namespace Aton.ServiceHost
{
    public partial class HostGUI : Form
    {
        static  ServiceInstallerHelper helper = new ServiceInstallerHelper();
        AtonServiceConfigHandler configHandler;
        string _exePath;
        public HostGUI()
        {
            InitializeComponent();
        }

        public void InitHost(AtonServiceConfigHandler handler,string execpath)
        {
            configHandler = handler;
            _exePath = execpath;
        }

 
        private void 启动服务SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool r = helper.InstallService(_exePath, configHandler.ServiceSummary.ServiceName, configHandler.ServiceSummary.ServiceName, configHandler.ServiceSummary.ServiceDesc, NativeInstaller.ServiceStartType.SERVICE_AUTO_START);
            toolStripStatusLabel1.Text = "安装结果 :" + (r ? "成功" : "失败");
        }

        private void 停止服务CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool r = helper.UnInstallService(configHandler.ServiceSummary.ServiceName);

            toolStripStatusLabel1.Text = "卸载结果 :" + (r ? "成功" : "失败");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool r = helper.StartService(configHandler.ServiceSummary.ServiceName);
            toolStripStatusLabel1.Text = ("启动服务 :" + (r ? "成功" : "失败"));

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool r = helper.StopService(configHandler.ServiceSummary.ServiceName);

            toolStripStatusLabel1.Text = ("停止服务 :" + (r ? "成功" : "失败"));
    
        }

        private void 退出QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 加载服务LToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            foreach (var item in configHandler)
            {
                listBox1.Items.Add(string.Format("服务名称: {0}   {1}", item.Name,item.Desc));
              
            }
        }

        private void 查询服务状态QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = helper.GetServiceState(configHandler.ServiceSummary.ServiceName);
            switch (r)
            {
                case NativeInstaller.ServiceState.SERVICE_STOPPED:
                    MessageBox.Show("服务状态: 停止");
                    break;
                case NativeInstaller.ServiceState.SERVICE_START_PENDING:
                    MessageBox.Show("服务状态: 等待启动");
                    break;
                case NativeInstaller.ServiceState.SERVICE_STOP_PENDING:
                    MessageBox.Show("服务状态: 等待停止");
                    break;
                case NativeInstaller.ServiceState.SERVICE_RUNNING:
                    MessageBox.Show("服务状态: 运行");
                    break;
                case NativeInstaller.ServiceState.SERVICE_CONTINUE_PENDING:
                    MessageBox.Show("服务状态: 等待继续");
                    break;
                case NativeInstaller.ServiceState.SERVICE_PAUSE_PENDING:
                    MessageBox.Show("服务状态: 等待暂停");
                    break;
                case NativeInstaller.ServiceState.SERVICE_PAUSED:
                    MessageBox.Show("服务状态: 暂停");
                    break;
            }
        }
    }
}
