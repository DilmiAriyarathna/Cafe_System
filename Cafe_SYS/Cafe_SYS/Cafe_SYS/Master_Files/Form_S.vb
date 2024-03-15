Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form_S

    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1, cmd2 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox5.Text = "5" Then
            Form32.src()
        End If
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"

        conn = GetConnect()
        conn.Open()
        cmd = New SqlCommand("select * from Selling_Price where Code='" + TextBox3.Text + "'", conn)
        dt = New DataTable()
        da = New SqlDataAdapter(cmd)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())

        If (x > 0) Then
            SQL = "update Selling_Price set N_sp = '" + TextBox2.Text + "' , O_sp = '" + TextBox1.Text + "' where Code = '" + TextBox3.Text + "'"
            cmd1 = New SqlCommand(SQL, conn)
            cmd1.ExecuteNonQuery()
            'conn.Close()
        Else
            SQL = "insert into Selling_Price (Code,O_sp,N_sp,Date,Time,Login) values ( " &
            " '" + TextBox3.Text + "' , " &
            " '" + TextBox1.Text + "' , " &
            " '" + TextBox2.Text + "' , " &
            " '" + d.ToString(formata) + "' , " &
            " '" + t.ToString(formatb) + "' , " &
            " '" + TextBox4.Text + "')"
            cmd2 = New SqlCommand(SQL, conn)
            cmd2.ExecuteNonQuery()
        End If

        conn.Close()

        Updt()
        clr()
        '  Me.Refresh()
    End Sub

    Private Sub Form_S_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Products"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()

        ComboBox1.Select()
        ComboBox1.DroppedDown = True

        TextBox4.Text = Form1.setname
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Code from Products where Name = '" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read = True)
            TextBox3.Text = dr.GetValue(0).ToString
            'TextBox4.Text = dr.GetValue(0).ToString
        End While
        conn.Close()

    End Sub
    Private Sub clr()
        Dim form_s As New Form_S
        Me.Hide()
        form_s.Show()
    End Sub
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Updt()
        conn = GetConnect()
        conn.Open()
        SQL = "update Products set S_Price = '" + TextBox2.Text + "' where Code = '" + TextBox3.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updated Successfully !")

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select CAST(P_Price as numeric(17,2))P_Price from Products where Code = '" + TextBox3.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read = True)
                TextBox1.Text = dr.GetValue(0).ToString
            End While
            conn.Close()

            TextBox2.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clr()
        Me.Refresh()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button1.Focus()
        End If
    End Sub


End Class