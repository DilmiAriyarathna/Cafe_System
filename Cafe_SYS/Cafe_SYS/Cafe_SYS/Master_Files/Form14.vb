Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form14
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim user, mcat, scat, supp As String

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        clr()
    End Sub
    Private Sub clr()
        Dim form14 As New Form14
        Me.Hide()
        form14.Show()
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
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Supplier set Name ='" + TextBox20.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Supplier set Code ='" + TextBox19.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Supplier  where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form12.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '''''''''''''running date time
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"
        '''''''''''''''''''''''''''''''''''''''''''''

        conn = GetConnect()
        conn.Open()
        SQL = " update Supplier set Contact1 = '" + TextBox4.Text + "' ,Contact2 = '" + TextBox5.Text + "' , " &
                " Address = '" + TextBox3.Text + "' ,Email = '" + TextBox7.Text + "' , " &
                "Date='" + d.ToString(formata) + "',Time='" + t.ToString(formatb) + "',login = '" + user + "' , " &
                "regstr_no = '" + TextBox8.Text + "' , " &
                "accno = '" + TextBox9.Text + "',Bank = '" + ComboBox1.Text + "' " &
            " where  Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' and ID = '" + TextBox6.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully updated !")
        clr()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If

        FillCombo()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form13.TextBox6.Text = "1"
        Form13.Show()
    End Sub
    Private Sub filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "select  ID,Contact1,Contact2,Address,Email,regstr_no, " &
                "accno,Bank from Supplier where Code = '" + Label18.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox6.Text = dr.GetValue(0).ToString     'id
            TextBox4.Text = dr.GetValue(1).ToString   'con1
            TextBox5.Text = dr.GetValue(2).ToString    'con2
            TextBox3.Text = dr.GetValue(3).ToString    'add
            TextBox7.Text = dr.GetValue(4).ToString    'emil
            TextBox8.Text = dr.GetValue(5).ToString    'reg
            TextBox9.Text = dr.GetValue(6).ToString   'acc
            ComboBox1.Text = dr.GetValue(7).ToString   'bnk
        End While

        conn.Close()
    End Sub
    Private Sub Label18_TextChanged(sender As Object, e As EventArgs) Handles Label18.TextChanged
        filldata()
    End Sub
End Class