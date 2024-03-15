Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form12
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim user As String

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        count()

        Display()
        grd()

        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If
        FillCombo()
    End Sub
    Private Sub count()
        conn = GetConnect()
        conn.Open()
        SQL = "select max(supp)supp from TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 60
        BunifuCustomDataGrid1.Columns(1).Width = 100
        BunifuCustomDataGrid1.Columns(2).Width = 350
    End Sub
    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID,Code,Name from Supplier", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()
            CheckBox1.BackColor = Color.Tan
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                TextBox2.Focus()
            Else
                TextBox12.Text = TextBox1.Text.Substring(0, 2) 'get first two letters

                Dim i = Convert.ToInt32(TextBox6.Text)
                i += 1
                TextBox18.Text = i.ToString()

                TextBox2.Text = TextBox12.Text + "00" + TextBox18.Text

                TextBox3.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
        End If
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
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text <> "" Then
            Dim a As String = TextBox1.Text
            TextBox1.Text = (StrConv(a, VbStrConv.ProperCase))
            TextBox1.Select(TextBox1.Text.Length, 0)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.Enabled = True
        End If
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Bank"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clr()
    End Sub
    Private Sub ADD()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"

        conn = GetConnect()
        conn.Open()
        SQL = "insert into Supplier(ID,Name,Code,Contact1,Contact2,Address,Email,Date,Time,login,regstr_no,accno,Bank) " &
                "values( '" + TextBox6.Text + "' , '" + TextBox1.Text + "' , '" + TextBox2.Text + "' , '" + TextBox4.Text + "' , " &
                " '" + TextBox5.Text + "' , '" + TextBox3.Text + "' , '" + TextBox7.Text + "' , '" + d.ToString(formata) + "' , " &
                " '" + t.ToString(formatb) + "' , '" + user + "' , '" + TextBox8.Text + "' , '" + TextBox9.Text + "' , " &
                " '" + ComboBox1.Text + "' )"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updates Successfuly !")

    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()
        ''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [supp] = [supp] + 1"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        ADD()
        Display()
        Form13.refreshme()
        clr()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub clr()
        Dim form12 As New Form12
        Me.Hide()
        form12.Show()
    End Sub

End Class