Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form38
    Dim imglocation As String
    Dim Emp_id As Integer
    Dim year, month As String

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"

    Dim gender As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        System.Diagnostics.Process.Start("D:\Projects\Final\Cafe_SYS\Employee\CV")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        imglocation = "D:\Projects\Final\Cafe_SYS\Employee\Img"
        Dim dg As New OpenFileDialog
        If dg.ShowDialog = DialogResult.OK Then
            imglocation = dg.FileName
            PictureBox1.ImageLocation = imglocation
        End If

        dg.Filter = "(*.jpg;*.png)|*.jpg;*.png"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox3.Text = "" Then
            MsgBox("Invalid Full Name !")
            Exit Sub
        End If

        If TextBox4.Text = "" Then
            MsgBox("Invalid Name !")
            Exit Sub
        End If

        If TextBox5.Text = "" Then
            MsgBox("Invalid Address !")
            Exit Sub
        End If

        If TextBox6.Text = "" Then
            MsgBox("Invalid NIC Number !")
            Exit Sub
        End If

        If TextBox8.Text = "" And TextBox9.Text = "" Then
            MsgBox("Invalid Contact Number !")
            Exit Sub
        End If

        If gender = "" Then
            MsgBox("Invalid Gender !")
            Exit Sub
        End If

        If CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("Invalid Permenent Status !")
            Exit Sub
        End If

        If CheckBox3.Checked = False And CheckBox4.Checked = False Then
            MsgBox("Invalid Active Status !")
            Exit Sub
        End If

        If Label26.Text = "" Then
            MsgBox("Invalid Type !")
            Exit Sub
        End If

        If Label27.Text = "" Then
            MsgBox("Invalid Division !")
            Exit Sub
        End If

        'If Label28.Text = "" Then
        '    MsgBox("Invalid Job Roll !")
        '    Exit Sub
        'End If

        If Label29.Text = "" Then
            MsgBox("Invalid Shift !")
            Exit Sub
        End If

        If Label30.Text = "" Then
            MsgBox("Invalid Bank !")
            Exit Sub
        End If

        If TextBox10.Text = "" Then
            MsgBox("Invalid Account Number !")
            Exit Sub
        End If
        ''
        conn = GetConnect()
        ''
        conn.Open()
        SQL = "update TGC set [Emp_ID] = [Emp_ID]+1"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT Emp_ID FROM TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox1.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        ''
        Insert()
        ''
        conn.Open()
        SQL = "Truncate table Table_tmp"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "Print_Emp_once"
        cmd = New SqlCommand(SQL, conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@code", TextBox2.Text)
        cmd.Parameters.AddWithValue("@id", TextBox1.Text)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        MessageBox.Show("Update Successfully !")
        Button6.Enabled = True
        Button3.Enabled = False

    End Sub
    Private Sub Insert()
        conn.Open()
        SQL = "insert into Employee (empid,Code,Full_Name,Name,DOB,Address,NIC,Contact1,Contact2, " &
                                    "Email,Acc_No,Bank,Type,Division,Basic_Sal,Job_role,appoinment, " &
                                    "permenent,shift,gender,p_status,A_status,flag,Login,l_acc,Date,Time,Note,half,OT) " &
                                    " " &
                                    "values(" &
                                    "@empid,@Code,@Full_Name,@Name,@DOB,@Address,@NIC,@Contact1,@Contact2, " &
                                    "@Email,@Acc_No,@Bank,@Type,@Division,@Basic_Sal,@Job_role,@appoinment, " &
                                    "@permenent,@shift,@gender,@p_status,@A_status,@flag,@Login,@l_acc, " &
                                    "@Date,@Time,@Note,@half,@OT)"
        cmd = New SqlCommand(SQL, conn)
        With cmd.Parameters
            .AddWithValue("@empid", TextBox1.Text)
            .AddWithValue("@Code", TextBox2.Text)
            .AddWithValue("@Full_Name", TextBox3.Text)
            .AddWithValue("@Name", TextBox4.Text)
            .AddWithValue("@DOB", DateTimePicker1.Value)
            .AddWithValue("@Address", TextBox5.Text)
            .AddWithValue("@NIC", TextBox6.Text)
            .AddWithValue("@Contact1", TextBox8.Text)
            .AddWithValue("@Contact2", TextBox9.Text)
            .AddWithValue("@Email", TextBox7.Text)
            .AddWithValue("@Acc_No", TextBox10.Text)
            .AddWithValue("@Bank", Label30.Text)
            .AddWithValue("@Type", Label26.Text)
            .AddWithValue("@Division", Label27.Text)
            .AddWithValue("@Basic_Sal", TextBox11.Text)
            .AddWithValue("@Job_role", Label28.Text)
            .AddWithValue("@appoinment", DateTimePicker2.Value)
            .AddWithValue("@permenent", DateTimePicker3.Value)
            .AddWithValue("@shift", Label29.Text)
            .AddWithValue("@gender", gender)

            If CheckBox1.Checked = True Then
                .AddWithValue("@p_status", "1")
            ElseIf CheckBox2.Checked = True Then
                .AddWithValue("@p_status", "5")
            End If

            If CheckBox3.Checked = True Then
                .AddWithValue("@A_status", "1")
            ElseIf CheckBox4.Checked = True Then
                .AddWithValue("@A_status", "5")
            End If

            .AddWithValue("@flag", "1")
            .AddWithValue("@Login", TextBox15.Text)
            .AddWithValue("@l_acc", Label31.Text)
            .AddWithValue("@Date", d.ToString(formata))
            .AddWithValue("@Time", t.ToString(formatb))
            .AddWithValue("@Note", TextBox14.Text)
            .AddWithValue("@half", TextBox12.Text)
            .AddWithValue("@OT", TextBox13.Text)
        End With
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub clr()
        Dim Form38 As New Form38
        Me.Hide()
        Form38.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clr()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form38_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        year = Date.Now.Year.ToString()
        month = Date.Now.Month.ToString()
        Label18.Text = year.Substring(year.Length - 2, 2)
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT max(Emp_ID)Emp_ID FROM TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                Emp_id = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        ''
        TextBox1.Text = Emp_id + 1
        ''
        TextBox2.Text = "TGC" + Label18.Text + "-00" + TextBox1.Text
        ''
        If (Form1.setname = "") Then
            TextBox15.Text = ""
        Else
            TextBox15.Text = Form1.setname
        End If
        ''
        conn.Open()
        SQL = "SELECT Acc_No FROM Usert where UName = '" + TextBox15.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                Label31.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        ''
        fillcombo()
        ''
        TextBox3.Focus()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Staff_Type where Type ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label26.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Staff_Division where Division ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label27.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox4_TextChanged(sender As Object, e As EventArgs) Handles ComboBox4.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Staff_Shift where Shift ='" + ComboBox4.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label29.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox5_TextChanged(sender As Object, e As EventArgs) Handles ComboBox5.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Bank where Name ='" + ComboBox5.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label30.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            DateTimePicker1.Focus()

        End If
    End Sub

    Private Sub DateTimePicker1_CloseUp(sender As Object, e As EventArgs) Handles DateTimePicker1.CloseUp
        TextBox6.Focus()
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            RadioButton1.Focus()
        End If
    End Sub

    Private Sub RadioButton1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RadioButton1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            RadioButton2.Focus()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            gender = "Male"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            gender = "Female"
        End If
    End Sub

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox4.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Enabled = False
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                CheckBox3.Focus()
            ElseIf CheckBox1.Checked = False Then
                CheckBox2.Focus()
            End If
        End If
    End Sub

    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox3.Focus()
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox4.Enabled = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox3.Checked = True Then
                TabPage3.Focus()
            ElseIf CheckBox3.Checked = False Then
                CheckBox4.Focus()
            End If
        End If
    End Sub

    Private Sub CheckBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TabPage3.Focus()
        End If
    End Sub

    Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox14.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox14.Focus()
        End If
    End Sub

    Private Sub TextBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox12.Focus()
        End If
    End Sub

    Private Sub TextBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox13.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim image() As Byte
        'Dim Str As FileStream
        'Dim br As BinaryReader
        'Str = New FileStream(imglocation, FileMode.Open, FileAccess.Read)
        'br = New BinaryReader(Str)
        'image = br.ReadBytes((Int())Str.Length)
        'Convert.ToInt32(FS.Length)
        conn = GetConnect()
        conn.Open()

        Dim mstream As New System.IO.MemoryStream()
        PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        image = mstream.GetBuffer()
        Dim FileSize As UInt32
        FileSize = mstream.Length
        mstream.Close()
        'conn.Open()        ''

        SQL = "insert into Emp_img(Emp_Code,ID,Img)values(@Emp_Code,@ID,@Img)"
        cmd = New SqlCommand(SQL, conn)
        With cmd.Parameters
            .AddWithValue("@Emp_Code", TextBox2.Text)
            .AddWithValue("@ID", TextBox1.Text)
            .AddWithValue("@Img", image)
        End With
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Update Successfully !")

        Button3.Enabled = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form40.Show()
        clr()
    End Sub

    Private Sub fillcombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Type from Staff_Type"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Division from Staff_Division"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        'conn = GetConnect()
        'conn.Open()
        'SQL = "select Name from Main_Cat2"
        'cmd = New SqlCommand(SQL, conn)
        'dr = cmd.ExecuteReader
        'While (dr.Read())
        '    ComboBox3.Items.Add(dr.GetString(0))
        'End While
        'conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Shift from Staff_Shift"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Bank"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox5.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
End Class