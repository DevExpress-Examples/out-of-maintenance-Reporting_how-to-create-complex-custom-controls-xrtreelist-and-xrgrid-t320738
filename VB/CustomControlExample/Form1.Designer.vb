Namespace TreeListExample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.button1 = New System.Windows.Forms.Button()
			Me.button2 = New System.Windows.Forms.Button()
			Me.button3 = New System.Windows.Forms.Button()
			Me.button4 = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(13, 13)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(259, 23)
			Me.button1.TabIndex = 0
			Me.button1.Text = "Preview XRTreeList Report"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' button2
			' 
			Me.button2.Location = New System.Drawing.Point(12, 96)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(260, 23)
			Me.button2.TabIndex = 1
			Me.button2.Text = "Preview XRGrid Report"
			Me.button2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button2.Click += new System.EventHandler(this.button2_Click);
			' 
			' button3
			' 
			Me.button3.Location = New System.Drawing.Point(13, 43)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(259, 23)
			Me.button3.TabIndex = 2
			Me.button3.Text = "Design XRTreeList Report"
			Me.button3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button3.Click += new System.EventHandler(this.button3_Click);
			' 
			' button4
			' 
			Me.button4.Location = New System.Drawing.Point(12, 125)
			Me.button4.Name = "button4"
			Me.button4.Size = New System.Drawing.Size(260, 23)
			Me.button4.TabIndex = 3
			Me.button4.Text = "Design XRGrid Report"
			Me.button4.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button4.Click += new System.EventHandler(this.button4_Click);
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(284, 261)
			Me.Controls.Add(Me.button4)
			Me.Controls.Add(Me.button3)
			Me.Controls.Add(Me.button2)
			Me.Controls.Add(Me.button1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents button1 As System.Windows.Forms.Button
		Private WithEvents button2 As System.Windows.Forms.Button
		Private WithEvents button3 As System.Windows.Forms.Button
		Private WithEvents button4 As System.Windows.Forms.Button
	End Class
End Namespace

