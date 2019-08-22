namespace WvsGo.GraphViewer
{
    partial class GraphViewer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.myView = new Northwoods.Go.GoView();
            this.SuspendLayout();
            // 
            // myView
            // 
            this.myView.AllowDrop = false;
            this.myView.AllowInsert = false;
            this.myView.AllowLink = false;
            this.myView.ArrowMoveLarge = 10F;
            this.myView.ArrowMoveSmall = 1F;
            this.myView.BackColor = System.Drawing.Color.White;
            this.myView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myView.Location = new System.Drawing.Point(0, 0);
            this.myView.Name = "myView";
            this.myView.ShowsNegativeCoordinates = false;
            this.myView.Size = new System.Drawing.Size(800, 450);
            this.myView.TabIndex = 0;
            this.myView.Text = "MyView";
            // 
            // GraphViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myView);
            this.Name = "GraphViewer";
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private Northwoods.Go.GoView myView;
    }
}
