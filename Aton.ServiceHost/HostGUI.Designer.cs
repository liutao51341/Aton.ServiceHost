namespace Aton.ServiceHost
{
    partial class HostGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.服务管理MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动服务SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止服务CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.加载服务LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询服务状态QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务管理MToolStripMenuItem,
            this.toolStripMenuItem3,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 服务管理MToolStripMenuItem
            // 
            this.服务管理MToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动服务SToolStripMenuItem,
            this.停止服务CToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.退出QToolStripMenuItem});
            this.服务管理MToolStripMenuItem.Name = "服务管理MToolStripMenuItem";
            this.服务管理MToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.服务管理MToolStripMenuItem.Text = "服务管理(&M)";
            // 
            // 启动服务SToolStripMenuItem
            // 
            this.启动服务SToolStripMenuItem.Name = "启动服务SToolStripMenuItem";
            this.启动服务SToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.启动服务SToolStripMenuItem.Text = "安装服务(&S)";
            this.启动服务SToolStripMenuItem.Click += new System.EventHandler(this.启动服务SToolStripMenuItem_Click);
            // 
            // 停止服务CToolStripMenuItem
            // 
            this.停止服务CToolStripMenuItem.Name = "停止服务CToolStripMenuItem";
            this.停止服务CToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.停止服务CToolStripMenuItem.Text = "卸载服务(&C)";
            this.停止服务CToolStripMenuItem.Click += new System.EventHandler(this.停止服务CToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "启动服务(&R)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "停止服务";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // 退出QToolStripMenuItem
            // 
            this.退出QToolStripMenuItem.Name = "退出QToolStripMenuItem";
            this.退出QToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出QToolStripMenuItem.Text = "退出(&Q)";
            this.退出QToolStripMenuItem.Click += new System.EventHandler(this.退出QToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载服务LToolStripMenuItem,
            this.查询服务状态QToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(81, 20);
            this.toolStripMenuItem3.Text = "服务状态(&S)";
            // 
            // 加载服务LToolStripMenuItem
            // 
            this.加载服务LToolStripMenuItem.Name = "加载服务LToolStripMenuItem";
            this.加载服务LToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.加载服务LToolStripMenuItem.Text = "加载服务(&L)";
            this.加载服务LToolStripMenuItem.Click += new System.EventHandler(this.加载服务LToolStripMenuItem_Click_1);
            // 
            // 查询服务状态QToolStripMenuItem
            // 
            this.查询服务状态QToolStripMenuItem.Name = "查询服务状态QToolStripMenuItem";
            this.查询服务状态QToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.查询服务状态QToolStripMenuItem.Text = "查询服务状态(&Q)";
            this.查询服务状态QToolStripMenuItem.Click += new System.EventHandler(this.查询服务状态QToolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(434, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel1.Text = "状态";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(434, 212);
            this.listBox1.TabIndex = 3;
            // 
            // HostGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(450, 300);
            this.MinimumSize = new System.Drawing.Size(450, 38);
            this.Name = "HostGUI";
            this.Text = "Aton 服务管理器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 服务管理MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动服务SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止服务CToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 退出QToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 加载服务LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询服务状态QToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
    }
}