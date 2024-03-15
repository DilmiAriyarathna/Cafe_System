Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form15
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        Me.KeyPreview = True
        '''''''''''''''''''''
        FillCombo()
        '''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select max(bnk)bnk from TGC"
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text <> "" Then
            Dim a As String = TextBox1.Text
            TextBox1.Text = (StrConv(a, VbStrConv.ProperCase))
            TextBox1.Select(TextBox1.Text.Length, 0)
        End If
    End Sub
    Private Sub clr()
        Dim form15 As New Form15
        Me.Hide()
        form15.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clr()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Bank where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox4.Text = dr.GetValue(0).ToString
            TextBox5.Text = dr.GetValue(1).ToString
        End While

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox2.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "update Bank set Name = '" + TextBox2.Text + "' where Id='" + TextBox4.Text + "' " &
                "and code = '" + TextBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Updated Successfully !")
            conn.Close()
        Else
            MsgBox("Invalid Bank Name !")
        End If
        clr()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not TextBox4.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "delete from Bank where Id='" + TextBox4.Text + "' " &
                "and code = '" + TextBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Updated Successfully !")
            conn.Close()
        Else
            MsgBox("Invalid Bank !")
        End If
        clr()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        TextBox12.Text = TextBox1.Text.Substring(0, 1) 'get first two letters

        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox18.Text = i.ToString()

        TextBox3.Text = "BK-" + TextBox12.Text + "-0" + TextBox18.Text
        ''''''''''''''''''''
        Dim x = Convert.ToInt32(TextBox6.Text)
        x += 1
        TextBox6.Text = x.ToString()
        '''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [bnk] = [bnk] + 1"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        If Not TextBox1.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Bank(id,Code,Name) values ('" + TextBox6.Text + "', " &
            " '" + TextBox3.Text + "' , '" + TextBox1.Text + "' )"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Updated Successfully !")
            conn.Close()
        Else
            MsgBox("Invalid Bank !")
        End If
        ''''''''''''''''''''''''''
        clr()
    End Sub
End Class