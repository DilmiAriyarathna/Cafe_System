Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form7
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        '''''''''''''''''''''
        FillCombo()
        '''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Count(Id) from Main_Cat2"
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
        SQL = "select Name from Main_Cat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()

        TextBox3.Text = "MC/0" + TextBox6.Text

        '''''''''''''''''''''''''''''
        If Not TextBox1.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Main_Cat2(id,code,Name) values ('" + TextBox6.Text + "', " &
            " '" + TextBox3.Text + "' , '" + TextBox1.Text + "' )"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Saved !")
            conn.Close()
        Else
            MsgBox("Invalid Main Category !")
        End If
        Dim form7 As New Form7
        Me.Hide()
        form7.Show()
    End Sub

    Private Sub Form7_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.F3) Then
            Dim form7 As New Form7
            Me.Hide()
            form7.Show()
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where Name ='" + ComboBox1.Text + "'"
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
            SQL = "update Main_Cat2 set Name = '" + TextBox2.Text + "' where Id='" + TextBox4.Text + "' " &
                "and code = '" + TextBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Updated !")
            conn.Close()
        Else
            MsgBox("Invalid Main Category !")
        End If

        Dim form7 As New Form7
        Me.Hide()
        form7.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not TextBox4.Text = "" Then
            conn = GetConnect()
            conn.Open()
            SQL = "delete from Main_Cat2 where Id='" + TextBox4.Text + "' " &
                "and code = '" + TextBox5.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Deleted !")
            conn.Close()
        Else
            MsgBox("Invalid Main Category !")
        End If

        Dim form7 As New Form7
        Me.Hide()
        form7.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class