namespace paletteControl
{
    partial class LinkControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DisposeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DisposeButton
            // 
            this.DisposeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisposeButton.Location = new System.Drawing.Point(74, 5);
            this.DisposeButton.Name = "DisposeButton";
            this.DisposeButton.Size = new System.Drawing.Size(24, 20);
            this.DisposeButton.TabIndex = 2;
            this.DisposeButton.UseVisualStyleBackColor = true;
            this.DisposeButton.Click += new System.EventHandler(this.DisposeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(0, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(74, 19);
            this.textBox1.TabIndex = 1;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.ExecuteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExecuteButton.Location = new System.Drawing.Point(0, 30);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(101, 42);
            this.ExecuteButton.TabIndex = 3;
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 4;
            // 
            // LinkControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DisposeButton);
            this.Name = "LinkControl";
            this.Size = new System.Drawing.Size(101, 90);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DisposeButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
    }
}
