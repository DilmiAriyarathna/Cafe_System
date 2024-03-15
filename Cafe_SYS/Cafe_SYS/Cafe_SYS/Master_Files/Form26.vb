Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form26
    Dim x As Integer
    Dim user As Integer

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form26_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT max(usr)usr FROM TGC"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                user = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception

        End Try
        conn.Close()
        ''
        TextBox1.Text = user + 1
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select User_Div from User_Division"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        ComboBox2.Select()
        ComboBox2.DroppedDown = True
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not ComboBox1.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "select ID,val,code from User_Division where User_Div ='" + ComboBox1.Text + "'"
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
            TextBox6.Focus()
            End If
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If Not TextBox6.Text = "" Then
                conn = GetConnect()
                conn.Open()
                SQL = "select * from Employee where empid = '" + TextBox6.Text + "' "
                cmd1 = New SqlCommand(SQL, conn)
                dt = New DataTable
                da = New SqlDataAdapter(cmd1)
                da.Fill(dt)
                x = Convert.ToInt32(dt.Rows.Count.ToString())
                If x = 0 Then
                    MsgBox("Invalid Employee !")
                    TextBox6.Text = ""
                    TextBox6.Focus()
                    Exit Sub
                ElseIf x > 0 Then
                    conn = GetConnect()
                    conn.Open()
                    SQL = "select Acc_No from Employee where empid ='" + TextBox6.Text + "'"
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While (dr.Read())
                        Label12.Text = dr.GetValue(0).ToString
                    End While
                    conn.Close()
                End If
                conn.Close()
            ElseIf TextBox6.Text = "" Then
                MsgBox("Invalid Employee !")
                TextBox6.Focus()
                Exit Sub
            End If
            TextBox4.Focus()
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox6.Text = "" Then
            MsgBox("Invalid Employee !")
            TextBox6.Focus()
            Exit Sub
        End If
        ''''
        If ComboBox2.Text = "" Then
            MsgBox("Invalid Project !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''
        If ComboBox1.Text = "" Then
            MsgBox("Invalid User Division !")
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''
        If TextBox4.Text = "" Then
            MsgBox("Invalid User Name !")
            TextBox4.Focus()
            Exit Sub
        End If
        ''''
        If TextBox2.Text = "" Then
            MsgBox("Invalid Password !")
            TextBox2.Focus()
            Exit Sub
        End If
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Usert where UName = '" + TextBox4.Text + "' or pass = '" + TextBox2.Text + "' "
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("User Already Exist !")
            TextBox4.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Focus()
            Exit Sub
        Else
            conn = GetConnect()
            conn.Open()
            SQL = "update TGC set [usr] = [usr] + 1"
            cmd3 = New SqlCommand(SQL, conn)
            cmd3.ExecuteNonQuery()
            conn.Close()
            ''
            insert()
        End If
        conn.Close()
    End Sub
    Private Sub insert()
        If (TextBox2.Text = TextBox3.Text) Then
            conn = GetConnect()
            conn.Open()
            cmd = New SqlCommand("insert into Usert(Uid,UName,pass,div,emp_code,project,U_val,Acc_No,Date,code) values ( " &
                " '" + TextBox1.Text + "', '" + TextBox4.Text + "','" + TextBox2.Text + "', " &
                " '" + Label7.Text + "' , '" + TextBox6.Text + "' , '" + ComboBox2.Text + "' , '" + Label10.Text + "', " &
                " '" + Label12.Text + "','" + d.ToString(formata) + "','" + Label11.Text + "')", conn)
            cmd.ExecuteNonQuery()
            MsgBox("Update Successfully !")
            conn.Close()
            Clr()

            'Displaydata()
        Else
            MsgBox("Please make sure your passwords match !")
            TextBox3.Text = ""
            TextBox3.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub Clr()
        Dim form26 As New Form26
        Me.Hide()
        form26.Show()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
End Class