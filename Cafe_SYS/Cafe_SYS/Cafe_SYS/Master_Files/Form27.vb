Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form27
    Dim x As Integer
    Dim user As Integer

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form27_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "select User_Div from User_Division"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        ComboBox3.Select()
        ComboBox3.DroppedDown = True
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not ComboBox4.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "select ID,val,code from User_Division where User_Div ='" + ComboBox4.Text + "'"
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While (dr.Read())
                    Label7.Text = dr.GetValue(0).ToString
                    Label10.Text = dr.GetValue(1).ToString
                    Label11.Text = dr.GetValue(2).ToString
                End While
                conn.Close()
            End If
            ''
            Button1.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Usert set div='" + Label7.Text + "',U_val='" + Label10.Text + "', " &
            "code='" + Label11.Text + "',Date='" + d.ToString(formata) + "' where UName = '" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Update Successfully !")
        conn.Close()
        Clr()
    End Sub
    Private Sub Clr()
        Dim form27 As New Form27
        Me.Hide()
        form27.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (TextBox2.Text = TextBox3.Text) Then
            conn = GetConnect()
            conn.Open()
            SQL = "update Usert set UName='" + TextBox4.Text + "',pass='" + TextBox2.Text + "', " &
               "Date='" + d.ToString(formata) + "' where UName = '" + ComboBox3.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Update Successfully !")
            conn.Close()
            Clr()
        Else
            MsgBox("Please make sure your passwords match !")
            TextBox3.Text = ""
            TextBox3.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Usert where UName = '" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Update Successfully !")
        conn.Close()
        Clr()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clr()
    End Sub
End Class