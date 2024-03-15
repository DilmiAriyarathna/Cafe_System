Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form29
    Dim x, cus As Integer

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()
            CheckBox1.BackColor = Color.Tan
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                TextBox3.Focus()
            Else
                TextBox9.Text = TextBox2.Text.Substring(0, 2) 'get first two letters
                TextBox3.Text = TextBox9.Text + "00" + TextBox1.Text
                TextBox4.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox6.Focus()
        End If
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

    Private Sub Form29_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "select max(cus)cus from TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                cus = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        ''
        TextBox1.Text = cus + 1
        ''
        TextBox2.Select()
        ''
        Display()
        ''
        grd()
    End Sub
    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID,Code,Name from Customers", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox3.Enabled = True
        End If
    End Sub

    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 60
        BunifuCustomDataGrid1.Columns(1).Width = 100
        BunifuCustomDataGrid1.Columns(2).Width = 280
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        conn = GetConnect()
        conn.Open()
        SQL = "insert into Customers(ID,Code,Name,Address,Contact1,Contact2,Email,Note,Date)values(" &
            "'" + TextBox1.Text + "' , '" + TextBox3.Text + "','" + TextBox2.Text + "','" + TextBox4.Text + "', " &
            "'" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + d.ToString(formata) + "')"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updates Successfuly !")
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [cus] = [cus] + 1"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        Create_from_other_form()
        ''
        '' clr()
    End Sub
    Private Sub Create_from_other_form()
        If Label9.Text = "55" Then
            Form32.TextBox4.Text = TextBox3.Text
            Form32.TextBox1.Text = TextBox2.Text
            Form32.TextBox2.Text = TextBox5.Text
            Form32.TextBox3.Text = TextBox4.Text
            Form32.Panel4.Visible = False
            Me.Hide()
        End If
    End Sub
    Private Sub clr()
        Dim form29 As New Form29
        Me.Hide()
        form29.Show()
    End Sub
End Class